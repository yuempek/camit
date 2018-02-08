using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Mail;
namespace iSpyApplication
{
	public static class Extensions
	{
		private static readonly System.Collections.Generic.Dictionary<string, System.Drawing.Color> Colours = new System.Collections.Generic.Dictionary<string, System.Drawing.Color>();
		public static bool IsValidEmail(this string email)
		{
			MailMessage mailMessage = new MailMessage();
			bool flag = false;
			try
			{
				mailMessage.To.Add(email);
			}
			catch
			{
				flag = true;
			}
			mailMessage.Dispose();
			return !flag;
		}
		public static System.Drawing.Color ToColor(this string colorRGB)
		{
			if (Extensions.Colours.ContainsKey(colorRGB))
			{
				return Extensions.Colours[colorRGB];
			}
			string[] array = colorRGB.Split(new char[]
			{
				','
			});
			System.Drawing.Color color = System.Drawing.Color.FromArgb((int)System.Convert.ToInt16(array[0]), (int)System.Convert.ToInt16(array[1]), (int)System.Convert.ToInt16(array[2]));
			try
			{
				Extensions.Colours.Add(colorRGB, color);
			}
			catch
			{
			}
			return color;
		}
		public static string ToRGBString(this System.Drawing.Color color)
		{
			return string.Concat(new object[]
			{
				color.R,
				",",
				color.G,
				",",
				color.B
			});
		}
		public static bool Has<T>(this System.Enum type, T value)
		{
            try
            {
                return (((int)(object)type & (int)(object)value) == (int)(object)value);
            }
            catch
            {
                return false;
            }
		}
		public static bool Is<T>(this System.Enum type, T value)
		{
            try
            {
                return (int)(object)type == (int)(object)value;
            }
            catch
            {
                return false;
            }
		}
		public static T Add<T>(this System.Enum type, T value)
		{
            try
            {
                return (T)(object)(((int)(object)type | (int)(object)value));
            }
            catch (Exception ex)
            {
                throw new ArgumentException(
                    string.Format(
                        "Could not append value from enumerated type '{0}'.",
                        typeof(T).Name
                        ), ex);
            }
		}
		public static T Remove<T>(this System.Enum type, T value)
		{
            try
            {
                return (T)(object)(((int)(object)type & ~(int)(object)value));
            }
            catch (Exception ex)
            {
                throw new ArgumentException(
                    string.Format(
                        "Could not remove value from enumerated type '{0}'.",
                        typeof(T).Name
                        ), ex);
            }
		}
		public static void DisposeAll(this System.Collections.IEnumerable set)
		{
			foreach (object current in set)
			{
				System.IDisposable disposable = current as System.IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
		}
	}
}
