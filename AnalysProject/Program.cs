using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace AnalysProject
{
    class Program
    {
        //Q1
        static List<string> urls = new List<string> {
        "https://www.google.com/", "https://www.amazon.fr", "https://www.netflix.com/", "https://www.apple.com",
        "https://www.bing.com", "https://fr.yahoo.com", "https://www.lemonde.fr", "https://www.stackoverflow.com/",
        "https://www.twitter.com/", "https://www.wikipedia.org/", "https://smee.io",
        "https://www.lemonde.fr/sciences/article/2023/04/13/la-mission-juice-qui-vise-les-satellites-de-jupiter-prete-au-decollage-depuis-la-base-de-kourou_6169352_1650684.html"
        };

        //Q2
        static List<string> urlsQ2 = new List<string>
        {
            "https://fr.wikipedia.org/wiki/Java_(technique)", "https://fr.wikipedia.org/wiki/C_sharp",
            "https://fr.wikipedia.org/wiki/Python_(langage)", "https://fr.wikipedia.org/wiki/C%2B%2B"
        };

        //Q3
        static List<string> urlsQ3 = new List<string>
        {
            "https://www.cnn.com/", "https://www.spotify.com/", "https://www.wikipedia.org/", "https://www.nike.com/",
            "https://www.reddit.com/", "https://www.nationalgeographic.com/", "https://www.etsy.com/", "https://www.theguardian.com/",
            "https://www.netflix.com/", "https://www.apple.com/"
        };

        // Fontion pour répondre à une requette GET
        static void SendResponse(string rsp, HttpListenerContext context)
        {
            HttpListenerResponse response = context.Response;

            // Définissez le contenu de la réponse
            string responseString = rsp;
            byte[] buffer = Encoding.UTF8.GetBytes(responseString);
            response.ContentType = "text/plain";
            response.ContentLength64 = buffer.Length;

            // Envoyez la réponse au client
            System.IO.Stream output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);
            output.Close();
        }

        public static async Task Main(string[] args)
        {

            HttpListener listener = new HttpListener();
            listener.Prefixes.Add("http://localhost:2000/");
            listener.Start(); // Démarrage
            Console.WriteLine("Serveur démarré");

            while (true)
            {
                HttpListenerContext context = listener.GetContext();
                string requestParameter = context.Request.Url.ToString().Substring(21);
                Console.WriteLine(requestParameter);
                
                // Gestion des endpoints
                switch (requestParameter)
                {
                    case "/api/Q1":
                        Q1 q1 = new Q1();
                        string q1Response = await q1.GetServerNameStatsAsync(urls);
                        SendResponse(q1Response, context);
                        break;
                    case "/api/Q2":
                        Q2 q2 = new Q2();
                        string q2Response = await q2.GetAgeStatsAsync(urlsQ2);
                        SendResponse(q2Response, context);
                        break;
                    case "/api/Q3":
                        Q3 q3 = new Q3();
                        string q3Response = await q3.GetSitesContentSize(urlsQ3);
                        SendResponse(q3Response, context);
                        break;
                    default:
                        break;
                }
            }

        }
    }
}
