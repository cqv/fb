using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PFF
{
    class Game
    {
        int AwayTeamId;
        string AwayTeamName;
        int AwayTeamScore;
        int HomeTeamId;
        string HomeTeamName;
        int HomeTeamScore;
        string Week;
        int Season;

        public Game(int awayTeamId, string awayTeamName, int awayTeamScore, int homeTeamId, string homeTeamName, int homeTeamScore, string week, int season)
        {
            AwayTeamId = awayTeamId;
            AwayTeamName = awayTeamName;
            AwayTeamScore = awayTeamScore;
            HomeTeamId = homeTeamId;
            HomeTeamName = homeTeamName;
            HomeTeamScore = homeTeamScore;
            Week = week;
            Season = season;
        }


        // methods

        public static DataTable ToDataTable(List<Game> games) 
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("AwayTeamId", typeof(int)));
            dt.Columns.Add(new DataColumn("AwayTeamName", typeof(string)));
            dt.Columns.Add(new DataColumn("AwayTeamScore", typeof(int)));
            dt.Columns.Add(new DataColumn("HomeTeamId", typeof(int)));
            dt.Columns.Add(new DataColumn("HomeTeamName", typeof(string)));
            dt.Columns.Add(new DataColumn("HomeTeamScore", typeof(int)));
            dt.Columns.Add(new DataColumn("Week", typeof(string)));
            dt.Columns.Add(new DataColumn("Season", typeof(int)));

            foreach (Game g in games)
            {
                DataRow r = dt.NewRow();
                r["AwayTeamId"] = g.AwayTeamId;
                r["AwayTeamName"] = g.AwayTeamName;
                r["AwayTeamScore"] = g.AwayTeamScore;
                r["HomeTeamId"] = g.HomeTeamId;
                r["HomeTeamName"] = g.HomeTeamName; 
                r["HomeTeamScore"] = g.HomeTeamScore;
                r["Week"] = g.Week;
                r["Season"] = g.Season;
                dt.Rows.Add(r);
            }

            return dt;
        }
    }
}
