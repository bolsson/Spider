using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NUnit.Framework;
using Spider;

namespace Testing
{
    [TestFixture]
    public class EvaluaterTest
    {
        [SetUp]
        public void Init()
        {
            //Note fill testdata  into the invertedindex -- in progress
            InvertedIndex index = new InvertedIndex();
            index.Add("", new List<string>() { "", "" });

            Tokens tokens = new Tokens();
            tokens.AddToken(new WordToken("hello world"));

            Parser parse = new Parser(tokens);
            Node root = parse.BinaryTree.root;

            Evaluater eval = new Evaluater(root, index);
            var res = eval.result;
        }

        [Test]
        public void Test()
        {

        }
    }
}