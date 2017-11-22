#include "stdafx.h"
#include "Decoder.h"

/*********************Define Static*********************/

FrameDecodedCallback FFmpegPlayerNative::Decoder::m_cbFrameDecoded;

/*******************************************************/

namespace FFmpegPlayerNative {


Decoder::Decoder()
{
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
	av_register_all();
	avdevice_register_all();
	avcodec_register_all();
	avformat_network_init();

	m_pFormatCtx = avformat_alloc_context();

	m_bInitialized = true;
}

/*
Open the given url and start decoding
*/
void Decoder::Open(std::string url) {
	AVStream* pVideoStream = nullptr;

	// open input
	int error = avformat_open_input(&m_pFormatCtx, url.c_str(), nullptr, nullptr);
	if (error != 0) {
		AvStrError(error);
		return;
	}

	error = avformat_find_stream_info(m_pFormatCtx, nullptr);
	if (error != 0) {
		AvStrError(error);
		return;
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
		return;
	}

	// find decoder
	m_pVideoCodec = avcodec_find_decoder(m_pFormatCtx->streams[m_iVideoStreamIndex]->codecpar->codec_id);
	if (m_pVideoCodec == nullptr) {
		printf("error: no decoder found");
		return;
	}

	// initialize codec
	m_pCodecCtx = avcodec_alloc_context3(m_pVideoCodec);
	error = avcodec_parameters_to_context(m_pCodecCtx, pVideoStream->codecpar);
	if (error < 0)
	{
		AvStrError(error);
		return;
	}

	error = avcodec_open2(m_pCodecCtx, m_pVideoCodec, nullptr);
	if (error < 0) {
		AvStrError(error);
		return;
	}
	
	// start decode thread
	// std::thread decThread(&Decoder::DecodeVideo, this); // c++/CLI can not have std::thread
}

/*
Decode video thread. Calls SendFrame for every decoded frame
*/
void Decoder::DecodeVideo() {
	AVPacket* packet = (AVPacket *)av_malloc(sizeof(AVPacket)); // encoded packet
	AVFrame* pFrame = av_frame_alloc(); // decoded frame
	AVFrame* pFrameRgb = av_frame_alloc(); // RGB format frame
	SwsContext *pSwsCtx; // image format conversion context

	AVPixelFormat format = AV_PIX_FMT_RGB24;
	//int got_picture; // deprecated

	int error = av_image_alloc(pFrameRgb->data, pFrameRgb->linesize, m_pCodecCtx->width, m_pCodecCtx->height, format, 1);
	if (error < 0) {
		AvStrError(error);
	}

	// format conversion, scaling context
	pSwsCtx = sws_getContext(
		m_pCodecCtx->width, m_pCodecCtx->height, m_pCodecCtx->pix_fmt,
		m_pCodecCtx->width, m_pCodecCtx->height, format, 
		SWS_BICUBIC, nullptr, nullptr, nullptr);

	// decode and get frames
	while (true) {
		// read from input into packet
		error = av_read_frame(m_pFormatCtx, packet);
		if (error < 0) {
			AvStrError(error);
		}

		// deprecated
		//error = avcodec_decode_video2(m_pCodecCtx, pFrame, &got_picture, packet);
		//if (error < 0) {
		//	AvStrError(error);
		//	return;
		//}

		// send in for decoding
		error = avcodec_send_packet(m_pCodecCtx, packet);
		if (error == AVERROR_EOF)
		{
			break;
		}

		// retrieve decoded frame
		error = avcodec_receive_frame(m_pCodecCtx, pFrame);
		if (error == 0) {
			// convert to given format
			int rows = sws_scale(pSwsCtx, pFrame->data, pFrame->linesize, 0, 
				m_pCodecCtx->height, pFrameRgb->data, pFrameRgb->linesize);

			pFrameRgb->width = m_pCodecCtx->width;
			pFrameRgb->height = m_pCodecCtx->height;
			SendFrame(pFrameRgb);
		}

		// free encoded packet
		av_packet_unref(packet);
	}
}

void Decoder::SendFrame(AVFrame * frame) {
	m_cbFrameDecoded(frame->data[0], frame->linesize, frame->width, frame->height, frame->pts);
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
	FFmpegPlayerNative::Decoder::m_cbFrameDecoded = aCallback;
}

}
