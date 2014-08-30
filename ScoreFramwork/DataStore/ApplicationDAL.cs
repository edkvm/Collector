using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScoreFramwork;

namespace ScoreDataSaverLib.DAL {
    public static class ApplicationDAL {

        public static string ConnectionString { get; set; }
        public static void InsertScoreList(List<Game> listScore) {

           /* using (ScoresDALDataContext ctx = new ScoresDALDataContext(ConnectionString)) {
                Match match = null;
                
                try {
                    
                    foreach (var score in listScore) {
                        match = TranslateScoreToMatch(score);
                        

                        

                        if (match != null) {
                            
                            ctx.Matches.InsertOnSubmit(match);
                            

                            
                        }
                    }

                    ctx.SubmitChanges();

                } catch (Exception ex) {
                    
                }

                
                
            }*/
        }

        public static void InsertScoreData(Game score) {

           /* using (ScoresDALDataContext ctx = new ScoresDALDataContext(ConnectionString)) {
                Match match = null;

                try {
                     
                    match = TranslateScoreToMatch(score);
                        
                    if (match != null) {
    
                        ctx.Matches.InsertOnSubmit(match);
                        
                    }
                    
                    ctx.SubmitChanges();

                } catch (Exception ex) {

                }



            }*/
        }

        //private static Match TranslateScoreToMatch(Score score) {
        //    Match match = new Match();

        //    match.AwayScore = score.AwayScore;
        //    match.AwayTeam = score.AwayTeam;
        //    match.Field = "soccer";
        //    match.HomeScore = score.HomeScore;
        //    match.HomeTeam = score.HomeTeam;
        //    match.LastUpdate = DateTime.Now;
        //    match.League = score.LeagueName;
        //    match.MatchDate = score.EventDate;

        //    return match;
        //}
    }
}
