using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScoreFramwork {

    public abstract class BaseCollector {

        protected List<Game> _innerScore;

        public BaseCollector() {

            _innerScore = new List<Game>();

        }
        
        public abstract List<Game> GetScore();

        public abstract void BuildScore(string rawData);

    }


}
