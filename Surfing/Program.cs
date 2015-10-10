using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
//using System.Net.Http;
using System.Collections.Generic;
using System.IO;

//using System.Windows.Forms;

namespace Spider
{
    class Program
    {
        //const string Seed = "http://kenrockwell.com";

        static void Main(string[] args)
        {
            Spider spider = new Spider();
            LinkTable linkTable = new LinkTable();
            ParseHtml parser = new ParseHtml();

            while (linkTable.HasLink())
            {
                var link = linkTable.GetLink();
                var webPage = spider.Crawl(link);
                if (webPage.Result == null || !webPage.Result.IsSuccessStatusCode || webPage.ToString().Length > 10000000 || webPage.Status == TaskStatus.Canceled || webPage.Status == TaskStatus.Faulted || webPage.IsFaulted ) continue;
                var htmlDoc = parser.GetDocument(webPage.Result);
                if (htmlDoc.Status == TaskStatus.Faulted || htmlDoc.Status == TaskStatus.Canceled)
                {
                    continue;
                }
                var linksOnPage = parser.FindLinksOnPage(htmlDoc.Result);
                linkTable.Add(linksOnPage);
            }
        }
    }

    class Spider
    {
        private readonly HttpClient _httpClient;

        public Spider()
        {
            _httpClient = new HttpClient();
        }


        public async Task<HttpResponseMessage> Crawl(string link)
        {
            try
            {
                var responseMessage = await _httpClient.GetAsync(link);
                return responseMessage;
            }
            catch (Exception e)
            {
                // on an exception send back the new response object, as it has no body it will produce no links but it will keep the spider crawling
                HttpResponseMessage responseMessage = new HttpResponseMessage();
                return responseMessage;
            }


        }
    }

    class ParseHtml
    {
        public async Task<HtmlDocument> GetDocument(HttpResponseMessage responseMessage)
        {
            string result = await responseMessage.Content.ReadAsStringAsync();
            if (!responseMessage.IsSuccessStatusCode)
                throw new FileNotFoundException("Unable to retrieve document");

            //string source = Encoding.GetEncoding("utf-8").GetString(responseMessage, 0, responseMessage.Length - 1);
            //source = WebUtility.HtmlDecode(source);
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(result);
            return document;
        }

        public List<string> FindLinksOnPage(HtmlDocument htmlDocument)
        {
            var linkList = new List<HtmlNode>();

            HtmlNode body = htmlDocument.DocumentNode.Descendants().FirstOrDefault(x => x.Name.Equals("body"));

            if (body != null) linkList = body.Descendants("a").ToList();

            return linkList.Select(atag => atag.GetAttributeValue("href", "no string")).Where(x => x.StartsWith("http://")).ToList();
        } 

        public string FindBodyText(HtmlDocument htmlDocument)
        {
            return "";
        }
    }

    class LinkTable
    {
        public string Link { get; set; }
        private readonly Dictionary<string, string> _linksFound;
        private readonly Dictionary<string, string> _linksVisited;
        private readonly Queue<string> _linksToVisitQueue;
        private const string Seed = "https://en.wikipedia.org/wiki/English_literature"; // "http://kenrockwell.com";

        public LinkTable()
        {
            _linksFound = new Dictionary<string, string>();
            _linksVisited = new Dictionary<string, string>();
            _linksToVisitQueue = new Queue<string>();
            _linksToVisitQueue.Enqueue(Seed);
        }

        public void Add(List<string> linkList)
        {
            foreach (var link in linkList.Where(link => !VisitedLink(link)))
            {
                _linksFound.Add(link, link);
                _linksToVisitQueue.Enqueue(link);
            }
        }

        private bool VisitedLink(string link)
        {
            if (!_linksFound.ContainsKey(link)) return false;
            return _linksFound[link] == link;
        }

        public string GetLink()
        {
            var link = _linksToVisitQueue.Dequeue();
            _linksVisited.Add(link, link);
            return link;
        }

        public bool HasLink()
        {
            return _linksToVisitQueue.Count > 0;
        }

    }
}
