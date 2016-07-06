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

        }
    }
}