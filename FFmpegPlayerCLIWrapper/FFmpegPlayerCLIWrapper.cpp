// This is the main DLL file.

#include "stdafx.h"

#include "FFmpegPlayerCLIWrapper.h"

namespace FFmpegPlayerCLI {
	FFWrapper::FFWrapper() {
	}

	void FFWrapper::Initialize() {
	}

	void FFWrapper::CreateDecoder() {
		nativeDecoder = new FFmpegPlayerNative::Decoder();
	}

	void FFWrapper::CreateDecoder(int format) {
		nativeDecoder = new FFmpegPlayerNative::Decoder(format);
	}

	void FFWrapper::RegisterDecodedCallback(FrameDecodedCallback_^ fp) {
		IntPtr stubPointer = Marshal::GetFunctionPointerForDelegate(fp);
		nativeDecoder->SetCallback(static_cast<FrameDecodedCallback>(stubPointer.ToPointer()));
	}

	/*void FFWrapper::Open(System::String^ url) {
		nativeDecoder->Open(marshal_as<std::string>(url));
	}*/

	System::String^ FFWrapper::Open(System::String^ url) {
		return gcnew String(nativeDecoder->Open(marshal_as<std::string>(url)).c_str());
	}

	void FFWrapper::DecodeLoop() {
		nativeDecoder->DecodeVideo();
	}

	void FFWrapper::YUV2RGB(
		array<System::Byte>^ yuvData,
		array<System::Byte>^ rgbData,
		int w, int h
	)
	{
		pin_ptr<System::Byte> yuv = &yuvData[0];
		pin_ptr<System::Byte> rgb = &rgbData[0];

		FFmpegPlayerNative::ImageConverter::yuv420p_to_rgb24(yuv, rgb, w, h);
	}
}
