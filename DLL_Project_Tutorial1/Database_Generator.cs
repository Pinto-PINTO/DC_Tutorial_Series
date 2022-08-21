using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL_Project_Tutorial1
{
    internal class Database_Generator : Database_Storage
    {
        private string GetFirstname
        {
           get { return firstName; }
        }

        private string GetLastname
        {
            get { return lastName; }
        }

        private string GetImg
        {
            get { return img; }
        }

        private uint GetPIN
        {
            get { return pin; }
        }

        private uint GetAcctNo
        {
            get { return acctNo; }
        }

        private int GetBalance
        {
            get { return balance; }
        }

        public void GetNextAccount(out uint pin, out uint acctNo, out string firstName, out string lastName, out string img, out int balance)
        {   
            firstName = GetFirstname;
            lastName = GetFirstname;
            img = GetImg;
            pin = GetPIN;
            acctNo = GetAcctNo;
            balance = GetBalance;
        }
    }
}
