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
        }

        [Test]
        public void TestVerifyTree()
        {

        }  

        private Node createTree()
        {
            Node root = new Node(new AndToken());

            return root;
        }


    }
}