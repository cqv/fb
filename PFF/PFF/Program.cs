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

namespace PFF
{
    class Program
    {
        static void Main(string[] args)
        {
            //// login

            //Console.WriteLine("Logging in...");
            //var client = new CookieAwareWebClient();
            //login(client);
            
            
            //// download page

            //Console.WriteLine("Downloading Rodgers W1 Passing Page");
            //string url = @"https://www.profootballfocus.com/data/by_week.php?tab=by_week";
            string pffDataDirectory = ConfigurationManager.AppSettings["pffDataDirectory"];
            string localPath = pffDataDirectory + @"\by_week.html";
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





            // close

            Console.WriteLine("\nPress enter to exit");
            Console.Read();
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
            loginData.Add("amember_login", ConfigurationManager.AppSettings["user"]);
            loginData.Add("amember_pass", ConfigurationManager.AppSettings["pass"]);
            
            client.UploadValues("member", "POST", loginData);            
        }
    }
}
