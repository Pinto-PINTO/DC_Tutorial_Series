using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace Tutorial1_Server
{
    internal class Server
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Welcome to my Server");
  
            ServiceHost host;
  
            NetTcpBinding tcp = new NetTcpBinding();

            host = new ServiceHost(typeof(Data_Server));

            host.AddServiceEndpoint(typeof(Data_Server_Interface), tcp, "net.tcp://localhost:8100/DataService");

            host.Open();

            Console.WriteLine("System Online");
            Console.ReadLine();

            host.Close();
        }
    }
}
