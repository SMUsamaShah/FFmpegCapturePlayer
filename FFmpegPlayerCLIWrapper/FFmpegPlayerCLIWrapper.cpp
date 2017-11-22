// This is the main DLL file.

#include "stdafx.h"

#include "FFmpegPlayerCLIWrapper.h"

namespace FFmpegPlayerCLI {
	FFWrapper::FFWrapper() {
		Initialize();
	}

	void FFWrapper::Initialize() {
		nativeDecoder = new FFmpegPlayerNative::Decoder();
	}

	void FFWrapper::RegisterDecodedCallback(FrameDecodedCallback_^ fp) {
		IntPtr stubPointer = Marshal::GetFunctionPointerForDelegate(fp);
		nativeDecoder->SetCallback(static_cast<FrameDecodedCallback>(stubPointer.ToPointer()));
	}

	void FFWrapper::Open(System::String^ url) {
		nativeDecoder->Open(marshal_as<string>(url));
	}

	void FFWrapper::DecodeThread() {
		nativeDecoder->DecodeVideo();
	}
}
