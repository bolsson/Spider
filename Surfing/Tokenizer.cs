using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spider
{
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
