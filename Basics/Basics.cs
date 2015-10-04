using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatasanoCrypto
{
    public static class Basics
    {
        public static void Main(string[] args)
        {

        }

        public static String HexToBase64(String hexstring)
        {
            return Convert.ToBase64String(StringToByteArray(hexstring));
        }

        public static String XOR (String stringA, String stringB)
        {
            byte[] byteStringA = StringToByteArray(stringA);
            byte[] byteStringB = StringToByteArray(stringB);
            byte[] xorred = new byte[byteStringA.Length];

            for (int x = 0; x < byteStringA.Length; x++)
            {
                xorred[x] = (byte)(byteStringA[x] ^ byteStringB[x]);
            }

            return ByteArrayToString(xorred);
        }


        public static byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                     .Where(x => x % 2 == 0)
                     .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                     .ToArray();
        }

        public static string ByteArrayToString(byte[] ba)
        {
            string hex = BitConverter.ToString(ba);
            return hex.Replace("-", "").ToLower();
        }
    }
}
