using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace AnalysProject
{
    class Q3
    {
        HttpClient httpClient;
        Dictionary<string, long> contentPerSite;
        public Q3()
        {
            httpClient = new HttpClient();
            contentPerSite = new Dictionary<string, long>();
        }

        public async Task<string> GetSitesContentSize(List<string> urls)
        {
            string rsp = "";
            await GetContentSize(urls);

            foreach (KeyValuePair<string, long> pair in contentPerSite)
            {
                rsp += $"Taille du contenu du {pair.Key} est de : {pair.Value} octets \n";
            }

            rsp += $"\n\n";
            rsp += $"Taille du contenu en moyenne est de : {GetAverageLength()} octets \n";

            return rsp;
        }

        public async Task GetContentSize(List<string> urls)
        {
            foreach (var url in urls)
            {

                var response = await httpClient.GetAsync(url);

                if (response.Content.Headers.Contains("Content-Length"))
                {
                    long siteLength = response.Content.Headers.ContentLength.Value;

                    contentPerSite[url] = siteLength;
                    //Console.WriteLine($"Taille de la page {url} est de : {siteLength} ");

                }
            }
        }

        public string GetAverageLength()
        {
            return contentPerSite.Values.Average().ToString("F4");
        }
    }
}
