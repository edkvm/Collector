using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScoreFramwork;
using ScoreDataSaverLib.DAL;

namespace ScoreCollector.Test {
    class Program {
        static void Main(string[] args) {
            Game s = new Game {
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

        public static void TestDB(Game score){
            ApplicationDAL.InsertScoreList(new List<Game>() { score });
        }

        public static void TestService(Game score) {
            SaverReference.SaverServiceContractClient sc = new SaverReference.SaverServiceContractClient();
            if (sc.SaveScore(new Game[] { score })) {
                Console.WriteLine("Yeah!");
            }
        }
    }
}
