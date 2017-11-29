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

typedef int(__stdcall* FrameDecodedCallback)(void* data, int* linesize, int width, int height, int64_t pts);

namespace FFmpegPlayerNative {

	class DllExport Decoder
	{
	public:
		Decoder();
		~Decoder();

		void                                          Initialize();
		void                                          Open(std::string url);
		void                                          SendFrame(AVFrame * frame);
		void                                          DecodeVideo();
		void                                          SetCallback(FrameDecodedCallback aCallback);

		static FrameDecodedCallback                   m_cbFrameDecoded;

	private:
		std::string                                   AvStrError(int errnum);

		bool                                          m_bInitialized;
		AVFormatContext*                              m_pFormatCtx;
		AVCodecContext*                               m_pCodecCtx;
		AVCodec*                                      m_pVideoCodec;
		int                                           m_iVideoStreamIndex = -1;
		int                                           m_iAvgFrameRate;
		AVPixelFormat                                 m_format;

	};
}



