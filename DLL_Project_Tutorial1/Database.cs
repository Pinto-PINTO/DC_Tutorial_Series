using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL_Project_Tutorial1
{
    public class Database
    {
        List<Database_Storage> dataList;

        public Database()
        {
            dataList = new List<Database_Storage>();

            String[] firstNameOptions = { "Ava", "Emma", "Liam", "Lucas", "Owen", "Sophia", "Sebastian", "Logan", "Ethan", "Johan" };
            String[] lastNameOptions = { "Adamson", "Bakers", "Miller", "Donald", "Gray", "Foster", "Sanders", "Brooks", "Ross", "Wood" };
            String[] imgOptions = { "https://images.pexels.com/photos/920382/pexels-photo-920382.jpeg", "https://images.pexels.com/photos/842554/pexels-photo-842554.jpeg", "https://images.pexels.com/photos/941572/pexels-photo-941572.jpeg", "https://images.pexels.com/photos/3761513/pexels-photo-3761513.jpeg", "https://images.pexels.com/photos/3769021/pexels-photo-3769021.jpeg", "https://images.pexels.com/photos/4050388/pexels-photo-4050388.jpeg", "https://images.pexels.com/photos/3194523/pexels-photo-3194523.jpeg", "https://images.pexels.com/photos/3762940/pexels-photo-3762940.jpeg", "https://images.pexels.com/photos/3756679/pexels-photo-3756679.jpeg", "https://images.pexels.com/photos/1310474/pexels-photo-1310474.jpeg", "https://images.pexels.com/photos/38554/girl-people-landscape-sun-38554.jpeg", "https://images.pexels.com/photos/937481/pexels-photo-937481.jpeg", "https://images.pexels.com/photos/1559486/pexels-photo-1559486.jpeg", "https://images.pexels.com/photos/1674752/pexels-photo-1674752.jpeg", "https://images.pexels.com/photos/428364/pexels-photo-428364.jpeg", "https://images.pexels.com/photos/1080213/pexels-photo-1080213.jpeg", "https://images.pexels.com/photos/1499327/pexels-photo-1499327.jpeg", "https://images.pexels.com/photos/4098157/pexels-photo-4098157.jpeg", "https://images.pexels.com/photos/2379004/pexels-photo-2379004.jpeg", "https://images.pexels.com/photos/2726111/pexels-photo-2726111.jpeg", "https://images.pexels.com/photos/1898555/pexels-photo-1898555.jpeg" };

            Random random = new Random(1);
            for (int i = 0; i < 151; i++)
            {
                Database_Storage identify = new Database_Storage();
                identify.firstName = firstNameOptions[random.Next(0, firstNameOptions.Length)];
                identify.lastName = lastNameOptions[random.Next(0, lastNameOptions.Length)];
                identify.img = imgOptions[random.Next(0, imgOptions.Length)];
                identify.acctNo = (uint)random.Next(500000, 1000000);
                identify.pin = (uint)random.Next(5000, 9000);
                identify.balance = random.Next(0, 1000000);

                dataList.Add(identify);
            }

        }

        public string GetFirstNameByIndex(int index)
        {
            return dataList[index - 1].firstName;
        }

        public string GetLastNameByIndex(int index)
        {
            return dataList[index - 1].lastName;
        }

        public string GetImgByIndex(int index)
        {
            return dataList[index - 1].img;
        }

        public uint GetAcctNoByIndex(int index)
        {
            return dataList[index - 1].acctNo;
        }

        public uint GetPinByIndex(int index)
        {
            return dataList[index - 1].pin;
        }

        public int GetBalanceByIndex(int index)
        {
            return dataList[index - 1].balance;
        }

        public int GetNumRecords()
        {
            return dataList.Count;
        }


    }
}
