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
        //public Node addNode(Token token)
        //{
        //    Node newNode = new Node(token);

        //    if (root == null)
        //    {
        //        root = newNode;

        //    }
        //    count++;
        //    return newNode;
        //}


        public void insertNode(Node root,Node newLeafNode)
        {
            Node temp;
            temp = root;
            if (count == 0) this.root = temp;

            
            if (String.Compare(temp.token.ToString(), newLeafNode.token.ToString()) < 0)
            {
                if (temp.left == null)
                {
                    temp.left = newLeafNode;
                    count++;
                }
                else
                {
                    insertNode(temp.left, newLeafNode);
                }
            }
            else if (string.Compare(temp.token.ToString(), newLeafNode.token.ToString()) >= 0)
            {
                if (temp.right == null)
                {
                    temp.right = newLeafNode;
                    count++;
                }
                else
                {
                    insertNode(temp.right, newLeafNode);
                }
            }
        }

        //public void insertRootNode(Node root, Node newRootNode)
        //{
        //    //Make sure the new root is a logictoken or return
        //    if (newRootNode.GetType() != typeof(LogicToken))
        //        return;

        //    if (string.Compare(root.token.ToString(), newRootNode.token.ToString()) < 0)
        //    {
        //        newRootNode.right = root;
        //    }
        //    else if (string.Compare(root.token.ToString(), newRootNode.token.ToString()) >= 0) //0 or 1
        //    {
        //        newRootNode.left = root;
        //    }
        //    this.root = root;
        //}

        //public void insertBranchNode(Node root, Node newBranchNode)
        //{
        //    Node temp;
        //    temp = root;

        //    if (String.Compare(temp.token.ToString(), newBranchNode.token.ToString()) < 0)
        //    {
        //        if (temp.left == null)
        //        {
        //            temp.left = newBranchNode;
        //            newBranchNode.left = temp.right;
        //            temp.right = null;
        //        }
        //        else
        //        {
        //            insertBranchNode(temp.left, newBranchNode);
        //        }
        //    }
        //    else if (String.Compare(temp.token.ToString(), newBranchNode.token.ToString()) > 0)
        //    {
        //        if (temp.right == null)
        //        {
        //            temp.right = newBranchNode;
        //            newBranchNode.right = temp.left;
        //            temp.left = null;
        //        }
        //        else
        //        {
        //            insertBranchNode(temp.right, newBranchNode);
        //        }
        //    }
        //}
        public void convertTreeToList(Node root, ref List<Token> nodeList)
        {
            var temp = root;
            //List<Token> nodeList = new List<Token>();
            if (root == null) return;

            convertTreeToList(temp.left, ref nodeList);
            //System.Console.Write(root.token + " ");
            nodeList.Add(temp.token);
            convertTreeToList(temp.right, ref nodeList);
        }

        public void displayTree(Node root)
        {
            if (root == null) return;

            displayTree(root.left);
            System.Console.Write(root.token + " ");
            displayTree(root.right);
        }



        //static void Main(string[] args)
        //{
        //    BinaryTreeImp btObj = new BinaryTreeImp();
        //    Node iniRoot = btObj.addNode(5);


        //    btObj.insertNode(btObj.root, iniRoot);
        //    btObj.insertNode(btObj.root, btObj.addNode(6));
        //    btObj.insertNode(btObj.root, btObj.addNode(10));
        //    btObj.insertNode(btObj.root, btObj.addNode(2));
        //    btObj.insertNode(btObj.root, btObj.addNode(3));
        //    btObj.displayTree(btObj.root);

        //    System.Console.WriteLine("The sum of nodes are " + count);
        //    Console.ReadLine();

        //}

        //public void insertNode(Node root, Node newNode)
        //{
        //    Node temp;
        //    temp = root;

        //    if (String.Compare(newNode.token.ToString(), temp.token.ToString()) < 0)
        //    {
        //        if (temp.left == null)
        //        {
        //            temp.left = newNode;
        //        }
        //        else
        //        {
        //            insertNode(temp.left, newNode);
        //        }
        //    }
        //    else if (String.Compare(newNode.token.ToString(), temp.token.ToString()) > 0)
        //    {
        //        if (temp.right == null)
        //        {
        //            temp.right = newNode;
        //        }
        //        else
        //        {
        //            insertNode(temp.right, newNode);
        //        }
        //    }
        //}
    }
}
