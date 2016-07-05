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


            _parser = new Parser(tokens);
            Node treeRoot = _parser.BinaryTree.root;

            Assert.AreEqual(1, _parser.BinaryTree.countNodes(_parser.BinaryTree.root));
        }
    }
}