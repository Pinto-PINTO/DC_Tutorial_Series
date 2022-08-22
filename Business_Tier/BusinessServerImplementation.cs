using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Tutorial1_Server;

namespace Business_Tier
{
    internal class BusinessServerImplementation : BusinessServerInterface
    {

        public Data_Server_Interface foob;

        public BusinessServerImplementation()
        {
            ChannelFactory<Data_Server_Interface> foobFactory;
            NetTcpBinding tcp = new NetTcpBinding();
            string URL = "net.tcp://localhost:8100/DataService";

            // Making Connection with Data Server Interface
            foobFactory = new ChannelFactory<Data_Server_Interface>(tcp, URL);
            foob = foobFactory.CreateChannel();
        }


        public int GetNumEntries()
        {
            return foob.GetNumEntries();
        }

        public void GetValuesForEntry(int index, out uint acctNo, out uint pin, out int bal, out string fName, out string lName, out string img)
        {
            foob.GetValuesForEntry(index, out acctNo, out pin, out bal, out fName, out lName, out img);
        }
    }
}
