using System.Text.RegularExpressions;
using Leaves.Lexer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Leaves.Test
{
    [TestClass]
    public class InputReaderTest
    {
        [TestMethod]
        public void Get_10000_Input_123()
        {
            var inputReader = new InputReader("123");
            Assert.AreEqual(inputReader.Get(100000), '\0');
        }
        
        [TestMethod]
        public void Get_0_Input_123()
        {
            var inputReader = new InputReader("123");
            Assert.AreEqual(inputReader.Get(), '1');
        }
        
        [TestMethod]
        public void Get_0_Input_абв()
        {
            var inputReader = new InputReader("абв");
            Assert.AreEqual(inputReader.Get(), 'а');
        }
        
        [TestMethod]
        public void Read_3times_Input_абв()
        {
            var inputString = "абв";
            var inputReader = new InputReader(inputString);

            for (int i = 0; i < inputString.Length; i++)
            {
                Assert.AreEqual(inputReader.Read(), inputString[i]);
            }
        }
        
        [TestMethod]
        public void TryRegex_Identifier_Input_123123()
        {
            var identifierRegex = new Regex(@"[A-Za-z_$][\w$]*");
            var inputString = "123123";
            var inputReader = new InputReader(inputString);

            Assert.AreEqual(inputReader.TryRegex(identifierRegex, out var result), false);
            Assert.IsNull(result);
        }
        
        [TestMethod]
        public void TryRegex_Identifier_Input_cash()
        {
            var identifierRegex = new Regex(@"[A-Za-z_$][\w$]*");
            var inputString = "$$$__CaSh_AnD_PhP__$$$";
            var inputReader = new InputReader(inputString);

            Assert.AreEqual(inputReader.TryRegex(identifierRegex, out var result), true);
            Assert.AreEqual(result, "$$$__CaSh_AnD_PhP__$$$");
        }
        
        [TestMethod]
        public void Skip_0_Input_абв()
        {
            var inputReader = new InputReader("абв");
            inputReader.Skip(0);
            Assert.AreEqual(inputReader.Get(), 'а');
        }
        
        [TestMethod]
        public void Skip_2_Input_абв()
        {
            var inputReader = new InputReader("абв");
            inputReader.Skip(2);
            Assert.AreEqual(inputReader.Get(), 'в');
        }
    }
}