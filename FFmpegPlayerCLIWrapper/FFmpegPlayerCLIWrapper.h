// FFmpegPlayerCLIWrapper.h

#pragma once

#include "Decoder.h"

#include <msclr\marshal_cppstd.h>

using namespace System;
using namespace msclr::interop;
using namespace System::Runtime::InteropServices;
using namespace std;

//#pragma comment(lib, "FFmpegPlayerNative.lib")

//delegate void FrameDecodedCallback_(void* data, int* linesize, int width, int height);

namespace FFmpegPlayerCLI {

	//delegate void FrameDecodedCallback_(void* data, int* linesize, int width, int height);

	public ref class FFWrapper
	{
	public:
		FFmpegPlayerNative::Decoder* nativeDecoder;

		delegate void FrameDecodedCallback_(IntPtr data, IntPtr linesize, int width, int height, Int64 pts);

		FFWrapper();

		void Initialize();

		void RegisterDecodedCallback(FrameDecodedCallback_^ fp);

		void Open(System::String^ url);

		void DecodeThread();
	};
}
