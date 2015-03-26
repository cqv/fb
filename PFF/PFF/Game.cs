using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PFF
{
    class Game
    {
        int _awayTeamId;
        string _awayTeamName;
        int _awayTeamScore;
        int _homeTeamId;
        string _homeTeamName;
        int _homeTeamScore;
        int _week;
        int _season;

        public Game(int awayTeamId, string awayTeamName, int awayTeamScore, int homeTeamId, string homeTeamName, int homeTeamScore, int week, int season)
        {
            _awayTeamId = awayTeamId;
            _awayTeamName = awayTeamName;
            _awayTeamScore = awayTeamScore;
            _homeTeamId = homeTeamId;
            _homeTeamName = homeTeamName;
            _homeTeamScore = homeTeamScore;
            _week = week;
            _season = season;
        }
    }
}
