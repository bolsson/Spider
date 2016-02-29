using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spider
{
    class Parser
    {
    }

    //NOTE: from https://dzone.com/articles/recursive-descent-parser-c

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
