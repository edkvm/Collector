using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScoreFramwork;
using System.Net;

namespace ScoreCollector {

    public class DataCollector {


        private System.Timers.Timer _syncDataWithSite;
        public event NewDate OnNewData = delegate { };
        public DataCollector(int syncInterval) {

            _syncDataWithSite = new System.Timers.Timer();
            _syncDataWithSite.Interval = syncInterval;
            _syncDataWithSite.Elapsed += new System.Timers.ElapsedEventHandler(_syncDataWithSite_Elapsed);
        
        }

        public void Start() {
            _syncDataWithSite.Start();
        }
        void _syncDataWithSite_Elapsed(object sender, System.Timers.ElapsedEventArgs e) {
            
            _syncDataWithSite.Stop();
            
            try {
                List<Game> data = ParseData(DateTime.Now);
                OnNewData(data);
            } catch (Exception ex) {
                // TODO: log
            } finally {
                _syncDataWithSite.Start();
            }
        }

        public static List<Game> ParseData(DateTime date) {

            ESPNSoccerCollector sb = new ESPNSoccerCollector();

            sb.BuildScore(GetDataFromSite(ESPNSoccerCollector.SiteName, "soccer", date));

            return sb.GetScore();

        }


        public static string GetDataFromSite(string basePath, string sportFiled, DateTime date) {

            DateTime currentDate = DateTime.Now;

            string dataDate = "";

            if (date.Date > currentDate.Date) {

                dataDate = "d" + (date.Day - currentDate.Day).ToString();

            } else if (date.Date < DateTime.Now.Date) {

                dataDate = "d-" + (currentDate.Day - date.Day).ToString();

            } else {

                dataDate = "";

            }

            WebClient client = new WebClient();

            string sitePath = basePath + sportFiled + "/" + dataDate;

            return client.DownloadString(basePath);
        }
    }
}
