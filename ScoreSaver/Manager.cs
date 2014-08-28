using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using ScoreDataSaverLib;
using System.ServiceModel.Description;
using System.Collections.Concurrent;
using ScoreFramwork;
using System.Configuration;
using ScoreDataSaverLib.DAL;

namespace ScoreSaver {
    
    public class Manager {

        private ServiceHost _host;
        private ConcurrentQueue<List<Score>> _dataQueue;
        private SaverBack _dataSaver;

        public Manager(string port) {

            // Create a queue
            ConcurrentQueue<List<Score>> dataQueue = new ConcurrentQueue<List<Score>>();

            // Create the WCF Interface
            SaverFront front = new SaverFront(dataQueue);
            
            // Create the back worker that saves the data
            _dataSaver = new SaverBack(dataQueue);

            // Bind an event from the front to the back to indicate new data
            front.OnNewData += new Notify(front_OnNewData); 

            _host = StartHosting(front, port);
            
            _host.Faulted += new EventHandler(_host_Faulted);

            ApplicationDAL.ConnectionString = AppConfig.ConnectionString;
        }

        void front_OnNewData() {
            _dataSaver.Consume();
        }

        void _host_Faulted(object sender, EventArgs e) {
            // TODO: 1. log
            //       2. disaster recovery
            throw new NotImplementedException();
        }



        public void Start() {
            _dataSaver.Start();
            _host.Open();
        }

        public void LoadConfiguration() {
            ExeConfigurationFileMap map = new ExeConfigurationFileMap();
            //map.ExeConfigFilename = 
        }

        public ServiceHost StartHosting(ISaverServiceContract front,string port) {

            // Declare Binding
            BasicHttpBinding httpBinding = new BasicHttpBinding();
            
            // 
            string endpointAddress = "http://localhost:" + port + "/Service";

            // Create host for the sevice to run in
            ServiceHost host = new ServiceHost(front, new Uri[] { new Uri(endpointAddress) });
            
            // Add an endpoint
            host.AddServiceEndpoint(typeof(ISaverServiceContract), httpBinding, "");
            
            AddMetaData(host);
            

            return host;

        }

        private void AddMetaData(ServiceHost host) {
            
            // Add metadata
            ServiceMetadataBehavior smb = host.Description.Behaviors.Find<ServiceMetadataBehavior>();

            // If not, add one
            if (smb == null) {
                smb = new ServiceMetadataBehavior();
            }

            smb.HttpGetEnabled = true;
            smb.MetadataExporter.PolicyVersion = PolicyVersion.Policy15;

            host.Description.Behaviors.Add(smb);

            host.AddServiceEndpoint(
                ServiceMetadataBehavior.MexContractName,
                MetadataExchangeBindings.CreateMexHttpBinding(),
                "mex"
            );
        }
    }
}
