using Business_Tier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
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
    // Creating delegate client
    public delegate string Search(string value);

    public partial class MainWindow : Window
    {
        private BusinessServerInterface foob;
        private Search searchReferance;

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
            /* int index = 0;
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
            ProfileImg.Source = new BitmapImage(link); */


            // Exception handing for Search Box and code clean up
            if (!InvalidCharacters(SearchBox.Text) && !SearchBox.Text.All(char.IsDigit))
            {

                // Handing the progress bar
                CloseUI();
                ProgressBarValue(10);



                searchReferance = Managing_Search;
                AsyncCallback callback;
                callback = this.OnSearchCompletion;


                ProgressBarValue(10);

                var result = searchReferance.BeginInvoke(SearchBox.Text, callback, null);

            }
            else
            {
                Console.WriteLine("Invalid Last Name");
            }


            /*searchReferance = Managing_Search;
            AsyncCallback callback;
            callback = this.OnSearchCompletion;
            var result = searchReferance.BeginInvoke(SearchBox.Text, callback, null);*/

        }

        public string Managing_Search(string value)
        {

            string fName = null;
            string lName = null;
            string img = null;
            int bal = 0;            
            uint acct = 0;
            uint pin = 0;

            foob.GetValuesForSearch(value, out acct, out pin, out bal, out fName, out lName, out img);

            ProgressBarValue(50);

            try
            {

                FirstName.Dispatcher.Invoke(new Action(() => FirstName.Text = fName));
                LastName.Dispatcher.Invoke(new Action(() => LastName.Text = lName));
                Balance.Dispatcher.Invoke(new Action(() => Balance.Text = bal.ToString("C")));
                AcctNo.Dispatcher.Invoke(new Action(() => AcctNo.Text = acct.ToString()));
                Pin.Dispatcher.Invoke(new Action(() => Pin.Text = pin.ToString("D4")));

                ProgressBarValue(60);

                IndexNum.Dispatcher.Invoke(new Action(() => IndexNum.Text = ""));

                Uri link = new Uri(img, UriKind.Absolute);
                Console.WriteLine(link.ToString());
                ProfileImg.Dispatcher.Invoke(new Action(() => ProfileImg.Source = new BitmapImage(link)));

                ProgressBarValue(100);
                StartUI();

                return "Request Completed";

            }
            catch
            {
                Console.WriteLine("The Search has failed");
                return "Error";

            }

        }


        private void OnSearchCompletion(IAsyncResult asyncResult)
        {

            Search searchdel;
            AsyncResult asyncobj = (AsyncResult) asyncResult;

            if (asyncobj.EndInvokeCalled == false)
            {
                searchdel = (Search)asyncobj.AsyncDelegate;
                var result = searchdel.EndInvoke(asyncResult);
            }

            asyncobj.AsyncWaitHandle.Close();

        }

        // Invalid Character Checking
        private bool InvalidCharacters(string yourString)
        {
            return yourString.Any(ch => !Char.IsLetterOrDigit(ch));
        }

        // Progress Bar
        private void ProgressBarValue(int value)
        {
            ProgressBar.Dispatcher.Invoke(new Action(() => ProgressBar.Value = value));
        }


        private void CloseUI()
        {
            SearchBox.Dispatcher.Invoke(new Action(() => SearchBox.IsReadOnly = true));
            IndexNum.Dispatcher.Invoke(new Action(() => IndexNum.IsReadOnly = true));
            goBtn.Dispatcher.Invoke(new Action(() => goBtn.IsEnabled = false));
            SearchBtn.Dispatcher.Invoke(new Action(() => SearchBtn.IsEnabled = false));
        }

        private void StartUI()
        {
            SearchBox.Dispatcher.Invoke(new Action(() => SearchBox.IsReadOnly = false));
            IndexNum.Dispatcher.Invoke(new Action(() => IndexNum.IsReadOnly = false));
            goBtn.Dispatcher.Invoke(new Action(() => goBtn.IsEnabled = true));
            SearchBtn.Dispatcher.Invoke(new Action(() => SearchBtn.IsEnabled = true));
        }

    }

}
