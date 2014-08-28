using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace ScoreFramwork {
    public sealed class SoccerStandBuilder : ScoreBuilder {

        public static string SiteName = "http://www.soccerstand.com/livescore/";



        public override List<Score> GetScore() {

            return _innerScore;

        }

        public override void BuildScore(string rawData) {

            Debug.WriteLine("Building Scores list");

            // Create document
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            
            // Load with raw string
            doc.LoadHtml(rawData);

            // Extract body
            HtmlAgilityPack.HtmlNode body = doc.DocumentNode.SelectSingleNode("//body");
            
            HtmlAgilityPack.HtmlNodeCollection matchCollection = body.SelectNodes("//div[starts-with(@class,'league_container soccer is_international')]");

            foreach (var m in matchCollection) {
                string league = "";
                // League name
                try {
                    league = m.ChildNodes[1].ChildNodes[0].SelectSingleNode("./text()[normalize-space()]").InnerText.Trim();
                } catch(Exception ex){
                    league = m.ChildNodes[0].ChildNodes[0].SelectSingleNode("./text()[normalize-space()]").InnerText.Trim();
                }

                HtmlAgilityPack.HtmlNodeCollection gameCollection = null;
                try {
                    gameCollection = m.ChildNodes[3].SelectNodes("./table/tbody/tr");
                } catch (Exception ex) {
                    gameCollection = m.ChildNodes[1].SelectNodes("./table/tbody/tr");
                }
                
                for (int j = 0; j < gameCollection.Count; j++) {
                    _innerScore.Add(BuildScore(gameCollection[j], league));
                }
            }



        }

        private Score BuildScore(HtmlAgilityPack.HtmlNode scoreNode, string league) {



            Score score = new Score();
            
            // Match date 
            score.EventDate = GetDate(scoreNode);
            
            // League name 
            score.LeagueName = league;
            
            // Get Home Team
            score.HomeTeam = scoreNode.ChildNodes[7].ChildNodes[1].SelectSingleNode("./text()[normalize-space()]").InnerText.Trim();
            
            // Get Away Team
            score.AwayTeam = scoreNode.ChildNodes[9].ChildNodes[0].SelectSingleNode("./text()[normalize-space()]").InnerText.Trim();
            
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
