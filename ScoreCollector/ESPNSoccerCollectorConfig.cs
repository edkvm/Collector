using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScoreCollector {

    public static class ESPNSoccerCollectorConfig {

        public static string TeamNamePath {
            get {
                return "//div[@class='team-name']";
            }
        }

        public static string ScorePath {
            get {
                return "//div[@class='team-score']";
            }
        }

        public static string GameTimePath {
            get {
                return "//div[@class='game-info']/span[@class='time']";
            }
        }

        public static string StartTimePath {
            get {
                return "//div[@class='game-info']/span[contains(@class,'time')]";
            }
        }

        public static string LeaguePath {
            get {
                return "//div[contains(@class,'score-league')]/.";
            }
        }
    }
}
