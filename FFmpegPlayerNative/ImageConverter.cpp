#include "stdafx.h"
#include "ImageConverter.h"

namespace FFmpegPlayerNative {

// https://stackoverflow.com/a/16108293
void ImageConverter::YUV2RGB(unsigned char *yuvDataIn, unsigned char *rgbDataOut, int w, int h, int outNCh) {
	const int ch2 = 2 * outNCh;

	unsigned char* pRGBs = (unsigned char*)rgbDataOut;
	unsigned char* pYUVs = (unsigned char*)yuvDataIn;

	for (int r = 0; r < h; r++)
	{
		unsigned char* pRGB = pRGBs + r * w * outNCh;
		unsigned char* pYUV = pYUVs + r * w * 2;

		//process two pixels at a time
		for (int c = 0; c < w; c += 2)
		{
			int C1 = pYUV[1] - 16;
			int C2 = pYUV[3] - 16;
			int D = pYUV[2] - 128;
			int E = pYUV[0] - 128;

			int R1 = (298 * C1 + 409 * E + 128) >> 8;
			int G1 = (298 * C1 - 100 * D - 208 * E + 128) >> 8;
			int B1 = (298 * C1 + 516 * D + 128) >> 8;

			int R2 = (298 * C2 + 409 * E + 128) >> 8;
			int G2 = (298 * C2 - 100 * D - 208 * E + 128) >> 8;
			int B2 = (298 * C2 + 516 * D + 128) >> 8;

			//unsurprisingly this takes the bulk of the time.
			pRGB[0] = (unsigned char)(R1 < 0 ? 0 : R1 > 255 ? 255 : R1);
			pRGB[1] = (unsigned char)(G1 < 0 ? 0 : G1 > 255 ? 255 : G1);
			pRGB[2] = (unsigned char)(B1 < 0 ? 0 : B1 > 255 ? 255 : B1);

			pRGB[3] = (unsigned char)(R2 < 0 ? 0 : R2 > 255 ? 255 : R2);
			pRGB[4] = (unsigned char)(G2 < 0 ? 0 : G2 > 255 ? 255 : G2);
			pRGB[5] = (unsigned char)(B2 < 0 ? 0 : B2 > 255 ? 255 : B2);

			pRGB += ch2;
			pYUV += 4;
		}
	}
}


// https://github.com/latelee/yuv2rgb/blob/master/yuv2rgb.c

static long int crv_tab[256];
static long int cbu_tab[256];
static long int cgu_tab[256];
static long int cgv_tab[256];
static long int tab_76309[256];
static unsigned char clp[1024];   //for clip in CCIR601   

void ImageConverter::init_yuv420p_table()
{
	long int crv, cbu, cgu, cgv;
	int i, ind;

	crv = 104597; cbu = 132201;  /* fra matrise i global.h */
	cgu = 25675;  cgv = 53279;

	for (i = 0; i < 256; i++)
	{
		crv_tab[i] = (i - 128) * crv;
		cbu_tab[i] = (i - 128) * cbu;
		cgu_tab[i] = (i - 128) * cgu;
		cgv_tab[i] = (i - 128) * cgv;
		tab_76309[i] = 76309 * (i - 16);
	}

	for (i = 0; i < 384; i++)
		clp[i] = 0;
	ind = 384;
	for (i = 0; i < 256; i++)
		clp[ind++] = i;
	ind = 640;
	for (i = 0; i < 384; i++)
		clp[ind++] = 255;
}

void ImageConverter::yuv420p_to_rgb24(unsigned char* yuvbuffer, unsigned char* rgbbuffer, int width, int height)
{
	int y1, y2, u, v;
	unsigned char *py1, *py2;
	int i, j, c1, c2, c3, c4;
	unsigned char *d1, *d2;
	unsigned char *src_u, *src_v;
	static int init_yuv420p = 0;

	src_u = yuvbuffer + width * height;   // u
	src_v = src_u + width * height / 4;  // v

	py1 = yuvbuffer;   // y
	py2 = py1 + width;
	d1 = rgbbuffer;
	d2 = d1 + 3 * width;

	if (init_yuv420p == 0)
	{
		init_yuv420p_table();
		init_yuv420p = 1;
	}

	for (j = 0; j < height; j += 2)
	{
		for (i = 0; i < width; i += 2)
		{
			u = *src_u++;
			v = *src_v++;

			c1 = crv_tab[v];
			c2 = cgu_tab[u];
			c3 = cgv_tab[v];
			c4 = cbu_tab[u];

			//up-left   
			y1 = tab_76309[*py1++];
			*d1++ = clp[384 + ((y1 + c1) >> 16)];
			*d1++ = clp[384 + ((y1 - c2 - c3) >> 16)];
			*d1++ = clp[384 + ((y1 + c4) >> 16)];

			//down-left   
			y2 = tab_76309[*py2++];
			*d2++ = clp[384 + ((y2 + c1) >> 16)];
			*d2++ = clp[384 + ((y2 - c2 - c3) >> 16)];
			*d2++ = clp[384 + ((y2 + c4) >> 16)];

			//up-right   
			y1 = tab_76309[*py1++];
			*d1++ = clp[384 + ((y1 + c1) >> 16)];
			*d1++ = clp[384 + ((y1 - c2 - c3) >> 16)];
			*d1++ = clp[384 + ((y1 + c4) >> 16)];

			//down-right   
			y2 = tab_76309[*py2++];
			*d2++ = clp[384 + ((y2 + c1) >> 16)];
			*d2++ = clp[384 + ((y2 - c2 - c3) >> 16)];
			*d2++ = clp[384 + ((y2 + c4) >> 16)];
		}
		d1 += 3 * width;
		d2 += 3 * width;
		py1 += width;
		py2 += width;
	}
}

}