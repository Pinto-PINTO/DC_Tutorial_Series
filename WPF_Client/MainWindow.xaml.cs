using System;
using System.Collections.Generic;
using System.Linq;
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
using System.ServiceProcess;
using System.ServiceModel;
using Tutorial1_Server;


namespace WPF_Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        

        public MainWindow()

        {

            private Data_Server_Interface foob;

        InitializeComponent();
            ChannelFactory<Data_Server_Interface> foobFactory;
            NetTcpBinding tcp = new NetTcpBinding();

            string URL = "net.tcp://localhost:8100/DataService";
            // Uri uri = new Uri("net.tcp://localhost:8100/Data_Service");
            foobFactory = new ChannelFactory<Data_Server_Interface>(tcp, URL);
            foob = foobFactory.CreateChannel();

            TotalNum.Text = foob.GetNumEntries().ToString();
        }

        private void goBtn_Click(object sender, RoutedEventArgs e)
        {
            int index = 0;
            string fName = "", lName = "";
            int bal = 0;
            uint acct = 0, pin = 0;            

            index = Int32.Parse(IndexNum.Text);
           
            foob.GetValuesForEntry(index, out acct, out pin, out bal, out fName, out lName);
            
            FNameBox.Text = fName;
            LNameBox.Text = lName;
            BalanceBox.Text = bal.ToString("C");
            AcctNoBox.Text = acct.ToString();
            PinBox.Text = pin.ToString("D4");
        }
    }


}
