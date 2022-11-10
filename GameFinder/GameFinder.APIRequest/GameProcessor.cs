using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace GameFinder.APIRequest
{
    public class GameProcessor
    {
        public async Task<Tuple<int,double>> LoadGame(string name)
        {
            name = name.ToLower();
            string[] namelist = name.Split(" ");
            name = "";
            foreach(string i in namelist)
            {
                name += i+"-";
            }
            name = name.Remove(name.Length - 1); 
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://www.metacritic.com/game/pc/"+name+"/critic-reviews"),
            };
            Tuple<int, double> returnTuple=null;
            Console.WriteLine(name);
            using (var response = await APIRequest.ApiClient.SendAsync(request))
            {
                if (response.IsSuccessStatusCode)
                {
                    var body = await response.Content.ReadAsStringAsync();
                    var document = new HtmlDocument();
                    document.LoadHtml(body);
                    var nodes = document.DocumentNode.SelectNodes("//div[@class='metascore_w large game positive']/span");
                    var nodes2 = document.DocumentNode.SelectNodes("//div[@class='metascore_w user medium game positive']");
                    int metaScore = 0;
                    double userScore = 0.0;
                    foreach (var node in nodes)
                    {
                        metaScore = int.Parse(node.InnerHtml);
                    }
                    foreach (var node in nodes2)
                    {
                        userScore = double.Parse(node.InnerHtml);
                    }

                    returnTuple = new Tuple<int, double>(metaScore, userScore);
                }
            }
            return returnTuple;
        }
    }
}
