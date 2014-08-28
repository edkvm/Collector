using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScoreDataSaverLib;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Collections.Concurrent;
using ScoreFramwork;

namespace ScoreSaver {
    class Program {

        
        static void Main(string[] args) {

            Manager _manager = new Manager(AppConfig.Port);

            _manager.Start();

            Console.WriteLine("Press any key to terminate...");
            Console.ReadLine();

        }

    

        
    }
}
