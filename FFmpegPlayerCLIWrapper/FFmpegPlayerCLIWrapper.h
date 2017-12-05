// FFmpegPlayerCLIWrapper.h

#pragma once

#include "Decoder.h"

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

		delegate void FrameDecodedCallback_(IntPtr data, IntPtr linesize, int width, int height, Int64 pts);
		//delegate void FrameDecodedCallback_(array<System::Byte>^ data, IntPtr linesize, int width, int height, Int64 pts);

		FFWrapper();

		void Initialize();

		void RegisterDecodedCallback(FrameDecodedCallback_^ fp);

		void Open(System::String^ url);

		void DecodeLoop();
	};
}
