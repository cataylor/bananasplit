using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BananaSplit.Models;
using HtmlAgilityPack;

namespace BananaSplit.Helpers
{
    public static class NbaSeasonParser
    {

        private static void ParseNBASeason(int year)
        {
            var html = new HtmlWeb();
            var teamLinks = GetAllTeamLinks();

            foreach (var team in teamLinks)
            {
                HtmlAgilityPack.HtmlDocument doc = html.Load(team.Url);
                //var node = doc.DocumentNode.SelectSingleNode("//*[@summary='Maximum Monthly Allowance']");

                foreach (var tableNode in doc.DocumentNode.SelectNodes("table border=0"))
                {
                    HtmlNodeCollection allTRTags = tableNode.SelectNodes(".//tr");
                    foreach (var tr in allTRTags)
                    {

                        var time = String.Empty;
                        var opponent = String.Empty;
                        var counter = 0;
                        foreach (var td in tr.SelectNodes(".//td"))
                        {
                            ++counter;
                            if (counter == 1)
                            {
                                continue; //already got date
                            }
                            else if (counter == 2)
                            {
                                var opponentText = td.SelectSingleNode(".//font");
                                opponent = opponentText.InnerHtml;

                            }
                            else if (counter == 3)
                            {
                                var timeText = td.SelectSingleNode(".//font");
                                time = timeText.InnerHtml;
                            }
                        }

                        var bold = tr.SelectSingleNode(".//b");
                        var partialDate = bold.InnerHtml;
                        var fullDate = parseSeasonDate(partialDate, time, year);

                        /*
                        
    <tr>
        <td><font class=verdana size=2><b> Date </b></font></td>

        <td><font class=verdana size=2><b> Opponent </b></font></td>

        <td><font class=verdana size=2><b> Time</b></font></td>

    </tr>

<tr>

        <td><font class=verdana size=1><b>Oct. 31</b></font></td>
     
        <td><font class=verdana size=1>at Detroit</font></td>
        
        <td align=right><font class=verdana size=1>7:30</font></td>
</tr>

                        */

                        //Pull Team
                        //Pull Date <td><font class=verdana size=1><b>Oct. 31</b></font></td>
                        //Pull Oppponent <td><font class=verdana size=1>at Detroit</font></td>
                        //Pull Time <td align=right><font class=verdana size=1>7:30</font></td>                        
                    }
                }


            }

        }

        private static DateTime parseSeasonDate(string monthDay, string time, int baseYear = 2013)
        {
            var monthDaySplit = monthDay.Split(" ".ToCharArray());
            var month = monthDaySplit[0];
            var day = monthDaySplit[1];
            var year = baseYear;

            if (month.Equals("Oct.", StringComparison.OrdinalIgnoreCase)
                || month.Equals("Nov.", StringComparison.OrdinalIgnoreCase)
                || month.Equals("Dec.", StringComparison.OrdinalIgnoreCase))
            {
                year = baseYear - 1;
            }

            var monthString = ConvertMonthToString(month);
            var fullDateString = String.Format("{0}/{1}/{2}", monthString, day, year);
            return DateTime.Parse(fullDateString);
        }

        private static String ConvertMonthToString(string month)
        {
            var convertedMonth = String.Empty;

            switch (month.ToLower())
            {
                case "jan.":
                    convertedMonth = "01";
                    break;
                case "feb.":
                    convertedMonth = "02";
                    break;
                case "mar.":
                    convertedMonth = "03";
                    break;
                case "apr.":
                    convertedMonth = "04";
                    break;
                case "may":
                    convertedMonth = "05";
                    break;
                case "jun.":
                    convertedMonth = "06";
                    break;
                case "jul.":
                    convertedMonth = "07";
                    break;
                case "aug.":
                    convertedMonth = "08";
                    break;
                case "sep.":
                    convertedMonth = "09";
                    break;
                case "oct.":
                    convertedMonth = "10";
                    break;
                case "nov.":
                    convertedMonth = "11";
                    break;
                case "dec.":
                    convertedMonth = "12";
                    break;
            }

            return convertedMonth;
        }


        private static List<TeamLink> GetAllTeamLinks()
        {
            var teamLinks = new List<TeamLink>();

            teamLinks.Add(new TeamLink() { Url = "http://espn.go.com/nba/teams/printSchedule?team=atl&season=2013", TeamLocation = "Atlanta Hawks" });
            teamLinks.Add(new TeamLink() { Url = "http://espn.go.com/nba/teams/printSchedule?team=bos&season=2013", TeamLocation = "Boston Celtics" });
            teamLinks.Add(new TeamLink() { Url = "http://espn.go.com/nba/teams/printSchedule?team=bkn&season=2013", TeamLocation = "Brooklyn Nets" });
            teamLinks.Add(new TeamLink() { Url = "http://espn.go.com/nba/teams/printSchedule?team=cha&season=2013", TeamLocation = "Charlotte Bobcats" });
            teamLinks.Add(new TeamLink() { Url = "http://espn.go.com/nba/teams/printSchedule?team=chi&season=2013", TeamLocation = "Chicago Bulls" });
            teamLinks.Add(new TeamLink() { Url = "http://espn.go.com/nba/teams/printSchedule?team=cle&season=2013", TeamLocation = "Cleveland Cavaliers" });
            teamLinks.Add(new TeamLink() { Url = "http://espn.go.com/nba/teams/printSchedule?team=dal&season=2013", TeamLocation = "Dallas Mavericks" });
            teamLinks.Add(new TeamLink() { Url = "http://espn.go.com/nba/teams/printSchedule?team=den&season=2013", TeamLocation = "Denver Nuggets" });
            teamLinks.Add(new TeamLink() { Url = "http://espn.go.com/nba/teams/printSchedule?team=det&season=2013", TeamLocation = "Detroit Pistons" });
            teamLinks.Add(new TeamLink() { Url = "http://espn.go.com/nba/teams/printSchedule?team=gs&season=2013", TeamLocation = "Golden State Warriors" });
            teamLinks.Add(new TeamLink() { Url = "http://espn.go.com/nba/teams/printSchedule?team=hou&season=2013", TeamLocation = "Houston Rockets" });
            teamLinks.Add(new TeamLink() { Url = "http://espn.go.com/nba/teams/printSchedule?team=ind&season=2013", TeamLocation = "Indiana Pacers" });
            teamLinks.Add(new TeamLink() { Url = "http://espn.go.com/nba/teams/printSchedule?team=lac&season=2013", TeamLocation = "Los Angeles Clippers" });
            teamLinks.Add(new TeamLink() { Url = "http://espn.go.com/nba/teams/printSchedule?team=lal&season=2013", TeamLocation = "Los Angeles Lakers" });
            teamLinks.Add(new TeamLink() { Url = "http://espn.go.com/nba/teams/printSchedule?team=mem&season=2013", TeamLocation = "Memphis Grizzlies" });
            teamLinks.Add(new TeamLink() { Url = "http://espn.go.com/nba/teams/printSchedule?team=mia&season=2013", TeamLocation = "Miami Heat" });
            teamLinks.Add(new TeamLink() { Url = "http://espn.go.com/nba/teams/printSchedule?team=mil&season=2013", TeamLocation = "Milwaukee Bucks" });
            teamLinks.Add(new TeamLink() { Url = "http://espn.go.com/nba/teams/printSchedule?team=min&season=2013", TeamLocation = "Minnesota Timberwolves" });
            teamLinks.Add(new TeamLink() { Url = "http://espn.go.com/nba/teams/printSchedule?team=no&season=2013", TeamLocation = "New Orleans Hornets" });
            teamLinks.Add(new TeamLink() { Url = "http://espn.go.com/nba/teams/printSchedule?team=ny&season=2013", TeamLocation = "New York Knicks" });
            teamLinks.Add(new TeamLink() { Url = "http://espn.go.com/nba/teams/printSchedule?team=okc&season=2013", TeamLocation = "Oklahoma City Thunder" });
            teamLinks.Add(new TeamLink() { Url = "http://espn.go.com/nba/teams/printSchedule?team=orl&season=2013", TeamLocation = "Orlando Magic" });
            teamLinks.Add(new TeamLink() { Url = "http://espn.go.com/nba/teams/printSchedule?team=phi&season=2013", TeamLocation = "Philadelphia 76ers" });
            teamLinks.Add(new TeamLink() { Url = "http://espn.go.com/nba/teams/printSchedule?team=phx&season=2013", TeamLocation = "Phoenix Suns" });
            teamLinks.Add(new TeamLink() { Url = "http://espn.go.com/nba/teams/printSchedule?team=por&season=2013", TeamLocation = "Portland Trail Blazers" });
            teamLinks.Add(new TeamLink() { Url = "http://espn.go.com/nba/teams/printSchedule?team=sac&season=2013", TeamLocation = "Sacramento Kings" });
            teamLinks.Add(new TeamLink() { Url = "http://espn.go.com/nba/teams/printSchedule?team=sa&season=2013", TeamLocation = "San Antonio Spurs" });
            teamLinks.Add(new TeamLink() { Url = "http://espn.go.com/nba/teams/printSchedule?team=tor&season=2013", TeamLocation = "Toronto Raptors" });
            teamLinks.Add(new TeamLink() { Url = "http://espn.go.com/nba/teams/printSchedule?team=utah&season=2013", TeamLocation = "Utah Jazz" });
            teamLinks.Add(new TeamLink() { Url = "http://espn.go.com/nba/teams/printSchedule?team=wsh&season=2013", TeamLocation = "Washington Wizards" });

            return teamLinks;
        }
    }

}




/*



*/