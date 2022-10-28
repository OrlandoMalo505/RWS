using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RWS
{
    public interface IRepo
    {
        Task<string> CallUrl(string fullUrl);
        List<string> ExtractHref(string URL);
        Task WriteToFile(string html, string url);
        Task SaveURLS(List<string> list);
        List<string> ExtractParagraphs(string URL);
        Task SaveElements(List<string> list, string url, string postfix);
        List<string> ExtractLiElements(string URL);
    }
}
