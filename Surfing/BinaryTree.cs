﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spider
{

    class Node
    {
        public string token;
        public Node left, right;

        public Node(string token)
        {
            this.token = token;
            left = null;
            right = null;
        }
    }

    class BinaryTreeImp
    {
        Node root;
        static int count = 0;

        public BinaryTreeImp()
        {
            root = null;
        }
        public Node addNode(string token)
        {
            Node newNode = new Node(token);

            if (root == null)
            {
                root = newNode;

            }
            count++;
            return newNode;


        }

        public void insertNode(Node root, Node newNode)
        {
            Node temp;
            temp = root;

            if (String.Compare(newNode.token, temp.token) < 0)
            {
                if (temp.left == null)
                {
                    temp.left = newNode;
                }
                else
                {
                    insertNode(temp.left, newNode);
                }
            }
            else if (String.Compare(newNode.token, temp.token) > 0)
            {
                if (temp.right == null)
                {
                    temp.right = newNode;
                }
                else
                {
                    insertNode(temp.right, newNode);
                }
            }
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
    }
}
