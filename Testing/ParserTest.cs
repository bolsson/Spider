using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NUnit.Framework;
using Spider;

namespace Testing
{
    [TestFixture]
    public class ParserTest
    {
        private Parser _parser;
        [SetUp]
        public void Init()
        {
            //Tokens tokens = new Tokens();
            //tokens.AddToken(new ParenthesisBeginToken());
            //tokens.AddToken(new WordToken("good men"));


            //_parser = new Parser(tokens);
            //Node treeRoot = _parser.BinaryTree.root;
        }

        [Test]
        public void TestParserBuildsTree()
        {
            Tokens tokens = new Tokens();
            tokens.AddToken(new ParenthesisBeginToken());
            tokens.AddToken(new WordToken("good men"));
            tokens.AddToken(new OrToken());
            tokens.AddToken(new ParenthesisBeginToken());
            tokens.AddToken(new WordToken("evil"));
            tokens.AddToken(new OrToken());
            tokens.AddToken(new WordToken("bad"));
            tokens.AddToken(new ParenthesisEndToken());
            tokens.AddToken(new ParenthesisEndToken());
            tokens.AddToken(new AndToken());
            tokens.AddToken(new ParenthesisBeginToken());
            tokens.AddToken(new WordToken("women"));
            tokens.AddToken(new AndToken());
            tokens.AddToken(new WordToken("children"));
            tokens.AddToken(new ParenthesisEndToken());


            _parser = new Parser(tokens);
            Node treeRoot = _parser.BinaryTree.root;
            Assert.AreEqual(9, _parser.BinaryTree.count);
        }

        [Test]
        public void TestInsertSingleWordOnly()
        {
            Tokens tokens = new Tokens();
            tokens.AddToken(new WordToken("good men"));

            _parser = new Parser(tokens);
            Node treeRoot = _parser.BinaryTree.root;
            Assert.AreEqual(1, _parser.BinaryTree.count);
        }

        [Test]
        public void TestInsertWordsOnlyNoLogic()
        {
            Tokens tokens = new Tokens();
            tokens.AddToken(new WordToken("w1"));
            tokens.AddToken(new WordToken("w2"));
            tokens.AddToken(new WordToken("w3"));
            tokens.AddToken(new WordToken("w4"));
            tokens.AddToken(new WordToken("w5"));
            tokens.AddToken(new WordToken("w6"));
            tokens.AddToken(new WordToken("w7"));

            _parser = new Parser(tokens);
            Node treeRoot = _parser.BinaryTree.root;
            Assert.AreEqual(13, _parser.BinaryTree.count);
        }

        //[Test]
        //public void TestInsertWordMixedAndLogic()
        //{
        //    Tokens tokens = new Tokens();
        //    tokens.AddToken(new WordToken("w1"));
        //    tokens.AddToken(new WordToken("w2"));
        //    tokens.AddToken(new WordToken("w3"));
        //    tokens.AddToken(new WordToken("w4"));
        //    tokens.AddToken(new WordToken("w5"));
        //    tokens.AddToken(new WordToken("w6"));
        //    tokens.AddToken(new WordToken("w7"));

        //    _parser = new Parser(tokens);
        //    Node treeRoot = _parser.BinaryTree.root;
        //    Assert.AreEqual(13, _parser.BinaryTree.count);
        //}
    }
}