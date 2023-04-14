using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace AnalysProject
{
    class Q1
    {

        static Dictionary<string, int> responseCounts;

        HttpClient httpClient;
        public Q1()
        {
            httpClient = new HttpClient();
            responseCounts = new Dictionary<string, int>();
        }

        public async Task<string> GetServerNameStatsAsync(List<string> urls)
        {
            foreach (string serverUrl in urls)
            {
                string response = await getServerName(serverUrl);
                if (responseCounts.ContainsKey(response))
                {
                    responseCounts[response]++;
                }
                else
                {
                    responseCounts.Add(response, 1);
                }
            }
            string rsp = "";
            foreach (KeyValuePair<string, int> pair in responseCounts)
            {
                rsp += $"Server {pair.Key} appears : {pair.Value} times \n";
            }
            return rsp;
        }

        public async Task<string> getServerName(string serverURL)
        {
            var response = await httpClient.GetAsync(serverURL);
            if (response.IsSuccessStatusCode)
            {
                if (response.Headers.TryGetValues("Server", out IEnumerable<string> values))
                {
                    string server = values.FirstOrDefault();
                    return server;
                }
                return "ErrorToGetServerName";
            } 
            return "ServerDidntRespondCorrectly";
        }
    }
        
}
