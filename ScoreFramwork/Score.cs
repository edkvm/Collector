using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ScoreFramwork {
    
    [DataContract]
    public class Score{
        
        [DataMember]
        public string HomeTeam { get; set;}

        [DataMember]
        public string AwayTeam { get; set;}

        [DataMember]
        public int HomeScore { get; set;}

        [DataMember]
        public int AwayScore { get; set;}

        [DataMember]
        public DateTime EventDate { get; set;}

        [DataMember]
        public string LeagueName { get; set;}



        public override string ToString (){

        return string.Format ("[Score: HomeTeam={0}, AwayTeam={1}, HomeScore={2}, AwayScore={3}, EventDate={4}, LeagueName={5}]", HomeTeam, AwayTeam, HomeScore, AwayScore, EventDate, LeagueName);

        }

    }

}
