using System;
namespace iSpyApplication.Video
{
	internal static class ByteArrayUtils
	{



        public static unsafe bool UnsafeCompare(byte[] a1, byte[] a2)
        {
            if (a1 == null || a2 == null || a1.Length != a2.Length)
                return false;
            fixed (byte* p1 = a1, p2 = a2)
            {
                byte* x1 = p1, x2 = p2;
                int l = a1.Length;
                for (int i = 0; i < l / 8; i++, x1 += 8, x2 += 8)
                    if (*((long*)x1) != *((long*)x2)) return false;
                if ((l & 4) != 0) { if (*((int*)x1) != *((int*)x2)) return false; x1 += 4; x2 += 4; }
                if ((l & 2) != 0) { if (*((short*)x1) != *((short*)x2)) return false; x1 += 2; x2 += 2; }
                if ((l & 1) != 0) if (*((byte*)x1) != *((byte*)x2)) return false;
                return true;
            }
        }
        /*
		public unsafe static bool UnsafeCompare(byte[] a1, byte[] a2)
		{
			if (a1 == null || a2 == null || a1.Length != a2.Length)
			{
				return false;
			}
			fixed (byte* ptr = a1)
			{
				fixed (byte* ptr2 = a2)
				{
					byte* ptr3 = ptr;
					byte* ptr4 = ptr2;
					int num = a1.Length;
					int i = 0;
					bool result;
					while (i < num / 8)
					{
						if (*(long*)ptr3 != *(long*)ptr4)
						{
							result = false;
							return result;
						}
						i++;
						ptr3 += (System.IntPtr)8 / 1;
						ptr4 += (System.IntPtr)8 / 1;
					}
					if ((num & 4) != 0)
					{
						if (*(int*)ptr3 != *(int*)ptr4)
						{
							result = false;
							return result;
						}
						ptr3 += (System.IntPtr)4 / 1;
						ptr4 += (System.IntPtr)4 / 1;
					}
					if ((num & 2) != 0)
					{
						if (*(short*)ptr3 != *(short*)ptr4)
						{
							result = false;
							return result;
						}
						ptr3 += (System.IntPtr)2 / 1;
						ptr4 += (System.IntPtr)2 / 1;
					}
					if ((num & 1) != 0 && *ptr3 != *ptr4)
					{
						result = false;
						return result;
					}
					result = true;
					return result;
				}
			}
		}//*/
		public static bool Compare(byte[] array, byte[] needle, int startIndex)
		{
			int num = needle.Length;
			int i = 0;
			int num2 = startIndex;
			while (i < num)
			{
				if (array[num2] != needle[i])
				{
					return false;
				}
				i++;
				num2++;
			}
			return true;
		}
		public static int Find(byte[] array, byte[] needle, int startIndex, int sourceLength)
		{
			int num = needle.Length;
			while (sourceLength >= num)
			{
				int num2 = System.Array.IndexOf<byte>(array, needle[0], startIndex, sourceLength - num + 1);
				if (num2 == -1)
				{
					return -1;
				}
				int num3 = 0;
				int num4 = num2;
				while (num3 < num && array[num4] == needle[num3])
				{
					num3++;
					num4++;
				}
				if (num3 == num)
				{
					return num2;
				}
				sourceLength -= num2 - startIndex + 1;
				startIndex = num2 + 1;
			}
			return -1;
		}
	}
}
