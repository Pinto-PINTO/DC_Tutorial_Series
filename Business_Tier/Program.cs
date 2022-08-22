using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Business_Tier
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Business Logic Server Initiated");


            ServiceHost host;
            NetTcpBinding tcp = new NetTcpBinding();


            host = new ServiceHost(typeof(BusinessServerImplementation));


            host.AddServiceEndpoint(typeof(BusinessServerInterface), tcp, "net.tcp://0.0.0.0:8200/BusinessService");

            host.Open();
            Console.WriteLine("System Online Waiting for Execution");
            Console.ReadLine();

            host.Close();
        }
    }
}
