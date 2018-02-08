using iSpyApplication.Audio;
using NAudio.Wave;
using System;
namespace iSpyApplication.Video
{
	public interface ISupportsAudio
	{
		event HasAudioStreamEventHandler HasAudioStream;
		WaveFormat RecordingFormat
		{
			get;
			set;
		}
	}
}
