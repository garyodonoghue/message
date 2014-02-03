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

        Style defaultStyle;
        // These integer variables store the numbers  
        // for the addition problem.  
        int var1;
        int var2;

        // Constructor
        public MainPage()
        {
            InitializeComponent();
            
            defaultStyle = sendBtn.Style;

            getContacts();
            generateProblem();
        }

        private void generateProblem()
        {
            // Create a Random object called randomizer  
            // to generate random numbers.
            Random randomizer = new Random();
            
            var1 = randomizer.Next(30);
            var2 = randomizer.Next(30);

            problemVar1.Text = var1.ToString();
            problemVar2.Text = var2.ToString();
 

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
       
            listCons = e.Results;
            autoCompleteBox1.ItemsSource = listCons.Cast<object>().ToList();
        }
 

        private void sendClick(object sender, RoutedEventArgs e)
        {
            int correctAnswer = var1 * var2;

            int attempt = Int32.Parse(resultTxt.Text);

            if (correctAnswer == attempt)
            {
                sendBtn.Style = (Style)Application.Current.Resources["CorrectAnswer"];
                wrong.Visibility = Visibility.Collapsed;

                SmsComposeTask smsComposeTask = new SmsComposeTask();

                smsComposeTask.To = autoCompleteBox1.Text;
                smsComposeTask.Body = _Message.Text;
                smsComposeTask.Show();
            }
            else {
                sendBtn.Style = (Style)Application.Current.Resources["WrongAnswer"];
                generateProblem();
                wrong.Visibility = Visibility.Visible;
            }
        }
    }
}