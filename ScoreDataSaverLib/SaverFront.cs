using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using ScoreFramwork;
using System.Collections.Concurrent;

namespace ScoreDataSaverLib {
    
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    [ServiceBehavior(InstanceContextMode=InstanceContextMode.Single)]
    public class SaverFront : ISaverServiceContract {

        private ConcurrentQueue<List<Score>> _outputQueue;

        public event Notify OnNewData = delegate { };

        public SaverFront(ConcurrentQueue<List<Score>> outputQueue) {

            _outputQueue = outputQueue;

            if (_outputQueue == null) {
                throw new NullReferenceException("Output queue must not be null");
            }

        }

        public bool SaveScore(List<Score> scoreList) {
            
            // Insert data to queue
            _outputQueue.Enqueue(scoreList);
            
            // Notify
            OnNewData();
            
            return true;
        }
    }
}
