using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spider
{
    class Evaluater
    {
        private Queue<Token> queue;
        private Stack<Token> stack;

        public Evaluater(Node root)
        {
            queue = new Queue<Token>();
            stack = new Stack<Token>();
            evaluaterOrder(root);
        }
        private void evaluaterOrder(Node root)
        {
            if (root == null) return;

            evaluaterOrder(root.left);
            evaluaterOrder(root.right);
            //when both left and right branch has been visited for a node
            queue.Enqueue(root.token);
        }

        public void evaluate(InvertedIndex index)
        {
            
            foreach (Token token in queue)
            {
                if (token.GetType() == typeof(WordToken))
                {
                    stack.Push(token);
                }
                if (token.GetType() == typeof(LogicToken))
                {
                    if (stack.Count >= 2) {
                        if (token.GetType() == typeof(AndToken))
                        {
                            var resultSet = index.getSetLinks((WordToken)stack.Pop()).Intersect(index.getSetLinks((WordToken)stack.Pop()));
                        }
                        if (token.GetType() == typeof(OrToken))
                        {

                        }
                    }

                }
            }
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

