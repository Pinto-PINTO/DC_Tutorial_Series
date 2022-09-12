using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tutorial1_Server;

namespace Business_Tier
{
    internal class BusinessServerImplementation : BusinessServerInterface
    {

        public Data_Server_Interface foob;

        uint AccessLogValue;

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
            try
            {
                return foob.GetNumEntries();
            }
            finally
            {
                AccessLog("Number of records in Database: " + foob.GetNumEntries());
            }
            
        }

        public void GetValuesForEntry(int index, out uint acctNo, out uint pin, out int bal, out string fName, out string lName, out string img)
        {
            foob.GetValuesForEntry(index, out acctNo, out pin, out bal, out fName, out lName, out img);
            AccessLog("FINDING data of index: " + index);
        }

        public void GetValuesForSearch(string searchText, out uint acctNo, out uint pin, out int bal, out string fName, out string lName, out string img)
        {
            fName = null;
            lName = null;
            acctNo = 0;
            pin = 0;
            bal = 0;
            img = null;
            bool searchFound = false;
            int numEntry = foob.GetNumEntries();

            AccessLog("Waiting for search results ....");

            for (int i = 1; i <= numEntry; i++)
            {
                uint SacctNo;
                uint Spin;
                int Sbal;
                string SfName;
                string SlName;
                string Simg;

                foob.GetValuesForEntry(i, out SacctNo, out Spin, out Sbal, out SfName, out SlName, out Simg);

                if (SlName.ToLower().Contains(searchText.ToLower()))
                {
                    AccessLog("SEARCHING for last name " + SlName + ", found on index " + i );

                    acctNo = SacctNo;
                    pin = Spin;
                    bal = Sbal;
                    fName = SfName;
                    lName = SlName;
                    img = Simg;
                    searchFound = true;
                    break;

                }
                

            }

            if (searchFound == false)
            {
                Console.WriteLine("Search not found");
            }

            Thread.Sleep(5000);
        }

        // Implementing the Log
        [MethodImpl(MethodImplOptions.Synchronized)]
        void AccessLog(string logString)
        {
            AccessLogValue++;
            Console.WriteLine("----------------------------------------------------");
            Console.WriteLine("Action: " + AccessLogValue);
            Console.WriteLine(logString);
        }
    }
}   
