using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MatasanoCrypto;

namespace MatasanoCrypto
{
    [TestClass]
    public class CryptoTests
    {
        [TestMethod]
        public void TestHexToBase64()
        {
            String hex = "49276d206b696c6c696e6720796f757220627261696e206c696b65206120706f69736f6e6f7573206d757368726f6f6d";
            Assert.AreEqual("SSdtIGtpbGxpbmcgeW91ciBicmFpbiBsaWtlIGEgcG9pc29ub3VzIG11c2hyb29t", Basics.HexToBase64(hex));
        }

        [TestMethod]
        public void TestXOR()
        {
            String stringA = "1c0111001f010100061a024b53535009181c";
            String stringB = "686974207468652062756c6c277320657965";
            Assert.AreEqual("746865206b696420646f6e277420706c6179", Basics.XOR(stringA, stringB));
        }
    }
}
