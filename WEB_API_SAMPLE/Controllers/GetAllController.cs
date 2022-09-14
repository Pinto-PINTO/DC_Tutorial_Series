using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Web.Http;
using Tutorial1_Server;
using API_Classes;
using System.ServiceModel;

namespace WEB_API_SAMPLE.Controllers
{
    public class GetAllController : ApiController
    {
        
        public Data_Server_Interface foob;


        // GET request
        public string Get()
        {
            SetDataServerInstance();
            return foob.GetNumEntries().ToString();
        }



        public DataIntermed Get(int id)
        {

            SetDataServerInstance();

            int index = 0;
            string fname = "";
            string lname = "";
            int bal = 0;
            uint acctNo = 0;
            uint pin = 0;
            string img = "";


            foob.GetValuesForEntry(id, out acctNo, out pin, out bal, out fname, out lname, out img);


            DataIntermed client = new DataIntermed();

            client.bal = bal;
            client.fname = fname;
            client.lname = lname;
            client.acctNo = acctNo;
            client.pin = pin;
            client.img = img;




            return client;
        }


        private void SetDataServerInstance()
        {
            ChannelFactory<Data_Server_Interface> foobFactory;



            NetTcpBinding tcp = new NetTcpBinding();

            string URL = "net.tcp://localhost:8100/DataService";



            //Establishing connection and getting the client count

            foobFactory = new ChannelFactory<Data_Server_Interface>(tcp, URL);

            foob = foobFactory.CreateChannel();
        }
    }
}