using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using DLL_Project_Tutorial1;

namespace Tutorial1_Server
{
    [ServiceBehavior (ConcurrencyMode = ConcurrencyMode.Multiple, UseSynchronizationContext = false)]
    internal class Data_Server : Data_Server_Interface {

        public Data_Server()
        {

        }
        public int GetNumEntries()
        {
            var databaseInstance = new Database();
            return databaseInstance.GetNumRecords();
        }
        public void GetValuesForEntry(int index, out uint acctNo, out uint pin, out int bal, out string fName, out string lName, out string img)
        {

            var databaseInstance = new Database();
            fName = databaseInstance.GetFirstNameByIndex(index);
            lName = databaseInstance.GetLastNameByIndex(index);
            img = databaseInstance.GetImgByIndex(index);
            acctNo = databaseInstance.GetAcctNoByIndex(index);
            pin = databaseInstance.GetPinByIndex(index);
            bal = databaseInstance.GetBalanceByIndex(index);

        }

    }
    
}
