using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using ScoreFramwork;

namespace ScoreCollector {

    public sealed class ESPNSoccerCollector : BaseCollector {

        public static string SiteName = "http://www.espnfc.us/scores?date=20140830";

        public override List<Game> GetScore() {

            return _innerScore;

        }

        public override void BuildScore(string rawData) {

            Debug.WriteLine("Building Scores list");

            // Create document
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            
            // Load with raw string
            doc.LoadHtml(rawData);



            HtmlAgilityPack.HtmlNodeCollection leagueCollection = doc.DocumentNode.SelectNodes(ESPNSoccerCollectorConfig.LeaguePath);

            foreach (var league in leagueCollection)
            {
                var gameList = league.SelectNodes(league.XPath + "//div[contains(@class,'score-box')]");
                foreach (var rawGame in gameList)
                {
                    _innerScore.Add(BuildScore(rawGame, ""));
                }
               
            }



        }

        private Game BuildScore(HtmlAgilityPack.HtmlNode scoreNode, string league) {

            Game score = new Game();
            
            // Match date 
            //score.EventDate = GetDate(scoreNode);
            
            // League name 
            score.LeagueName = league;

            // Get Team names
            var teamNames = ParseTeamNames(scoreNode,scoreNode.XPath + ESPNSoccerCollectorConfig.TeamNamePath);
            
            // Get Game score
            var gameScore = ParseGameScore(scoreNode,scoreNode.XPath + ESPNSoccerCollectorConfig.ScorePath);
            
            // Get Game time
            var gameTime = ParseGameTime(scoreNode,scoreNode.XPath + ESPNSoccerCollectorConfig.GameTimePath);
            
            // Get Game start time
            var startTime = ParseStartTime(scoreNode, scoreNode.XPath + ESPNSoccerCollectorConfig.StartTimePath);
            
            // Get Game Status

            return score;


        }

        // Parse 
        public string[] ParseArrayValues(HtmlAgilityPack.HtmlNode node, string valuesPath) {
            var valuesArray = node.SelectNodes(valuesPath);
            
            string[] values = null;

            if (valuesArray != null && valuesArray.Count >= 2) {
                values = new string[2];
                for (int i = 0; i <= 1; i++) {
                    values[i] = valuesArray[i].InnerText.Trim();
                }
            }

            return values;

        }

        // Prase the team names
        public string[] ParseTeamNames(HtmlAgilityPack.HtmlNode node, string teamNamePath)
        {
            return ParseArrayValues(node, teamNamePath);
        }

        //
        public int[] ParseGameScore(HtmlAgilityPack.HtmlNode node, string teamScorePath) {
            int[] values = null;
            // Getting the raw string values from the page
            var rawValues = ParseArrayValues(node, teamScorePath);
            
            // Parsing the values into a int array
            if (rawValues != null && rawValues.Length >= 2) {
                values = new int[2];
                int i = 0;
                foreach (var rawValue in rawValues) {
                    if (int.TryParse(rawValue, out values[i])) {
                        i++;
                    } else {
                        // TODO: Add log
                        break;
                    }
                }
            } else {
                values = new int[] { -1, -1};
            }

            return values;
        }

        //
        public int ParseGameTime(HtmlAgilityPack.HtmlNode node, string gameTimePath) {
            var rawValue = node.SelectSingleNode(gameTimePath);
            
            int gameTime = -1;
            
            if (rawValue != null && !string.IsNullOrEmpty(rawValue.InnerText)) {
                if (!int.TryParse(rawValue.InnerText.Trim().Split('\'')[0], out gameTime)) {
                    gameTime = -1;
                    // TODO: Add logs
                }
            }

            return gameTime;
        }

        //
        public DateTime ParseStartTime(HtmlAgilityPack.HtmlNode node, string gameTimePath) {

            var rawValue = node.SelectSingleNode(gameTimePath);

            DateTime gameTime = DateTime.Today;

            if (rawValue != null && !string.IsNullOrEmpty(rawValue.InnerText)) {
                if (!DateTime.TryParse(rawValue.InnerText.Trim().Split(' ')[0], out gameTime)) {
                    gameTime = DateTime.Today.AddHours(16);
                    // TODO: Add logs
                }
            }

            return gameTime;
        }

        public int ParseGameStatus() {
            return 0;
        }

        public DateTime ParseGameStartTime() {
            return DateTime.Now;
        }
        

    }
}
