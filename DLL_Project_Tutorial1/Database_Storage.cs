using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL_Project_Tutorial1
{
    internal class Database_Storage
    {
        public uint acctNo;
        public uint pin;
        public int balance;
        public string firstName;
        public string lastName;
        public string img;
        public Database_Storage()
        {
            acctNo = 0;
            pin = 0;
            balance = 0;
            firstName = "";
            lastName = "";
            img = "";
        }

    }
}
