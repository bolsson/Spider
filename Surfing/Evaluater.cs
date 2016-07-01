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
        private Stack<IEnumerable<string>> stack;
        private InvertedIndex _index;
        public IEnumerable<string> result;

        public Evaluater(Node root, InvertedIndex index)
        {
            queue = new Queue<Token>();
            this.stack = new Stack<IEnumerable<string>>();
            _index = new InvertedIndex();
            evaluaterOrder(root);
            result = stack.Pop();
        }
        private void evaluaterOrder(Node node)
        {
            if (node == null) return;

            evaluaterOrder(node.left);
            evaluaterOrder(node.right);
            //when both left and right branch has been visited for a node
            //queue.Enqueue(root.token);
            evaluateTwoNodes(node);
        }

        private void evaluateTwoNodes(Node node)
        {

            if (node.token.GetType() == typeof(LogicToken))
            {
                evaluate(node.token);
            }
            else if (node.token.GetType() == typeof(WordToken))
            {
                stack.Push(_index.getSetLinks((WordToken)node.token));
            }
        }

        //the evaluater uses Reverse Polish Notation
        public void evaluate(Token token)
        {
            IEnumerable<string> newLinkSet = null;
            if (stack.Count >= 2)
            {
                if (token.GetType() == typeof(AndToken))
                {
                    newLinkSet = stack.Pop().Intersect(stack.Pop());
                    
                }
                if (token.GetType() == typeof(OrToken))
                {
                    newLinkSet = stack.Pop().Union(stack.Pop());
                }
                stack.Push(newLinkSet);
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

