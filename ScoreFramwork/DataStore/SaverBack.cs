using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Concurrent;
using ScoreFramwork;
using System.Threading;
using ScoreDataSaverLib.DAL;

namespace ScoreDataSaverLib {
    
    public class SaverBack {

        private ConcurrentQueue<List<Game>> _inputQueue;
        private Thread _backgroundWorker;
        private ManualResetEvent _signalOnData;
        public ConcurrentDictionary<string, Game> _cache = new ConcurrentDictionary<string,Game>();
        
        public SaverBack(ConcurrentQueue<List<Game>> inputQueue) {
            _inputQueue = inputQueue;
            _backgroundWorker = new Thread(new ThreadStart(RunLoop));
            _signalOnData = new ManualResetEvent(true);

        }

        public void Start() {
            if (_inputQueue == null) {
                throw new NullReferenceException("inputQueue must be defined");
            } else {
               _backgroundWorker.Start();
            }
        }

        public void Consume() {
            _signalOnData.Set();
        }

        private void RunLoop() {

            List<Game> list = null;
            while (true) {
                _signalOnData.WaitOne();
                if (_inputQueue.Count <= 0) {
                    _signalOnData.Reset();
                }

                if (_inputQueue.TryDequeue(out list)) {
                    
                    if (list != null) {
                        
                        ApplicationDAL.InsertScoreList(list);
                        
                    } else {
                        // TODO: Log Error
                    }

                } else {
                    // TODO: Log Error
                }

            }
        }   
    }
}
