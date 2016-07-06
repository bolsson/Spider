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
                buildOrBranchIfNoLogicTokensBetweenWords();
        }
        private void _query()
        {
            _andTerm();
            if (next.GetType() == typeof(AndNotToken))
            {
                Token token = _tokens.getNextToken();
                BinaryTree = new BinaryTreeImp();
                BinaryTree.insertNode(new Node(token), branchesStack.Pop());
                branchesStack.Push(BinaryTree.root);
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
                Token token = _tokens.getNextToken();
                BinaryTree = new BinaryTreeImp();
                BinaryTree.insertNode(new Node(token), branchesStack.Pop());
                branchesStack.Push(BinaryTree.root);
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
                Token token = _tokens.getNextToken();
                BinaryTree = new BinaryTreeImp();
                BinaryTree.insertNode(new Node(token), branchesStack.Pop());
                branchesStack.Push(BinaryTree.root);
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
                //pop the last two node-branches and combine them, then push their root back on the stack
                var leaf = branchesStack.Pop();
                var root = branchesStack.Pop();
                BinaryTree = new BinaryTreeImp();
                BinaryTree.insertNode(root, leaf);
                branchesStack.Push(BinaryTree.root);
                if (_tokens.isEmpty())
                {
                    BinaryTree = new BinaryTreeImp();
                    leaf = branchesStack.Pop();
                    root = branchesStack.Pop();
                    BinaryTree.insertNode(root, leaf);
                    branchesStack.Push(BinaryTree.root);
                    return;
                } 
                next = _tokens.PeekToken();
                _term();
            }
            if (next.GetType() == typeof(WordToken) && (!_tokens.isEmpty()))
            {
                Token wordToken = _tokens.getNextToken();
                branchesStack.Push(new Node(wordToken));
                if (_tokens.isEmpty() && BinaryTree.count == 0) //a one word search string
                {
                    BinaryTree.root = new Node(wordToken);
                    return;
                }
                if (_tokens.isEmpty())
                {
                    BinaryTree.insertNode(BinaryTree.root, new Node(wordToken));
                    return;
                }

                next = _tokens.PeekToken();
                //buildOrBranchIfNoLogicTokensBetweenWords((WordToken)wordToken);
                _term();
            }
            if (_tokens.isEmpty())
                return;
        }


        private void buildOrBranchIfNoLogicTokensBetweenWords()
        {
            BinaryTree.root = new Node(_tokens.getNextToken());
            branchesStack.Push(BinaryTree.root);
          
            while (!_tokens.isEmpty())
            {
                Token wordToken = _tokens.getNextToken();

                Node OrNode = new Node(new OrToken());
                BinaryTree = new BinaryTreeImp();
                BinaryTree.insertNode(OrNode, new Node(wordToken));
                branchesStack.Push(BinaryTree.root);
                BinaryTree = new BinaryTreeImp();
                BinaryTree.insertNode(branchesStack.Pop(), branchesStack.Pop());
                branchesStack.Push(BinaryTree.root);
            }
        }
    }



    //NOTE: from https://dzone.com/articles/recursive-descent-parser-c
    //NOTE: even better http://blog.roboblob.com/2014/12/12/introduction-to-recursive-descent-parsers-with-csharp/

    //public class Tokenizer
    //{
    //    private readonly StringReader _reader;
    //    private string _text;

    //    public Tokenizer(string text)
    //    {
    //        _text = text;
    //        _reader = new StringReader(text);
    //    }

    //    public IEnumerable<Token> Tokenize()
    //    {
    //        var tokens = new List<Token>();
    //        while (_reader.Peek() != -1)
    //        {
    //            while (Char.IsWhiteSpace((char)_reader.Peek()))
    //            {
    //                _reader.Read();
    //            }

    //            if (_reader.Peek() == -1)
    //                break;

    //            var c = (char)_reader.Peek();
    //            switch (c)
    //            {
    //                case '!':
    //                    tokens.Add(new NegationToken());
    //                    _reader.Read();
    //                    break;
    //                case '(':
    //                    tokens.Add(new OpenParenthesisToken());
    //                    _reader.Read();
    //                    break;
    //                case ')':
    //                    tokens.Add(new ClosedParenthesisToken());
    //                    _reader.Read();
    //                    break;
    //                default:
    //                    if (Char.IsLetter(c))
    //                    {
    //                        var token = ParseKeyword();
    //                        tokens.Add(token);
    //                    }
    //                    else
    //                    {
    //                        var remainingText = _reader.ReadToEnd() ?? string.Empty;
    //                        throw new Exception(string.Format("Unknown grammar found at position {0} : '{1}'", _text.Length - remainingText.Length, remainingText));
    //                    }
    //                    break;
    //            }
    //        }
    //        return tokens;
    //    }

    //    private Token ParseKeyword()
    //    {
    //        var text = new StringBuilder();
    //        while (Char.IsLetter((char)_reader.Peek()))
    //        {
    //            text.Append((char)_reader.Read());
    //        }

    //        var potentialKeyword = text.ToString().ToLower();

    //        switch (potentialKeyword)
    //        {
    //            case "true":
    //                return new TrueToken();
    //            case "false":
    //                return new FalseToken();
    //            case "and":
    //                return new AndToken();
    //            case "or":
    //                return new OrToken();
    //            default:
    //                throw new Exception("Expected keyword (True, False, And, Or) but found " + potentialKeyword);
    //        }
    //    }
    //}


    //public bool Parse()
    //{
    //    while (_tokens.Current != null)
    //    {
    //        var isNegated = _tokens.Current is NegationToken;
    //        if (isNegated)
    //            _tokens.MoveNext();

    //        var boolean = ParseBoolean();
    //        if (isNegated)
    //            boolean = !boolean;

    //        while (_tokens.Current is OperandToken)
    //        {
    //            var operand = _tokens.Current;
    //            if (!_tokens.MoveNext())
    //            {
    //                throw new Exception("Missing expression after operand");
    //            }
    //            var nextBoolean = ParseBoolean();

    //            if (operand is AndToken)
    //                boolean = boolean && nextBoolean;
    //            else
    //                boolean = boolean || nextBoolean;

    //        }

    //        return boolean;
    //    }

    //    throw new Exception("Empty expression");
    //}


    //private bool ParseBoolean()
    //{
    //    if (_tokens.Current is BooleanValueToken)
    //    {
    //        var current = _tokens.Current;
    //        _tokens.MoveNext();

    //        if (current is TrueToken)
    //            return true;

    //        return false;
    //    }
    //    if (_tokens.Current is OpenParenthesisToken)
    //    {
    //        _tokens.MoveNext();

    //        var expInPars = Parse();

    //        if (!(_tokens.Current is ClosedParenthesisToken))
    //            throw new Exception("Expecting Closing Parenthesis");

    //        _tokens.MoveNext();

    //        return expInPars;
    //    }
    //    if (_tokens.Current is ClosedParenthesisToken)
    //        throw new Exception("Unexpected Closed Parenthesis");

    //    // since its not a BooleanConstant or Expression in parenthesis, it must be a expression again
    //    var val = Parse();
    //    return val;
    //}

}
