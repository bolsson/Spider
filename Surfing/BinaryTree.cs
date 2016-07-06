using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spider
{

    public class Node
    {
        public Token token;
        public Node left, right;

        public Node(Token token)
        {
            this.token = token;
            left = null;
            right = null;
        }
    }

    public class BinaryTreeImp
    {
        private Node _root;
        private int _count = 0;

        public Node root
        {
            get { return _root; }
            set
            {
                _root = value;
                _count = noOfNodes();
            }
        }

        public int count
        {
            get { return noOfNodes(); }
        }

        public BinaryTreeImp()
        {
            _root = null;
        }

        public void insertNode(Node root, Node newLeafNode)
        {
            Node temp;
            temp = root;
            if (_count == 0) this.root = temp;

            if (temp.left == null)
            {
                temp.left = newLeafNode;
                _count++;
            }
            else if (temp.right == null)
            {
                temp.right = newLeafNode;
                _count++;
            }
            else
                insertNode(temp.right, newLeafNode); //NOTE: should not happen as we build trees with two balances leafs only
        }


        public void convertTreeToList(Node root, ref List<Token> nodeList)
        {
            var temp = root;
            //List<Token> nodeList = new List<Token>();
            if (root == null) return;
            nodeList.Add(temp.token);

            convertTreeToList(temp.right, ref nodeList);
            //System.Console.Write(root.token + " ");

            convertTreeToList(temp.left, ref nodeList);
        }

        public void displayTree(Node root)
        {
            if (root == null) return;

            displayTree(root.right);
            System.Console.Write(root.token + " ");
            displayTree(root.left);
        }

        public int noOfNodes()
        {
            _count = 0;
            return countNodes(root);
        }
        private int countNodes(Node root)
        {
            if (root == null) return _count;

            _count = countNodes(root.right);

            _count = countNodes(root.left);
            _count++;
            return _count;
        }
    }
}
