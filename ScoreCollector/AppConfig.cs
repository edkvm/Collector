using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace ScoreCollector {
    public static class AppConfig {

        private static string _env;
        static AppConfig() {

            try {
                _env = ConfigurationManager.AppSettings["ENV"].ToString();
            } catch (Exception ex) {
                // TODO: log
            }


        }

        public static int CheckInterval {
            get {
                int interval = 10000;

                try {
                    if (int.TryParse(ConfigurationManager.AppSettings["Port"].ToString(), out interval)) {
                        interval = 10000;
                    }
                } catch (Exception ex) {
                    // TODO: log
                    interval = 10000;
                }

                return interval;
            }
        }

        
    }
}
