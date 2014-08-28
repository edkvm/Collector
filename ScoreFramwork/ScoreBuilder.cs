using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScoreFramwork {
    public abstract class ScoreBuilder {

        protected List<Score> _innerScore;

        public ScoreBuilder() {

            _innerScore = new List<Score>();

        }
        
        public abstract List<Score> GetScore();

        public abstract void BuildScore(string rawData);

    }


}
