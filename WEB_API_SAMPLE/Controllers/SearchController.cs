﻿using API_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.Http;
using Tutorial1_Server;

namespace WEB_API_SAMPLE.Controllers
{
    public class SearchController : ApiController
    {
        public Data_Server_Interface foob;


        public DataIntermed Post([FromBody] SearchData searchText)
        {
            Data_Server_Initilization();

            // VARIABLE INITILIZATION
            int index = 0;
            string fName = "";
            string lName = "";
            int bal = 0;
            uint acctNo = 0;
            uint pin = 0;
            string img = "";

            int entries = foob.GetNumEntries();
            int checkvar = 0;


            for (int i = 1; i <= entries; i++)
            {
                
                string firstName;
                string lastName;
                uint accountNo;
                uint pinNo;
                int balance;
                string imgSource;
                SearchData searchable = searchText;

                foob.GetValuesForEntry(i, out accountNo, out pinNo, out balance, out firstName, out lastName, out imgSource);

                if (lastName.ToLower().Contains(searchable.searchStr.ToLower()))
                {


                    checkvar = 1;
                    acctNo = accountNo;
                    pin = pinNo;
                    bal = balance;
                    fName = firstName;
                    lName = lastName;
                    img = imgSource;

                    break;
                }


            }


            // FEEDING DATA
            DataIntermed client = new DataIntermed();

            client.bal = bal;
            client.fname = fName;
            client.lname = lName;
            client.acctNo = acctNo;
            client.pin = pin;
            client.img = img;

            return client;

        }


        private void Data_Server_Initilization()
        {
            ChannelFactory<Data_Server_Interface> foobFactory;
            NetTcpBinding tcp = new NetTcpBinding();
            string URL = "net.tcp://localhost:8100/DataService";

            foobFactory = new ChannelFactory<Data_Server_Interface>(tcp, URL);
            foob = foobFactory.CreateChannel();
        }
    }
}