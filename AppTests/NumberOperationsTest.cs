using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppInterface.Algorithms;

namespace AppTests
{
    [TestClass]
    public class NumberOperationsTest
    {

        private NumberOperations numbersExtension;

        public NumberOperationsTest()
        {
            this.numbersExtension = new NumberOperations();
        }

        [TestMethod]
        public void ExpressionParserTest()
        {
            Assert.AreEqual(numbersExtension.ExpressionParser("(2 + 2) * (2 + 2)"), 16);
            Assert.AreEqual(numbersExtension.ExpressionParser("(64416 / 61)"), 1056);
            Assert.AreEqual(numbersExtension.ExpressionParser("(1169 + 89)"), 1258);
            Assert.AreEqual(numbersExtension.ExpressionParser("(19 * 2)"), 38);
            Assert.AreEqual(numbersExtension.ExpressionParser("(34 * 1)"), 34);
            Assert.AreEqual(numbersExtension.ExpressionParser("(-58 + 96)"), 38);
            Assert.AreEqual(numbersExtension.ExpressionParser("((1169 + 89) - (19 * 2))"), 1220);
            Assert.AreEqual(numbersExtension.ExpressionParser("(((1169 + 89) - (19 * 2)) + (34 * 1))"), 1254);
            Assert.AreEqual(numbersExtension.ExpressionParser("((64416 / 61) * (1254 * (-58 + 96)))"), 50320512);
            Assert.AreEqual(numbersExtension.ExpressionParser("((64416 / 61) * ((((1169 + 89) - (19 * 2)) + (34 * 1)) * (-58 + 96)))"), 50320512);
        }

        [TestMethod]
        public void UnwrapNumberTest()
        {
            Assert.AreEqual(numbersExtension.ExpressionParser(numbersExtension.UnwrapNumber("16")), 16);
            Assert.AreEqual(numbersExtension.ExpressionParser(numbersExtension.UnwrapNumber("1056")), 1056);
            Assert.AreEqual(numbersExtension.ExpressionParser(numbersExtension.UnwrapNumber("1258")), 1258);
            Assert.AreEqual(numbersExtension.ExpressionParser(numbersExtension.UnwrapNumber("34")), 34);
            Assert.AreEqual(numbersExtension.ExpressionParser(numbersExtension.UnwrapNumber("38")), 38);
            Assert.AreEqual(numbersExtension.ExpressionParser(numbersExtension.UnwrapNumber("1254")), 1254);
            Assert.AreEqual(numbersExtension.ExpressionParser(numbersExtension.UnwrapNumber("50320512")), 50320512);
        }
    }
}
