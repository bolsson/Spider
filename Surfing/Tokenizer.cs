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
        public override string ToString()
        {
            return "Base class for Token";
        }
    }

    public class LogicToken : Token
    {
    }
    public class AndToken : LogicToken
    {
        public override string ToString()
        {
            return "AND";
        }
    }

    public class OrToken : LogicToken
    {
        public override string ToString()
        {
            return "OR";
        }
    }

    public class AndNotToken : LogicToken
    {
        public override string ToString()
        {
            return "ANDNOT"; ;
        }
    }

    public class ParenthesisToken : Token
    {
    }

    public class ParenthesisBeginToken : ParenthesisToken
    {
        public override string ToString()
        {
            return "(";
        }
    }

    public class ParenthesisEndToken : ParenthesisToken
    {
        public override string ToString()
        {
            return ")";
        }
    }

    public class Quote : Token
    {
        public override string ToString()
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

        public override string ToString()
        {
            return _word;
        }
    }
    public class Tokens
    {
        private Queue<Token> _tokenQueue;

        public Tokens()
        {
            _tokenQueue = new Queue<Token>();
        }

        public void AddToken(Token token)
        {
            _tokenQueue.Enqueue(token);
        }

        public Token getNextToken()
        {
            var firstToken = _tokenQueue.Dequeue();           
            return firstToken;
        }

        public Token PeekToken()
        {
            return _tokenQueue.Peek();
        }

        public Boolean isEmpty()
        {
            return _tokenQueue.Count() == 0;
        }
    }

    public class Lexer
    {
        private StringReader _reader;
        public Tokens tokens;

        public Lexer(string queryExpression)
        {
            tokens = new Tokens();
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
                    tokens.AddToken(new ParenthesisBeginToken());
                    _reader.Read();
                }
                else if (c == ')')
                {
                    tokens.AddToken(new ParenthesisEndToken());
                    _reader.Read();
                }
                else if (Char.IsLetterOrDigit(c) || c == '"' || c == '\'')
                {
                    var word = GetWord(_reader);
                    if (word.ToUpper() == "AND")
                    {
                        tokens.AddToken(new AndToken());
                    }
                    else if (word.ToUpper() == "OR")
                    {
                        tokens.AddToken(new OrToken());
                    }
                    else if (word.ToUpper() == "ANDNOT")
                    {
                        tokens.AddToken(new AndNotToken());
                    }
                    else
                    {
                        tokens.AddToken(new WordToken(word));
                    }
                }
               
                
                else
                    throw new Exception("Unknown character in expression: " + c);
            }
            return tokens;
        }

        private string GetWord(StringReader reader)
        {
            List<char> letterList = new List<char>();
            char c;
            string word = "";
            int nrQuotes = 0;
            if ('"' == (char)reader.Peek() || '\'' == (char)reader.Peek()) nrQuotes++;
            while (char.IsLetterOrDigit((char)reader.Peek()) || (nrQuotes > 0) )
            { 
                c = (char) reader.Read();
                if (c == '"') 
                {
                    nrQuotes++;
                    c = (char)reader.Read();
                }
                if (nrQuotes > 2) break;
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
