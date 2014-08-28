using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using ScoreFramwork;

namespace ScoreDataSaverLib {
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface ISaverServiceContract {
        
        [OperationContract]
        bool SaveScore(List<Score> scoreList);

        // TODO: Add your service operations here
    }

    
}
