using System.Collections.Generic;
using Leaves.Lexer;
using Leaves.Lexer.Exception;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Leaves.Test
{
    [TestClass]
    public class LexerAnalyzerTest
    {
        [TestMethod]
        public void GetLexems_Two_Identifiers()
        {
            var input = new InputReader("      a123\tcda    \t\t\t\n\n\n   ");
            var expected = new List<Lexem> { new Lexem("IDENTIFIER", "a123"), new Lexem("IDENTIFIER", "cda") };
            var analyzer = new LexerAnalyzer(input);

            var result = analyzer.GetLexems();
            
            CollectionAssert.AreEqual(result, expected);
        }
        
        [TestMethod]
        public void GetLexems_Incorrect_Negative()
        {
            var input = new InputReader("  \t\t\t\n\n\n      a123\t1cda     ");
            var analyzer = new LexerAnalyzer(input);

            Assert.ThrowsException<LexerException>(analyzer.GetLexems);
        }
    }
}