using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NUnit.Framework;
using Spider;

namespace Testing
{
    [TestFixture]
    public class TokenizerTest
    {
        private Tokens _tokens;
        private Lexer _Tokenizer;
        [SetUp]
        public void Init()
        {
            //string queryExpression = "(\"good men\" OR (evil OR mean)) AND (man AND woman)";
            //_Tokenizer = new Lexer(queryExpression);
        }

        [Test]
        public void testTokens()
        {
            string queryExpression = "(\"good men\" OR (evil OR mean)) AND (man AND woman)";
            _Tokenizer = new Lexer(queryExpression);
            _Tokenizer.Tokenize();
            _tokens = _Tokenizer.tokens;
            Assert.AreEqual(_tokens.getNextToken().ToString(), "(");
            Assert.AreEqual(((WordToken)_tokens.getNextToken()).ToString(), "good men");
            Assert.AreEqual(_tokens.getNextToken().ToString(), "OR");

        }
    }
}