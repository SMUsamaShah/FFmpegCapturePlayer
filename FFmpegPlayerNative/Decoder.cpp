#include "stdafx.h"
#include "Decoder.h"

/*********************Define Static*********************/

//FrameDecodedCallback FFmpegPlayerNative::Decoder::m_cbFrameDecoded;

/*******************************************************/
#define RAWFRAME
namespace FFmpegPlayerNative {

Decoder::Decoder()
{
	m_convFormat = AV_PIX_FMT_BGR24;
	//m_sendFormat = AV_PIX_FMT_YUV420P; //AV_PIX_FMT_YUV420P; // AV_PIX_FMT_BGR24
	Initialize();
}

Decoder::Decoder(int sendFormat)
{
	m_convFormat = (AVPixelFormat)sendFormat; 
	//m_sendFormat = (AVPixelFormat)sendFormat; //AV_PIX_FMT_YUV420P; // AV_PIX_FMT_BGR24
	Initialize();
}

Decoder::~Decoder()
{
	//av_frame_free(&pFrameYUV);
	//av_frame_free(&pFrame);
	avcodec_free_context(&m_pCodecCtx);
	avformat_close_input(&m_pFormatCtx);
}

void Decoder::Initialize() {
	//av_register_all();
	avdevice_register_all();
	//--avcodec_register_all();
	avformat_network_init();

	m_pFormatCtx = avformat_alloc_context();

	m_bInitialized = true;
}

/*
Open ffmpeg with the given url.
*/
std::string Decoder::Open(std::string url) {
	AVStream* pVideoStream = nullptr;

	// open input
	int error = avformat_open_input(&m_pFormatCtx, url.c_str(), nullptr, nullptr);
	if (error != 0) {
		return AvStrError(error);
		//return;
	}

	error = avformat_find_stream_info(m_pFormatCtx, nullptr);
	if (error != 0) {
		return AvStrError(error);
		//return;
	}

	// find video stream
	for (unsigned int i = 0; i < m_pFormatCtx->nb_streams; ++i) {
		if (m_pFormatCtx->streams[i]->codecpar->codec_type == AVMEDIA_TYPE_VIDEO) 
		{
			m_iVideoStreamIndex = i;
			pVideoStream = m_pFormatCtx->streams[i];
			break;
		}
	}
	if (m_iVideoStreamIndex == -1 || pVideoStream == nullptr) {
		printf("error: no video stream found");
		return std::string("error: no video stream found");
	}

	// find decoder
	m_pVideoCodec = avcodec_find_decoder(m_pFormatCtx->streams[m_iVideoStreamIndex]->codecpar->codec_id);
	if (m_pVideoCodec == nullptr) {
		printf("error: no decoder found");
		return std::string("error: no decoder found");
	}

	// initialize codec
	m_pCodecCtx = avcodec_alloc_context3(m_pVideoCodec);
	error = avcodec_parameters_to_context(m_pCodecCtx, pVideoStream->codecpar);
	if (error < 0)
	{
		return AvStrError(error);
		//return;
	}

	error = avcodec_open2(m_pCodecCtx, m_pVideoCodec, nullptr);
	if (error < 0) {
		return AvStrError(error);
		//return;
	}
	return std::string();
	// std::thread decThread(&Decoder::DecodeVideo, this); // c++/CLI can not have std::thread
}

/*
Decode video thread. Calls SendFrame for every decoded frame
*/
void Decoder::DecodeVideo() {
	int error = 0;
	
	AVPacket* packet = (AVPacket *)av_malloc(sizeof(AVPacket)); // encoded packet
	AVFrame* pFrame = av_frame_alloc(); // decoded frame
	AVFrame* pFrameRgb = av_frame_alloc(); // converted RGB format frame
	error = av_image_alloc(pFrameRgb->data, pFrameRgb->linesize, m_pCodecCtx->width, m_pCodecCtx->height, m_convFormat, 1);
	if (error < 0) {
		AvStrError(error);
	}
	
	SwsContext *pSwsCtx; // image format conversion context

	// format conversion, scaling context
	pSwsCtx = sws_getContext(
		m_pCodecCtx->width, m_pCodecCtx->height, m_pCodecCtx->pix_fmt,
		m_pCodecCtx->width, m_pCodecCtx->height, m_convFormat, 
		SWS_BICUBIC, nullptr, nullptr, nullptr);

	// decode and get frames
	while (true) {
		// read from input into packet
		error = av_read_frame(m_pFormatCtx, packet);
		if (error < 0) {
			if (error == AVERROR_EOF)
				return;
			AvStrError(error);
		}

		// send in for decoding only video stream
		if (packet->stream_index == m_iVideoStreamIndex) {
			error = avcodec_send_packet(m_pCodecCtx, packet);
			if (/*error == AVERROR_EOF || */error < 0)
			{
				goto clean;
			}
		}
		else {
			continue;
		}
		// retrieve decoded frame
		error = avcodec_receive_frame(m_pCodecCtx, pFrame);
		if (error == 0) {
			if (m_convFormat == AV_PIX_FMT_YUV420P && pFrame->format == AV_PIX_FMT_YUV420P)
			{
				// no conversion needed
				SendFrame(pFrame);
			}
			else //if (AV_PIX_FMT_YUV420P != m_convFormat)
			{
				// convert to given format (rgb by default)
				int rows = sws_scale(pSwsCtx, pFrame->data, pFrame->linesize, 0,
					m_pCodecCtx->height, pFrameRgb->data, pFrameRgb->linesize);

				pFrameRgb->width = pFrame->width;
				pFrameRgb->height = pFrame->height;
				pFrameRgb->pts = pFrame->pts;
				pFrameRgb->format = m_convFormat;
				SendFrame(pFrameRgb);
			}
		}
		else if (error == AVERROR_EOF)
		{
			goto clean;
		}
		else if (error < 0 && error != AVERROR(EAGAIN))
		{
			goto clean;
		}

	clean:
		if (error < 0)
			AvStrError(error);

		// free encoded packet
		av_packet_unref(packet);
	}
	
	// free allocated frames
	av_freep(&pFrameRgb->data[0]);
	av_frame_free(&pFrameRgb);

	av_freep(&pFrame->data[0]);
	av_frame_free(&pFrame);
}

void Decoder::SendFrame(AVFrame * frame) 
{
	//***
	// 1000 decoded non-converted frames (YUV) ~= 440MB, livesteam:  680MB
	// 1000 decoded and converted frames (RGB) ~= 810MB, livesteam: 1200MB
	// static int n = 0;
	// if (n++ > 1000) // to test memory usage by 1000 decoded frames
	//	 return;
	//***
	

	if (frame->format == 0) 
	{
		uint8_t* buffer;
		int bufsize;
		CopyFrameToBuffer(&buffer, &bufsize, frame);

		m_cbFrameDecoded(buffer, bufsize, frame->width, frame->height, frame->pts, frame->format);

		delete[] buffer;
	}

	// send without copying to array for RGB/BGR
	if (frame->format == AVPixelFormat::AV_PIX_FMT_BGR24) {
		// send pointer only, data will be copied from this pointer on the other side.
		// this is possible for RGB because RGB data is already in one plane in data[0] only
		// unconverted YUV data is in 3 planes in data[0], data[1] and data[2]
		//
		// Above method (CopyFrameToBuffer) can also be used in place of this
		m_cbFrameDecoded(frame->data[0], frame->linesize[0], frame->width, frame->height, frame->pts, frame->format);
	}
}

// Deep copy an AVFrame
void Decoder::CopyFrame(AVFrame *dstFrame, AVFrame *srcFrame) {
	AVFrame *copyFrame = av_frame_alloc();
	copyFrame->format = srcFrame->format;
	copyFrame->width = srcFrame->width;
	copyFrame->height = srcFrame->height;
	copyFrame->channels = srcFrame->channels;
	copyFrame->channel_layout = srcFrame->channel_layout;
	copyFrame->nb_samples = srcFrame->nb_samples;

	av_frame_get_buffer(copyFrame, 32);
	av_frame_copy(copyFrame, srcFrame);
	av_frame_copy_props(copyFrame, srcFrame);

	dstFrame = copyFrame;
}

// Copy frame data to a 2d array
int Decoder::CopyFrameToBuffer(uint8_t** dstBuffer, int* dstbufsize , AVFrame *srcFrame) {
	int error = 0;
	int align1 = 32;
	int align2 = 8; // align 16,32 is not working correctly for yuv, image returned is distorted

	int bufsize = av_image_get_buffer_size((AVPixelFormat)srcFrame->format,
		srcFrame->width, srcFrame->height, align1);

	if (bufsize < 0) {
		error = bufsize;
		AvStrError(error);
		return error;
	}

	uint8_t* buffer = new uint8_t[bufsize];

	int byteswritten = av_image_copy_to_buffer(buffer, bufsize, srcFrame->data, srcFrame->linesize,
		(AVPixelFormat)srcFrame->format, srcFrame->width, srcFrame->height, align2);

	if (byteswritten < 0) {
		error = byteswritten;
		AvStrError(error);
		return error;
	}

	*dstbufsize = bufsize;
	*dstBuffer = buffer;

	return error;
}

std::string Decoder::AvStrError(int errnum)
{
	char buf[128];
	av_strerror(errnum, buf, sizeof(buf));
	printf("error: %s", buf);
	return std::string(buf);
}

void Decoder::SetCallback(FrameDecodedCallback aCallback)
{
	//FFmpegPlayerNative::Decoder::m_cbFrameDecoded = aCallback;
	m_cbFrameDecoded = aCallback;
}

}
