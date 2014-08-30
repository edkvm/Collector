using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ScoreFramwork {
    
    
    public class Game{
        
        
        public string[] Teams { get; set;}

        public int[][] Score { get; set;}

        public int GameTime { get; set; }

        public DateTime StartTime { get; set; }

        public int Status { get; set; }

        public DateTime CeatedAt { get; set;}

        public string LeagueName { get; set;}

        public Game() {
            Score = new int[6][2];
        }

        public override string ToString (){

        return string.Format ("[Score: HomeTeam={0}, AwayTeam={1}, HomeScore={2}, AwayScore={3}, EventDate={4}, LeagueName={5}]", HomeTeam, AwayTeam, HomeScore, AwayScore, EventDate, LeagueName);

        }

    }

}
