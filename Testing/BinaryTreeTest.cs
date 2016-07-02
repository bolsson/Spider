using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NUnit.Framework;
using Spider;

namespace Testing
{
    [TestFixture]
    public class BinaryTreeTest
    {
        private Spider.BinaryTreeImp binaryTree;

        [SetUp]
        public void Init()
        {
            binaryTree = new BinaryTreeImp();
        }

        [Test]
        public void TestAddNode()
        {
            Node and1 = new Node(new AndToken());
            Assert.IsInstanceOf(typeof(Node), and1);
            Assert.IsNotNull(and1);
        }

        [Test]
        public void TestCountNodes()
        {
            Node no1 = new Node(new WordToken("1"));
            Node no2 = new Node(new WordToken("2"));
            Node no3 = new Node(new WordToken("3"));
            Node no4 = new Node(new WordToken("4"));
            Node no5 = new Node(new WordToken("5"));
            Node no6 = new Node(new WordToken("6"));
            binaryTree.insertNode(no1, no2);
            binaryTree.insertNode(binaryTree.root, no3);
            binaryTree.insertNode(binaryTree.root, no4);
            binaryTree.insertNode(binaryTree.root, no5);
            binaryTree.insertNode(binaryTree.root, no6);

            List<Token> tokenList = new List<Token>();
            binaryTree.convertTreeToList(binaryTree.root, ref tokenList);
            Assert.AreEqual(6, tokenList.Count);
            Assert.AreNotEqual(7, tokenList.Count);

            binaryTree = null;
            Assert.IsNull(binaryTree);

            binaryTree = new BinaryTreeImp();
            Assert.AreEqual(0, binaryTree.count);
        }

        [Test]
        public void TestContainsNodes()
        {
            Node no1 = new Node(new WordToken("1"));
            Node no2 = new Node(new WordToken("2"));
            Node no3 = new Node(new WordToken("3"));
            Node no4 = new Node(new WordToken("4"));
            Node no5 = new Node(new WordToken("5"));
            Node no6 = new Node(new WordToken("6"));
            Node no7 = new Node(new WordToken("7"));
            binaryTree.insertNode(no1, no2);
            binaryTree.insertNode(binaryTree.root, no3);
            binaryTree.insertNode(binaryTree.root, no4);
            binaryTree.insertNode(binaryTree.root, no5);
            binaryTree.insertNode(binaryTree.root, no6);

            List<Token> tokenList = new List<Token>();
            binaryTree.convertTreeToList(binaryTree.root, ref tokenList);
            Assert.Contains(no1.token, tokenList);
            Assert.Contains(no2.token, tokenList);
            Assert.Contains(no3.token, tokenList);
            Assert.Contains(no4.token, tokenList);
            Assert.Contains(no5.token, tokenList);
            Assert.Contains(no6.token, tokenList);      
        }

        [Test]
        public void testRoot()
        {
            Node no1 = new Node(new WordToken("1"));
            Node no2 = new Node(new WordToken("2"));
            Node no3 = new Node(new WordToken("3"));
            Node no4 = new Node(new WordToken("4"));
            Node no5 = new Node(new WordToken("5"));
            Node no6 = new Node(new WordToken("6"));
            Node no7 = new Node(new WordToken("7"));
            binaryTree.insertNode(no3, no2);
            binaryTree.insertNode(binaryTree.root, no1);
            binaryTree.insertNode(binaryTree.root, no4);
            binaryTree.insertNode(binaryTree.root, no5);
            binaryTree.insertNode(binaryTree.root, no6);

            Assert.AreSame(no3, binaryTree.root);
            Assert.AreNotSame(no1, binaryTree.root);
        }

        [Test]
        public void TestTraverseOrder()
        {
            Node no1 = new Node(new WordToken("1"));
            Node no2 = new Node(new WordToken("2"));
            Node no3 = new Node(new WordToken("3"));
            Node no4 = new Node(new WordToken("4"));
            Node no5 = new Node(new WordToken("5"));
            Node no6 = new Node(new WordToken("6"));
            Node no7 = new Node(new WordToken("7"));
            binaryTree.insertNode(no3, no2);
            binaryTree.insertNode(binaryTree.root, no1);
            binaryTree.insertNode(binaryTree.root, no4);
            binaryTree.insertNode(binaryTree.root, no5);
            binaryTree.insertNode(binaryTree.root, no6);

            var tokenCheckList = new List<Token>()
            {
                no3.token,
                no2.token,
                no1.token,
                no4.token,
                no5.token,
                no6.token
            };

            var tokenNegativeCheckList = new List<Token>()
            {
                no1.token,
                no2.token,
                no3.token,
                no4.token,
                no5.token,
                no6.token
            };

            List<Token> tokenList = new List<Token>();
            binaryTree.convertTreeToList(binaryTree.root, ref tokenList);

            Assert.AreEqual(tokenCheckList, tokenList);
            Assert.AreNotEqual(tokenNegativeCheckList, tokenList);
        }

        private Node createTree()
        {
            Node root = new Node(new AndToken());

            return root;
        }


    }
}