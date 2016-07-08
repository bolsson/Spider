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
        string doc1 = "doc1";
        string doc2 = "doc2";
        string doc3 = "doc3";
        string doc4 = "doc4";
        string doc5 = "doc5";
        string doc6 = "doc6";
        InvertedIndex index;
        [SetUp]
        public void Init()
        {

            //Note fill testdata  into the invertedindex -- in progress
            index = new InvertedIndex();
            index.Add(doc1, new List<string>() { "word1", "word2", "word3", "word4" });
            index.Add(doc2, new List<string>() { "word2", "word4", "word6", "word8" });
            index.Add(doc3, new List<string>() { "word3", "word6", "word9", "word12" });
            index.Add(doc4, new List<string>() { "word4", "word8", "word12", "word16" });
            index.Add(doc5, new List<string>() { "word5", "word10", "word15", "word20" });
            index.Add(doc6, new List<string>() { "word6", "word12", "word18", "word24" });
        }

        [Test]
        public void TestOr()
        {
            Tokens tokens = new Tokens();
            tokens.AddToken(new WordToken("word1"));
            tokens.AddToken(new WordToken("word2"));
            tokens.AddToken(new WordToken("word4"));
            tokens.AddToken(new WordToken("word6"));
            tokens.AddToken(new WordToken("word8"));

            Parser parse = new Parser(tokens);
            Node root = parse.BinaryTree.root;

            Evaluater eval = new Evaluater(root, index);
            List<string> res = eval.result.ToList<string>();
            Assert.IsNotNull(res);
            Assert.Contains("doc1", res);
            Assert.Contains("doc2", res);
            Assert.Contains("doc3", res);
            Assert.Contains("doc4", res);
            Assert.Contains("doc6", res);
            Assert.AreEqual(5, res.Count);
            Assert.AreNotEqual(2, res.Count);
        }

        [Test]
        public void TestAnd()
        {
            Tokens tokens = new Tokens();
            tokens.AddToken(new ParenthesisBeginToken());
            tokens.AddToken(new WordToken("word6"));
            tokens.AddToken(new AndToken());
            tokens.AddToken(new WordToken("word12"));
            tokens.AddToken(new ParenthesisEndToken());

            Parser parse = new Parser(tokens);
            Node root = parse.BinaryTree.root;

            Evaluater eval = new Evaluater(root, index);
            List<string> res = eval.result.ToList<string>();
            Assert.IsNotNull(res);
            Assert.Contains("doc6", res);
            Assert.Contains("doc3", res);
            Assert.AreEqual(2, res.Count);
        }

        [Test]
        public void TestAndNot()
        {
            Tokens tokens = new Tokens();
            tokens.AddToken(new ParenthesisBeginToken());
            tokens.AddToken(new WordToken("word6"));
            tokens.AddToken(new AndNotToken());
            tokens.AddToken(new WordToken("word12"));
            tokens.AddToken(new ParenthesisEndToken());

            Parser parse = new Parser(tokens);
            Node root = parse.BinaryTree.root;

            Evaluater eval = new Evaluater(root, index);
            List<string> res = eval.result.ToList<string>();
            Assert.IsNotNull(res);
            Assert.Contains("doc2", res);
            Assert.AreEqual(1, res.Count);
        }

        [Test]
        public void TestComplexSearchExpression()
        {
            Tokens tokens = new Tokens();
            tokens.AddToken(new ParenthesisBeginToken());
            tokens.AddToken(new WordToken("word3"));
            tokens.AddToken(new OrToken());
            tokens.AddToken(new WordToken("word10"));
            tokens.AddToken(new OrToken());
            tokens.AddToken(new WordToken("word9"));
            tokens.AddToken(new ParenthesisEndToken());

            tokens.AddToken(new AndToken());

            tokens.AddToken(new ParenthesisBeginToken());
            tokens.AddToken(new WordToken("word2"));
            tokens.AddToken(new OrToken());
            tokens.AddToken(new WordToken("word6"));
            tokens.AddToken(new ParenthesisEndToken());

            tokens.AddToken(new AndNotToken());

            tokens.AddToken(new ParenthesisBeginToken());
            tokens.AddToken(new WordToken("word12"));
            tokens.AddToken(new ParenthesisEndToken());

            Parser parse = new Parser(tokens);
            Node root = parse.BinaryTree.root;

            Evaluater eval = new Evaluater(root, index);
            List<string> res = eval.result.ToList<string>();
            Assert.IsNotNull(res);
            Assert.Contains("doc1", res);
            Assert.AreEqual(1, res.Count);
        }
    }
}