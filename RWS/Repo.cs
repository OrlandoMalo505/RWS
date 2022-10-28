using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RWS
{
    public class Repo : IRepo
    {

        public async Task<string> CallUrl(string fullUrl)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync(fullUrl);
            return response;
        }

        public List<string> ExtractHref(string URL)
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = new HtmlDocument();
            doc = web.Load(URL);

            List<string> links = new List<string>();

            foreach (HtmlNode link in doc.DocumentNode.SelectNodes("//a[@href]"))
            {
                HtmlAttribute att = link.Attributes["href"];

                if (att.Value.Contains("a"))
                {
                    if (att.Value.StartsWith("/") && RemoveStaticFiles(att.Value))
                    {
                        links.Add(att.Value);
                    }
                }
            }

            return links;
        }

        public List<string> ExtractParagraphs(string URL)
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = new HtmlDocument();
            doc = web.Load(URL);

            List<string> paragraphs = new List<string>();

            foreach (HtmlNode p in doc.DocumentNode.SelectNodes("//p"))
            {
                paragraphs.Add(p.OuterHtml);
            }

            return paragraphs;
        }
        public List<string> ExtractLiElements(string URL)
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = new HtmlDocument();
            doc = web.Load(URL);

            List<string> elements = new List<string>();

            foreach (HtmlNode el in doc.DocumentNode.SelectNodes("//li"))
            {
                elements.Add(el.OuterHtml);
            }

            return elements;
        }
        

        public async Task WriteToFile(string html, string url)
        {
            await File.WriteAllTextAsync($"{url.Replace("https://","").Replace("/","")}.csv", html);
        }

        private bool RemoveStaticFiles(string value)
        {
            if(value.Contains("images"))
                return false;
            return true;
        }

        public async Task SaveURLS(List<string> list)
        {
            await File.WriteAllLinesAsync("URLS.csv", list);
        }

        public async Task SaveElements(List<string> list, string url, string postfix)
        {
            await File.WriteAllLinesAsync($"{url.Replace("https://", "").Replace("/", "")}-{postfix}.csv", list);
        }
    }
}
