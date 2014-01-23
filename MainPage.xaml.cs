using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;
using Microsoft.Phone.UserData;

namespace DrunkyTexty
{
    public partial class MainPage : PhoneApplicationPage
    {
        Contacts cons;
        IEnumerable<Contact> listCons;

        // Constructor
        public MainPage()
        {
            InitializeComponent();
            getContacts();
            NumberData.Text = "l";
        }

        private void getContacts()
        {
            cons = new Contacts();
            //Identify the method that runs after the asynchronous search completes.
            cons.SearchCompleted += new EventHandler<ContactsSearchEventArgs>(ContactsSearchCompleted);
            //Start the asynchronous search.
            cons.SearchAsync(String.Empty, FilterKind.None, "Contacts Test #1");
        }

        private void ContactsSearchCompleted(object sender, ContactsSearchEventArgs e)
        {
            cons.SearchCompleted -= ContactsSearchCompleted;
            //e.Results should be the list of contact, since there's no filter applyed in the search you shoul have all contact here

            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //_Number.ItemsSource = e.Results;
            //NumberData.ItemsSource = e.Results;
            listCons = e.Results;
            NumberData.ItemsSource = listCons;
        }
 

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            SmsComposeTask smsComposeTask = new SmsComposeTask();

            smsComposeTask.To = NumberData.Text;
            smsComposeTask.Body = _Message.Text;
            smsComposeTask.Show();
        }
    }
}