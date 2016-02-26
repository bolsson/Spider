using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvertedIndex
{
    class InvertedIndex
    {
        Dictionary<string, List<string>> invertedIndex = new Dictionary<string, List<string>>();
        public void Add(string urlString, List<string> wordsOnPage)
        {
            foreach (var word in wordsOnPage)
            {
                if (invertedIndex.ContainsKey(urlString))
                {
                    var wordList = invertedIndex[urlString];
                    wordList.Add(word);
                }
                else
                {
                    var wordList = new List<string>();
                    wordList.Add(word);
                    invertedIndex.Add(urlString, wordList);
                }
            }
        }
    }
}
