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
        public Node root;
        public int count = 0;

        public BinaryTreeImp()
        {
            root = null;
        }

        public void insertNode(Node root, Node newLeafNode)
        {
            Node temp;
            temp = root;
            if (countNodes(this.root) == 0) this.root = temp;

            
            if (String.Compare(temp.token.ToString(), newLeafNode.token.ToString()) < 0)
                if (temp.left == null)
                    temp.left = newLeafNode;
                else
                    insertNode(temp.left, newLeafNode);
            else if (string.Compare(temp.token.ToString(), newLeafNode.token.ToString()) >= 0)
                if (temp.right == null)
                    temp.right = newLeafNode;
                else
                    insertNode(temp.right, newLeafNode);
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

        public int countNodes(Node root)
        {
            if (root == null) return count;
            
            count = countNodes(root.right);
            
            count = countNodes(root.left);
            count++;
            return count; 
        }
    }
}
