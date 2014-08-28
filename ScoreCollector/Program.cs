using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Net;
using ScoreFramwork;
using System.Diagnostics;

namespace ScoreCollector {
    class Program {

       
        static void Main(string[] args) {


            DataCollector dt = new DataCollector(1000); //AppConfig.CheckInterval);
            dt.OnNewData += new NewDate(dt_OnNewData);
            Console.WriteLine("Starting service");
            dt.Start();
            

            Console.WriteLine("Press any key to terminate...");
            Console.ReadLine();
                
        }

        static void dt_OnNewData(List<Score> data) {
            Debug.WriteLine("Building Scores list");
			Console.WriteLine ("Data recived:{0}",data.Count);;
        }

       
    }
}
