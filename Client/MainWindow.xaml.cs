using Business_Tier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Tutorial1_Server;

namespace Client
{
    public partial class MainWindow : Window
    {
        private BusinessServerInterface foob;

        public MainWindow()
        {
            InitializeComponent();


            ChannelFactory<BusinessServerInterface> foobFactory;
            NetTcpBinding tcp = new NetTcpBinding();

            string URL = "net.tcp://localhost:8200/BusinessService";

                try
                {
                    foobFactory = new ChannelFactory<BusinessServerInterface>(tcp, URL);
                    foob = foobFactory.CreateChannel();

                    TotalNum.Text = foob.GetNumEntries().ToString();
                }
                catch
                {
                    MessageBox.Show("Unable to make a connection to the main server");
                    return;
                }
            
                
        }

        private void goBtn_Click(object sender, RoutedEventArgs e)
        {
            int index = 0;
            string fName = "", lName = "", img = "";
            int bal = 0;
            uint acct = 0, pin = 0;

            try
            {
                index = Int32.Parse(IndexNum.Text);
            }
            catch
            {
                MessageBox.Show("Enter a number!");
                return;
            }



            try
            {
                if (index > foob.GetNumEntries() || index <= 0)

                {
                    Console.WriteLine(foob.GetNumEntries());
                    MessageBox.Show("Please enter a valid index from 1 to 151");
                    return;
                }
                else
                {
                    foob.GetValuesForEntry(index, out acct, out pin, out bal, out fName, out lName, out img);
                }


                FirstName.Text = fName;
                LastName.Text = lName;
                Balance.Text = bal.ToString("C");
                AcctNo.Text = acct.ToString();
                Pin.Text = pin.ToString("D4");
            }
            catch
            {
                MessageBox.Show("Unable to make a connection to the server");
                return;
            }


            // Profile Picture

            Uri link = new Uri(img, UriKind.Absolute);
            Console.WriteLine(link.ToString());
            ProfileImg.Source = new BitmapImage(link);



        }

        private void SearchBtnClick(object sender, RoutedEventArgs e)
        {
            int index = 0;
            string fName = "", lName = "", img = "";
            int bal = 0;
            uint acct = 0, pin = 0;

            foob.GetValuesForSearch(SearchBox.Text, out acct, out pin, out bal, out fName, out lName, out img);

            FirstName.Text = fName;
            LastName.Text = lName;
            Balance.Text = bal.ToString("C");
            AcctNo.Text = acct.ToString();
            Pin.Text = pin.ToString("D4");

            Uri link = new Uri(img, UriKind.Absolute);
            Console.WriteLine(link.ToString());
            ProfileImg.Source = new BitmapImage(link);

        }
    }

}
