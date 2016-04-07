using System;
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
        public string toString()
        {
            return "AND";
        }
    }

    public class OrToken : LogicToken
    {
        public string toString()
        {
            return "OR";
        }
    }

    public class AndNotToken : LogicToken
    {
        public string toString()
        {
            return "ANDNOT";
        }
    }

    public class ParenthesisToken : Token
    {
    }

    public class ParenthesisBeginToken : ParenthesisToken
    {
        public string toString()
        {
            return "(";
        }
    }

    public class ParenthesisEndToken : ParenthesisToken
    {
        public string toString()
        {
            return ")";
        }
    }

    public class Quote : Token
    {
        public string toString()
        {
            return "'";
        }
    }

    public class QuoteBegin : Quote { }

    public class QuoteEnd : Quote { }

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
                else if (c == ')')
                {
                    _tokens.AddToken(new ParenthesisEndToken());
                }
                else if (Char.IsLetterOrDigit(c))
                {
                    var word = GetWord(_reader);
                    if (word.ToUpper() == "AND")
                    {
                        _tokens.AddToken(new AndToken());
                    }
                    else if (word.ToUpper() == "OR")
                    {
                        _tokens.AddToken(new OrToken());
                    }
                    else if (word.ToUpper() == "ANDNOT")
                    {
                        _tokens.AddToken(new AndNotToken());
                    }
                    else
                    {
                        _tokens.AddToken(new WordToken(word));
                    }
                }
               
                
                else
                    throw new Exception("Unknown character in expression: " + c);
            }
            return _tokens;
        }

        private string GetWord(StringReader reader)
        {
            List<char> letterList = new List<char>();
            char c;
            string word = "";
            int nrQuotes = 0;
            if ('"' == (char)reader.Peek()) nrQuotes++;
            while (char.IsLetterOrDigit((char)reader.Peek()) && (nrQuotes < 2 || nrQuotes == 1) )
            { 
                c = (char) reader.Read();
                if (c == '"') 
                {
                    nrQuotes++;
                    c = (char)reader.Read();
                }
                letterList.Add(c);
            }
            letterList.ForEach(letter =>
                {
                    word += letter;
                }
            );
            return word;
        }
    }
}
