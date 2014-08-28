using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScoreFramwork;
using ScoreDataSaverLib.DAL;

namespace ScoreCollector.Test {
    class Program {
        static void Main(string[] args) {
            Score s = new Score {
                AwayScore = 1,
                EventDate = DateTime.Now,
                LeagueName = "England",
                AwayTeam = "",
                HomeScore = 1,
                HomeTeam = "sd"
            };

            TestDB(s);
            TestService(s);
            Console.ReadLine();

        }

        public static void TestDB(Score score){
            ApplicationDAL.InsertScoreList(new List<Score>() { score });
        }

        public static void TestService(Score score) {
            SaverReference.SaverServiceContractClient sc = new SaverReference.SaverServiceContractClient();
            if (sc.SaveScore(new Score[] { score })) {
                Console.WriteLine("Yeah!");
            }
        }
    }
}
