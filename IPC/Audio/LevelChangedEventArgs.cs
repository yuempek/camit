using System;
namespace iSpyApplication.Audio
{
	public class LevelChangedEventArgs : System.EventArgs
	{
		private readonly float[] _maxsamples;
		public float[] MaxSamples
		{
			get
			{
				return this._maxsamples;
			}
		}
		public LevelChangedEventArgs(float[] maxsamples)
		{
			this._maxsamples = maxsamples;
		}
	}
}
