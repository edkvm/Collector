using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace ScoreSaver {
    public static class AppConfig {

        private static string _env;
        static AppConfig() {

            try {
                _env = ConfigurationManager.AppSettings["ENV"].ToString();
            } catch (Exception ex) {
                // TODO: log
            }


        }

        public static String Port {
            get {
                string port = "";

                try {
                    port = ConfigurationManager.AppSettings["Port"].ToString();
                } catch (Exception ex) {
                    // TODO: log
                    port = "9090";
                }

                return port;
            }
        }

        public static String ConnectionString {
            get {
                
                // No connection service should fail to start
                return ConfigurationManager.ConnectionStrings["ScoresDB_" + _env].ToString();
            }
        }
    }
}
