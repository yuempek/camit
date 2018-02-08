using System;
namespace iSpyApplication.Audio
{
	public class DataAvailableEventArgs : System.EventArgs
	{
		private readonly byte[] _raw;
		public byte[] RawData
		{
			get
			{
				return this._raw;
			}
		}
		public DataAvailableEventArgs(byte[] raw)
		{
			this._raw = raw;
		}
	}
}
