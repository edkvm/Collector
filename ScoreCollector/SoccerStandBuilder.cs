using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace ScoreFramwork {
    public sealed class SoccerStandBuilder : ScoreBuilder {

        public static string SiteName = "http://scores.espn.go.com/nba/scoreboard?date=20140312";



        public override List<Score> GetScore() {

            return _innerScore;

        }

        public override void BuildScore(string rawData) {

            Debug.WriteLine("Building Scores list");

            // Create document
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            
            // Load with raw string
            doc.LoadHtml(rawData);



            HtmlAgilityPack.HtmlNodeCollection gameCollection = doc.DocumentNode.SelectNodes("//div[contains(@id,'gamebox')]/.");

            foreach (var rawGame in gameCollection)
            {
               _innerScore.Add(BuildScore(rawGame, ""));
               
            }



        }

        private Score BuildScore(HtmlAgilityPack.HtmlNode scoreNode, string league) {

            Score score = new Score();
            
            // Match date 
            //score.EventDate = GetDate(scoreNode);
            
            // League name 
            score.LeagueName = league;
            
            // Get Home Team
            score.HomeTeam = scoreNode.SelectSingleNode(scoreNode.XPath + "//p[contains(@id,'hNameOffset')]").InnerText.Trim();
            
            // Get Away Team
            score.AwayTeam = scoreNode.SelectSingleNode(scoreNode.XPath + "//p[contains(@id,'aNameOffset')]").InnerText.Trim();
            
            // Get Home Team score
            int parsedScore = -1;

            if (!int.TryParse(scoreNode.ChildNodes[8].ChildNodes[1].InnerText.Trim(), out parsedScore)) {
                parsedScore = -1;
            }
            
            score.HomeScore = parsedScore;

            if (!int.TryParse(scoreNode.ChildNodes[8].ChildNodes[3].InnerText.Trim(), out parsedScore)) {
                parsedScore = -1;
            }
            
            // Get Away Team score
            score.AwayScore = parsedScore;
            
            return score;


        }



        private DateTime GetDate(HtmlAgilityPack.HtmlNode scoreNode) {
            
            DateTime date = DateTime.MinValue;
            string rawDate = "";
            
            try{
                rawDate = scoreNode.ChildNodes[5].ChildNodes[1].GetAttributeValue("data-date", "");
            } catch(Exception ex){
                rawDate = scoreNode.ChildNodes[1].ChildNodes[1].GetAttributeValue("data-date", "");
            }
            if (!DateTime.TryParse(rawDate, out date)) {
                date = DateTime.MinValue;
            }
            
            return date;

        }

    }
}
