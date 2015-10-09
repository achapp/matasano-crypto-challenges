using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatasanoCrypto
{
    public static class Basics
    {
        private static Dictionary<char, double> ENGLISH_LETTER_FREQUENCIES = new Dictionary<char,double>{
                                                                       {'E', 12.02}, {'T', 9.10}, {'A', 8.12}, {'O', 7.68}, {'I', 7.31}, {'N', 6.95}, {'S', 6.28}, {'R', 6.32}, {'H', 5.92},
                                                                       {'D', 4.32}, {'L', 3.98}, {'U', 2.88}, {'C', 2.71}, {'M', 2.61}, {'F', 2.30}, {'Y', 2.11}, {'W', 2.09}, {'G', 2.03},
                                                                       {'P', 1.82}, {'B', 1.49}, {'V', 1.11}, {'K', 0.69}, {'X', 0.17}, {'Q', 0.11}, {'J', 0.10}, {'Z', 0.07}
                                                                   };
        public static void Main(string[] args)
        {
            Console.WriteLine(DetectSingleByteXORCipher("1b37373331363f78151b7f2b783431333d78397828372d363c78373e783a393b3736"));
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

        private static String DetectSingleByteXORCipher(string encyptedHex)
        {
            byte[] encryptedBytes = StringToByteArray(encyptedHex);
            double hiScore = Double.MaxValue;
            int presumedKey = 0;
            String presumedUnencrypted = String.Empty;

            //try each possible character as the key
            for (int k = 0; k < 255; k++ )
            {
                byte[] xorred = new byte[encryptedBytes.Length];
                for (int x = 0; x < encryptedBytes.Length; x++)
                {
                    xorred[x] = (byte)(encryptedBytes[x] ^ k);
                }

                double score = XORScore(xorred);
                if(score < hiScore)
                {
                    hiScore = score;
                    presumedKey = k;
                    presumedUnencrypted = ByteArrayToString(xorred);

                }
            }
            return presumedUnencrypted;
        }

        private static double XORScore(byte[] xorredResult)
        {
            Dictionary<char, int> letterFrequencies = new Dictionary<char, int>();
            int numLetters = 0;
            double score = 0;
            
            //count occurences of each letter
            foreach(char c in xorredResult)
            {
                if ((c>='A' && c<='Z') || (c>='a' && c<='z'))
                {
                    char letter = char.ToUpper(c);

                    numLetters++;
                    if (letterFrequencies.ContainsKey(letter))
                    {
                        letterFrequencies[letter] = letterFrequencies[letter] + 1;
                    }
                    else
                    {
                        letterFrequencies.Add(letter, 1);
                    }
                }
            }

            foreach(KeyValuePair<char, double> f in ENGLISH_LETTER_FREQUENCIES)
            {
                double test = 0;
                if (letterFrequencies.ContainsKey(f.Key))
                {
                    test = letterFrequencies[f.Key];
                }
                
                score += Math.Abs(test - f.Value);
            }
            return score;
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
