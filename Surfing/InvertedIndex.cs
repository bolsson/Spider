using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spider
{
    class InvertedIndex
    {
        Dictionary<string, SortedSet<string>> invertedIndex = new Dictionary<string, SortedSet<string>>();
        public void Add(string url, List<string> wordsOnPage)
        {
            SortedSet<string> urlSet;

            foreach (var word in wordsOnPage)
            {
                if (invertedIndex.ContainsKey(word))
                {
                    urlSet = invertedIndex[word];
                    urlSet.Add(url);
                    invertedIndex[word] = urlSet;
                }
                else
                {
                    urlSet = new SortedSet<string>();
                    urlSet.Add(url);
                    invertedIndex.Add(word, urlSet);
                }
                
            }
        }

        public SortedSet<string> getSetLinks(WordToken searchWord)
        {
            return invertedIndex[searchWord.ToString()];
        }
    }
}
