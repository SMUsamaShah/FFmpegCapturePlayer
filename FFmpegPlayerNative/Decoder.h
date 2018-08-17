#pragma once

extern "C"
{
#include "libavcodec/avcodec.h"
#include "libavformat/avformat.h"
#include "libavdevice/avdevice.h"
#include "libswscale/swscale.h"
#include "libavutil/imgutils.h"
}

#include <string>

//extern "C" __declspec(dllexport)
//extern "C" __stdcall
#define DllExport   __declspec( dllexport ) 

typedef int(__stdcall* FrameDecodedCallback)(void* data, int size, int width, int height, int64_t pts, int format);

namespace FFmpegPlayerNative {

	class DllExport Decoder
	{
	public:
		Decoder();
		~Decoder();

		Decoder(int format);
		void                                 Initialize();
		//void                                 Open(std::string url);
		std::string                          Open(std::string url);
		void                                 SendFrame(AVFrame * frame);
		void                                 DecodeVideo();
		void                                 SetCallback(FrameDecodedCallback aCallback);

		void                                 CopyFrame(AVFrame *dstFrame, AVFrame *srcFrame);
		int                                  CopyFrameToBuffer(uint8_t** dstBuffer, int* dstbufsize, AVFrame* scrFrame);

		//static FrameDecodedCallback          m_cbFrameDecoded;
		FrameDecodedCallback                 m_cbFrameDecoded;

	private:
		std::string                          AvStrError(int errnum);

		bool                                 m_bInitialized;
		AVFormatContext*                     m_pFormatCtx;
		AVCodecContext*                      m_pCodecCtx;
		AVCodec*                             m_pVideoCodec;
		int                                  m_iVideoStreamIndex = -1;
		int                                  m_iAvgFrameRate;

		AVPixelFormat                        m_convFormat; // format to convert to
	};
}



