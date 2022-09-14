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
using Newtonsoft.Json;
using RestSharp;
using API_Classes;

namespace Async_Client
{
      
    public delegate string Search(string value);

    public partial class MainWindow : Window
    {
        private string searchText;
        private string base_url;


        public MainWindow()
        {
            InitializeComponent();
            RestClient restClient = new RestClient("http://localhost:59284/");
            RestRequest restRequest = new RestRequest("api/Getall/", Method.Get);

            RestResponse restResponse = restClient.Execute(restRequest);
            TotalNum.Text = restResponse.Content;

        }
 

        // VALUE COMPARISONS
        public static string ValueChecking(int val)
        {
            if (val == 0)
                return "EQUAL";
            else if (val < 0)
                return "LESS";
            else
                return "LARGER";
        }




        private void goBtn_Click(object sender, RoutedEventArgs e)
        {
            int index = 0;

            RestClient restClient;
            RestRequest restRequest;
            RestResponse restResponse = null;
            DataIntermed client;


            try
            {
                index = Int32.Parse(IndexNum.Text);
            }
            catch
            {
                MessageBox.Show("Enter a number!");
                return;
            }



            if ((ValueChecking(index) != "less than") && index < 100001)

            {
                // EXCEPTION HANDLING
                try
                {
                    restClient = new RestClient(base_url);
                    restRequest = new RestRequest("api/Getall/{id}", Method.Get);
                    restRequest.AddUrlSegment("id", index);
                    restResponse = restClient.Execute(restRequest);

                    client = JsonConvert.DeserializeObject<DataIntermed>(restResponse.Content);

                }
                catch (Exception)
                {

                    Console.WriteLine("ERROR OCCURED", e);
                }

            }
            else
            {
                Console.WriteLine("NUMBER NOT WITHIN THE RANGE!");
            }


            client = JsonConvert.DeserializeObject<DataIntermed>(restResponse.Content);


            // PROFILE PICTURE
            Uri link = new Uri(client.img, UriKind.Absolute);
            Console.WriteLine(link.ToString());
            ProfileImg.Source = new BitmapImage(link);


            // FIELDS
            FirstName.Text = client.fname.ToString();
            LastName.Text = client.lname;
            Balance.Text = client.bal.ToString("C");
            AcctNo.Text = client.acctNo.ToString();
            Pin.Text = client.pin.ToString("D4");

        }


        private async void SearchBtnClick(object sender, RoutedEventArgs e)
        {

            searchText = SearchBox.Text;

            // EXCEPTION HANDING AND CLEANUP
            if (!InvalidCharacters(searchText) && !searchText.All(char.IsDigit))
            {

                ProgressBarValue(10);
                CloseUI();

                Task<string> task = new Task<string>(Managing_Search);

                ProgressBarValue(20);
                task.Start();
                ProgressBarValue(30);

                var result = await task;
                ProgressBarValue(100);

            }
            else
            {
                Console.WriteLine("Invalid Last Name");
            }


        }



        public string Managing_Search()
        {

            RestClient restClient;
            RestRequest restRequest;
            RestResponse restResponse;
            DataIntermed client;


            SearchData clientSearch = new SearchData();

            clientSearch.searchStr = searchText;

            Console.WriteLine(clientSearch);
            restClient = new RestClient("http://localhost:59284/");
            restRequest = new RestRequest("api/search/", Method.Post);



            restRequest.AddJsonBody(clientSearch);

            restResponse = restClient.Execute(restRequest);

            client = JsonConvert.DeserializeObject<DataIntermed>(restResponse.Content);





            if (client.fname.ToString() != null)
            {

                FirstName.Dispatcher.Invoke(new Action(() => FirstName.Text = client.fname.ToString()));
                LastName.Dispatcher.Invoke(new Action(() => LastName.Text = client.lname.ToString()));
                Balance.Dispatcher.Invoke(new Action(() => Balance.Text = client.bal.ToString("C")));
                AcctNo.Dispatcher.Invoke(new Action(() => AcctNo.Text = client.acctNo.ToString()));
                Pin.Dispatcher.Invoke(new Action(() => Pin.Text = client.pin.ToString("D4")));

                ProgressBarValue(60);

                IndexNum.Dispatcher.Invoke(new Action(() => IndexNum.Text = ""));




                // PROFILE PICTURE
                Uri link = new Uri(client.img, UriKind.Absolute);
                Console.WriteLine(link.ToString());
                ProfileImg.Dispatcher.Invoke(new Action(() => ProfileImg.Source = new BitmapImage(link)));

                ProgressBarValue(90);



                StartUI();
                return "VALID REQUEST";

            }
            else
            {
                ProgressBarValue(0);
                return "NOT FOUND";

            }

        }


        // CHARACTER CHECKING IN SEARCH BAR
        private bool InvalidCharacters(string yourString)
        {
            return yourString.Any(ch => !Char.IsLetterOrDigit(ch));
        }

        // PROGRESS BAR
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

        private void url_box_click(object sender, RoutedEventArgs e)
        {
            base_url = url_box.Text;
            url_display.Text = base_url;
        }

    }
}
