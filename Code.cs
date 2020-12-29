using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVGA_GpuInfo
{
	internal class Utility
	{

		//EVGA encryption algo
		public static void Encrypt(ref byte[] buffer, string password)
		{
			//default password if none given
			string text = "eNcRyPt";
			if (password.Length > 0)
			{
				text = password;
			}
            int length = text.Length;
			//loops though each byte.
			for (int i = 0; i < buffer.Length; i++)
			{

                //constrains the index to the max length of password.
                int index = i % length;

				//sets up byte using password char at index as seed then + array position
				byte b = (byte)((int)text[index] + i);
				byte[] array = buffer;
                int num = i;

				//xor
				array[num] ^= b;
			}
		}

		public static string widebytearray2string(byte[] inputdata)
		{
			//Basically just removes every 2nd byte thats 00
			string workingstr = "";
			foreach (byte B in inputdata)
			{
				if (B > 31 && B < 127 || B == 0x0d || B == 0x0a || B == 0x09)
					workingstr += (char)B;
			}
			return workingstr;
		}

		public static byte[] narrowstring2widearray(string inputdata)
		{
			byte[] workingarray = new byte[(inputdata.Length * 2) + 2];
			int si = 0;

			//EVGA Header and Footer
			workingarray[0] = 0xff;
			workingarray[1] = 0xfe;
			workingarray[workingarray.Length - 1] = 0;
            workingarray[workingarray.Length - 2] = 0x0A;

			//Change every 2nd byte to 0
			for (int i = 2; i < workingarray.Length; i++)
			{
				int index = i % 2;
				if (index == 1)
				{
					workingarray[i] = 0;
				}
				else
				{
					workingarray[i] = (byte)((int)inputdata[si++]);

				}
			}
			return workingarray;
		}
	}
}
