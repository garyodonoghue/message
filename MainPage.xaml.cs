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
        Contacts cons2;
        IEnumerable<Contact> listCons;

        Style defaultStyle;
        // These integer variables store the numbers  
        // for the addition problem.  
        int var1;
        int var2;
        private IEnumerable<IEnumerable<ContactPhoneNumber>> smsContact;

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

            if (resultTxt.Text.Length != 0)
            {
                int attempt = Int32.Parse(resultTxt.Text);

                if (correctAnswer == attempt)
                {
                    sendBtn.Style = (Style)Application.Current.Resources["CorrectAnswer"];
                    wrong.Visibility = Visibility.Collapsed;

                    
                    listCons.Cast<object>().ToList();

                    cons2 = new Contacts();
                    //Identify the method that runs after the asynchronous search completes.
                    cons2.SearchCompleted += new EventHandler<ContactsSearchEventArgs>(SendSMS);
                    //Start the asynchronous search.
                    cons2.SearchAsync(String.Empty, FilterKind.None, "Contacts Test #1");

                    
                }
                else
                {
                    sendBtn.Style = (Style)Application.Current.Resources["WrongAnswer"];
                    generateProblem();
                    wrong.Visibility = Visibility.Visible;
                }
            }
        }

        private void SendSMS(object sender, ContactsSearchEventArgs e)
        {
            SmsComposeTask smsComposeTask = new SmsComposeTask();
            cons2.SearchCompleted -= ContactsSearchCompleted;

            foreach (var c in e.Results)
            {
                CustomContact contact = new CustomContact();
                contact.Name = c.DisplayName;
                if (contact.Name == autoCompleteBox1.Text)
                {
                    var number = c.PhoneNumbers.FirstOrDefault(); //change this to whatever number you want
                    if (number != null)
                        smsComposeTask.To = number.PhoneNumber;
                    else
                        contact.Number = "";
                }
            }
            smsComposeTask.Body = _Message.Text;
            smsComposeTask.Show();
        }
    }
}