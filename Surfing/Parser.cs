using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spider
{
    public class Parser
    {
        public BinaryTreeImp BinaryTree;
        Tokens _tokens;
        Token next;
        private Stack<Node> branchesStack;

        public Parser(Tokens tokens)
        {
            branchesStack = new Stack<Node>();
            BinaryTree = new BinaryTreeImp();
            _tokens = tokens;

            next = _tokens.PeekToken();
            if (next.ToString() == "(")
                _query();
            else //NOTE: if the first token is not a parenthesis then all tokens are treated as word tokens, not logic tokens or others
                _buildTreeWordsOnly();
        }
        private void _query()
        {
            _andTerm();
            if (next.GetType() == typeof(AndNotToken))
            {
                newSimpleTree(new Node(_tokens.getNextToken()), branchesStack.Pop());
                next = _tokens.PeekToken();
                _query();
            }
            else
                return;
        }

        private void _andTerm()
        {
            _orTerm();
            if (next.GetType() == typeof(AndToken))
            {
                newSimpleTree(new Node(_tokens.getNextToken()), branchesStack.Pop());
                next = _tokens.PeekToken();
                _andTerm();
            }
            else
                return;
        }

        private void _orTerm()
        {
            _term();
            if (next.GetType() == typeof(OrToken))
            {
                newSimpleTree(new Node(_tokens.getNextToken()), branchesStack.Pop());
                next = _tokens.PeekToken();
                _orTerm();
            }
            else
                return;
        }

        private void _term()
        {
            if (next.GetType() == typeof(ParenthesisBeginToken) && (!_tokens.isEmpty()))
            {
                Token token = _tokens.getNextToken();
                BinaryTree = new BinaryTreeImp(); //reset the binary tree to ready for a new branch build
                next = _tokens.PeekToken();
                _term();
            }
            if (next.GetType() == typeof(ParenthesisEndToken) && (!_tokens.isEmpty()))
            {
                Token token = _tokens.getNextToken();
                _buildTreeFromAllRemainingBranches();
                if (_tokens.isEmpty())
                    return;
                next = _tokens.PeekToken();
                _term();
            }
            if (next.GetType() == typeof(WordToken) && (!_tokens.isEmpty()))
            {
                Token wordToken = _tokens.getNextToken();
                branchesStack.Push(new Node(wordToken));
                next = _tokens.PeekToken();
                _term();
            }
            if (_tokens.isEmpty())
                return;
        }

        private void newSimpleTree(Node root, Node leaf)
        {
            BinaryTree = new BinaryTreeImp();
            BinaryTree.insertNode(root, leaf);
            branchesStack.Push(BinaryTree.root);
        }

        private void _buildTreeFromAllRemainingBranches()
        {
            while (branchesStack.Count > 1)
            {
                var leaf = branchesStack.Pop();
                var root = branchesStack.Pop();
                newSimpleTree(root, leaf);
            }
        }

        private void _buildTreeWordsOnly()
        {
            BinaryTree.root = new Node(_tokens.getNextToken());
            branchesStack.Push(BinaryTree.root);
          
            while (!_tokens.isEmpty())
            {
                Token wordToken = _tokens.getNextToken();
                Node OrNode = new Node(new OrToken());
                newSimpleTree(OrNode, new Node(wordToken));
                newSimpleTree(branchesStack.Pop(), branchesStack.Pop());
            }
        }
    }



    //NOTE: from https://dzone.com/articles/recursive-descent-parser-c
    //NOTE: even better http://blog.roboblob.com/2014/12/12/introduction-to-recursive-descent-parsers-with-csharp/

}
