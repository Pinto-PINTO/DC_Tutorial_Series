using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Business_Tier
{
    [ServiceContract]
    public interface BusinessServerInterface
    {
        [OperationContract]
        [FaultContract(typeof(Exception))]
        int GetNumEntries();

        [OperationContract]
        [FaultContract(typeof(Exception))]
        void GetValuesForEntry(int index, out uint acctNo, out uint pin, out int bal, out string fName, out string lName, out string img);
    }
}
