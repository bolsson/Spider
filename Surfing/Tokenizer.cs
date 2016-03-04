﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Spider
{

    public abstract class Token
    {
    }

    public class LogicToken : Token
    {
    }
    public class AndToken : LogicToken
    {
    }

    public class OrToken : LogicToken
    {
    }

    public class AndNotToken : LogicToken
    {
    }

    public class ParenthesisToken : Token
    {
    }

    public class ParenthesisBeginToken : ParenthesisToken
    {
    }

    public class ParenthesisEndToken : ParenthesisToken
    {
    }

    public class WordToken : Token
    {
        private readonly string _word;

        public WordToken(string word)
        {
            _word = word;
        }

        public string Word
        {
            get { return _word; }
        }
    }
    class Tokens
    {
        private List<Token> _tokenList;

        public Tokens()
        {
            _tokenList = new List<Token>();
        }

        public void AddToken(Token token)
        {
            _tokenList.Add(token);
        }

        public Token getNextToken()
        {
            var firstToken = _tokenList[0];
            _tokenList.RemoveAt(0);
            return firstToken;
        }

        public Boolean isEmpty()
        {
            return _tokenList.Count() == 0;
        }
    }

    class Lexer
    {
        private StringReader _reader;
        private Tokens _tokens;

        public Lexer(string queryExpression)
        {
            _tokens = new Tokens();
            _reader = new StringReader(queryExpression);
        }

        public Tokens Tokenize()
        {
            while (_reader.Peek() != -1)
            {
                var c = (char)_reader.Peek();
                if (Char.IsWhiteSpace(c))
                {
                    _reader.Read();
                    continue;
                }
                if (c == '(')
                {
                    _tokens.AddToken(new ParenthesisBeginToken());
                }
            }
            return _tokens;
        }

    }
}
