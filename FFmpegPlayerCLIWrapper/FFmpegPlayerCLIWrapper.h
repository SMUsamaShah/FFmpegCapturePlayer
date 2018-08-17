// FFmpegPlayerCLIWrapper.h

#pragma once

#include "Decoder.h"
#include "ImageConverter.h"

#include <msclr\marshal_cppstd.h>

using namespace System;
using namespace msclr::interop;
using namespace System::Runtime::InteropServices;

//#pragma comment(lib, "FFmpegPlayerNative.lib")

namespace FFmpegPlayerCLI {

	public ref class FFWrapper
	{
	public:
		FFmpegPlayerNative::Decoder* nativeDecoder;

		delegate void FrameDecodedCallback_(IntPtr data, int size, int width, int height, Int64 pts, int format);

		FFWrapper();

		void Initialize();
		void CreateDecoder();
		void CreateDecoder(int format);

		void RegisterDecodedCallback(FrameDecodedCallback_^ fp);

		//void Open(System::String^ url);
		System::String^ Open(System::String^ url);

		void DecodeLoop();

		static void YUV2RGB(
			array<System::Byte>^ yuvData,
			array<System::Byte>^ rgbData,
			int w, int h
		);
	};
}
