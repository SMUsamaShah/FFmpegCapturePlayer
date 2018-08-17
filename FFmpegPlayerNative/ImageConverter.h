#pragma once

#include <string>

#define DllExport   __declspec( dllexport ) 

namespace FFmpegPlayerNative {

	class DllExport ImageConverter
	{
	public:
		static void                 YUV2RGB(unsigned char * yuvDataIn, unsigned char * rgbDataOut, int w, int h, int outNCh);
		
		static void                 yuv420p_to_rgb24(unsigned char* yuvbuffer, unsigned char* rgbbuffer, int width, int height);
	private: 
		static void                 init_yuv420p_table();

		//ImageConverter();
		//~ImageConverter();
	};
}

