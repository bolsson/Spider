using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvertedIndex
{
    class InvertedIndex
    {
        Dictionary<string, SortedSet<string>> invertedIndex = new Dictionary<string, SortedSet<string>>();
        public void Add(string url, List<string> wordsOnPage)
        {
            SortedSet<string> urlList;

            foreach (var word in wordsOnPage)
            {
                if (invertedIndex.ContainsKey(word))
                {
                    urlList = invertedIndex[word];
                    urlList.Add(url);
                    invertedIndex[word] = urlList;
                }
                else
                {
                    urlList = new SortedSet<string>();
                    urlList.Add(url);
                    invertedIndex.Add(word, urlList);
                }
                
            }
        }
    }
}
