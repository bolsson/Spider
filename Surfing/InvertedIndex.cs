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

        public void Add(string url, List<string> wordsOnPage)
        {
            List<string> urlList;

            foreach (var word in wordsOnPage)
            {
                if (invertedIndex.ContainsKey(word))
                {
                    urlList = invertedIndex[word];
                    urlList.Add(url);
                }
                else
                {
                    urlList = new List<string>();
                    urlList.Add(url);
                }
                invertedIndex.Add(word, urlList);
            }
        }
    }
}