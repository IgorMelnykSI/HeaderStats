using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace AnalysProject
{
    class Q2
    {
        HttpClient httpClient;

        // âges des pages dans un dictionnaire
        Dictionary<string, double> ages;
        public Q2()
        {
            httpClient = new HttpClient();
            ages = new Dictionary<string, double>();
        }

        public async Task<string> GetAgeStatsAsync(List<string> urls)
        {
            string rsp = "";
            await GetAge(urls);

            foreach (KeyValuePair<string, double> pair in ages)
            {
                rsp += $"Age de la page {pair.Key} est de : {pair.Value} heures \n";
            }

            rsp += $"\n\n";

            rsp += $"Temps de vie moyen : {GetAverageLifeTime()} heures \n";
            rsp += $"Ecart type : {GetStandardDeviation()} heures \n";

            return rsp;
        }

        public async Task GetAge(List<string> urls)
        {
            
            foreach (var url in urls)
            {
                
                var response = await httpClient.GetAsync(url);

                if (response.Content.Headers.Contains("Last-Modified"))
                {
                    DateTimeOffset siteDate = response.Content.Headers.LastModified.Value;

                    double age = (DateTimeOffset.Now - siteDate).TotalHours;
                    ages[url] = age;
                    //Console.WriteLine($"Age de la page {url} est de : {age} heures");

                }
            }
        }

        public string GetAverageLifeTime()
        {
            return ages.Values.Average().ToString("F4");
        }

        public string GetStandardDeviation()
        {
            double moyenne = ages.Values.Average();
            double sommeCarreDiff = 0;

            foreach (KeyValuePair<string, double> pair in ages)
            {
                double diff = pair.Value - moyenne;
                sommeCarreDiff = diff * diff;
            }

            double variance = sommeCarreDiff / ages.Count;
            return Math.Sqrt(variance).ToString("F4");
        }
    }
}
