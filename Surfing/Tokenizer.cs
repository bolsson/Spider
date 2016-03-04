using System;
using System.Collections.Generic;
using System.Linq;
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
    class Tokenizer
    {

        private List<string> _tokenList;

        public Tokenizer()
        {
            _tokenList = new List<string>();
        }

        public void AddToken(string token)
        {
            _tokenList.Add(token);
        }

        public string getNextToken()
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
}
