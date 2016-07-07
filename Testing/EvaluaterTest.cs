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
            string word1 = "word1";
            string word2 = "word2";
            string word3 = "word3";
            string word4 = "word4";
            string word5 = "word5";
            string word6 = "word6";
            //Note fill testdata  into the invertedindex -- in progress
            InvertedIndex index = new InvertedIndex();
            index.Add(word1, new List<string>() { "doc1", "doc2", "doc3", "doc4" });
            index.Add(word2, new List<string>() { "doc2", "doc4", "doc6", "doc8" });
            index.Add(word3, new List<string>() { "doc3", "doc6", "doc9", "doc12" });
            index.Add(word4, new List<string>() { "doc4", "doc8", "doc12", "doc16" });
            index.Add(word5, new List<string>() { "doc5", "doc10", "doc15", "doc20" });
            index.Add(word6, new List<string>() { "doc6", "doc12", "doc18", "doc24" });

            Tokens tokens = new Tokens();
            tokens.AddToken(new WordToken(word1));
            tokens.AddToken(new AndToken());
            tokens.AddToken(new WordToken(word2));

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