using System;
namespace iSpyApplication.Audio
{
	public class AudioSourceErrorEventArgs : System.EventArgs
	{
		private readonly string _description;
		public string Description
		{
			get
			{
				return this._description;
			}
		}
		public AudioSourceErrorEventArgs(string description)
		{
			this._description = description;
		}
	}
}
