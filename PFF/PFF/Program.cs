using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MyScraper;
using System.Collections.Specialized;
using System.Configuration;
using HtmlAgilityPack;
using System.Web;

namespace PFF
{
    class Program
    {
        static void Main(string[] args)
        {
            // vars

            string pffDataDirectory = ConfigurationManager.AppSettings["pffDataDirectory"];
            string localPath = pffDataDirectory + @"\2014_03.html";
            

            //// login

            //Console.WriteLine("Logging in...");
            //var client = new CookieAwareWebClient();
            //login(client);


            //// download page

            //Console.WriteLine("Downloading Y2014 W03");
            //string url = @"https://www.profootballfocus.com/data/by_week.php?tab=by_week&season=2014&wk=3&teamid=-1&gameid=&stats=";
            //client.DownloadFile(url, localPath);
            

            // Get seasons

            HtmlDocument doc = new HtmlDocument();
            doc.Load(localPath);
            string xpath = @"//select[@id='selSeason']/option";
            HtmlNode docNode = doc.DocumentNode;
            //string xpath = @"/html/body/div/form/div[1]/table/tbody/tr[2]/td/table/tbody/tr[2]/td[2]/div/select/option[1]";
            HtmlNodeCollection seasonNodes = docNode.SelectNodes(xpath);
            List<int> seasons = new List<int>();
            foreach (HtmlNode n in seasonNodes)
            {
                int i = Int32.Parse(n.Attributes["value"].Value);
                Console.WriteLine(i);
                seasons.Add(i);
            }
            

            // Get weeks

            
            List<string> weeks = new List<string>();
            weeks.Concat(getWeeks(doc, @"//div[@id='header']/table/tr[2]/td/table/tr[2]/td[4]/div/ul/li"));
            weeks.Concat(getWeeks(doc, @"//div[@id='header']/table/tr[2]/td/table/tr[2]/td[6]/div/ul/li"));
            weeks.Concat(getWeeks(doc, @"//div[@id='header']/table/tr[2]/td/table/tr[2]/td[8]/div/ul/li"));
            
            
            // Get games

            Console.WriteLine("Parsing games...");
            HtmlNodeCollection gameNodes = doc.DocumentNode.SelectNodes(@"//*[@id='games']/table[1]/tr/td");
            List<Game> games = new List<Game>();
            foreach (HtmlNode gameNode in gameNodes)
            {
                games.Add(parseGame(gameNode));
            }
            

            Console.WriteLine("Created game objects");
            
            

            // close

            Console.WriteLine("\nPress enter to exit");
            Console.Read();
        }

        private static Game parseGame(HtmlNode gameNode)
        {
            string weekString = gameNode.SelectSingleNode("div/table/tr[2]/td/table/tr/th[1]").InnerText;
            int week = Int32.Parse(weekString.Substring("Week ".Length));
            HtmlNode gameRowNode = gameNode.SelectSingleNode("div/table/tr[5]/td/table/tr");
            int awayScore = Int32.Parse(gameRowNode.SelectSingleNode("td[1]").InnerText);
            string awayTeamLink = gameRowNode.SelectSingleNode("td[2]/a").Attributes["href"].Value;
            int awayTeamId = Int32.Parse(HttpUtility.ParseQueryString(awayTeamLink).Get("teamid"));
            int season = Int32.Parse(HttpUtility.ParseQueryString(awayTeamLink).Get("season"));
            string awayTeamName = gameRowNode.SelectSingleNode("td[2]/a/img").Attributes["alt"].Value;
            int homeScore = Int32.Parse(gameRowNode.SelectSingleNode("td[5]").InnerText);
            string homeTeamLink = gameRowNode.SelectSingleNode("td[4]/a").Attributes["href"].Value;
            int homeTeamId = Int32.Parse(HttpUtility.ParseQueryString(homeTeamLink).Get("teamid"));
            string homeTeamName = gameRowNode.SelectSingleNode("td[4]/a/img").Attributes["alt"].Value;
            return new Game(awayTeamId, awayTeamName, awayScore, homeTeamId, homeTeamName, homeScore, week, season);
        }

        private static List<string> getWeeks(HtmlDocument doc, string path)
        {
            List<string> weeks = new List<string>();
            foreach(HtmlNode n in doc.DocumentNode.SelectNodes(path)) 
            {
                string s = n.SelectSingleNode("a/span").InnerText;
                Console.WriteLine(s);
                weeks.Add(s);
            }
            return weeks;
        }

        private static void login(CookieAwareWebClient client)
        {
            client.BaseAddress = @"https://www.profootballfocus.com/amember/";
            
            var loginData = new NameValueCollection();
            loginData.Add("amember_login", ConfigurationManager.AppSettings["pffUser"]);
            loginData.Add("amember_pass", ConfigurationManager.AppSettings["pffPass"]);
            
            client.UploadValues("member", "POST", loginData);            
        }
    }
}
