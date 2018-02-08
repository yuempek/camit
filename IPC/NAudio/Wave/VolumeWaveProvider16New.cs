using System;
namespace NAudio.Wave
{
	public class VolumeWaveProvider16New : IWaveProvider
	{
		private readonly IWaveProvider _sourceProvider;
		private float _volume;
		public float Volume
		{
			get
			{
				return this._volume;
			}
			set
			{
				this._volume = value;
			}
		}
		public WaveFormat WaveFormat
		{
			get
			{
				return this._sourceProvider.WaveFormat;
			}
		}
		public VolumeWaveProvider16New(IWaveProvider sourceProvider)
		{
			this.Volume = 1f;
			this._sourceProvider = sourceProvider;
			if (sourceProvider.WaveFormat.Encoding != WaveFormatEncoding.Pcm)
			{
				throw new System.ArgumentException("Expecting PCM input");
			}
			if (sourceProvider.WaveFormat.BitsPerSample != 16)
			{
				throw new System.ArgumentException("Expecting 16 bit");
			}
		}
		public int Read(byte[] buffer, int offset, int count)
		{
			int num = this._sourceProvider.Read(buffer, offset, count);
			if (System.Math.Abs(this._volume - 0f) < 1.401298E-45f)
			{
				for (int i = 0; i < num; i++)
				{
					buffer[offset++] = 0;
				}
			}
			else
			{
				if (System.Math.Abs(this._volume - 1f) > 1.401298E-45f)
				{
					for (int j = 0; j < num; j += 2)
					{
						short num2 = (short)((int)buffer[offset + 1] << 8 | (int)buffer[offset]);
						float num3 = (float)num2 * this._volume;
						num2 = (short)num3;
						if (this.Volume > 1f)
						{
							if (num3 > 32767f)
							{
								num2 = 32767;
							}
							else
							{
								if (num3 < -32768f)
								{
									num2 = -32768;
								}
							}
						}
						buffer[offset++] = (byte)(num2 & 255);
						buffer[offset++] = (byte)(num2 >> 8);
					}
				}
			}
			return num;
		}
	}
}
