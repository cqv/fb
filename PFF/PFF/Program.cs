using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MyScraper;
using System.Collections.Specialized;
using System.Configuration;

namespace PFF
{
    class Program
    {
        static void Main(string[] args)
        {
            // login

            Console.WriteLine("Logging in...");
            var client = new CookieAwareWebClient();
            client.BaseAddress = @"https://www.profootballfocus.com/amember/";
            var loginData = new NameValueCollection();
            
            loginData.Add("amember_login", ConfigurationManager.AppSettings["user"]);
            loginData.Add("amember_pass", ConfigurationManager.AppSettings["pass"]);
            client.UploadValues("member", "POST", loginData);
            

            // download page

            Console.WriteLine("Downloading Rodgers W1 Passing Page");
            string url = @"https://www.profootballfocus.com/data/gstats.php?tab=by_week&season=2014&gameid=3217&teamid=1&stats=p&playerid=";
            string localPath = @"C:\Users\Ian\Documents\PFF\STL@ARI - QB.html";
            client.DownloadFile(url, localPath);
            

            // Count dropdown



            
            // close

            Console.WriteLine("\nPress enter to exit");
            Console.Read();
        }
    }
}
