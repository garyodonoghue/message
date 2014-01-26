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

            generateCaptcha();
        }

        private void generateCaptcha()
        {
            throw new NotImplementedException();
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
 

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            SmsComposeTask smsComposeTask = new SmsComposeTask();

            smsComposeTask.To = autoCompleteBox1.Text;
            smsComposeTask.Body = _Message.Text;
            smsComposeTask.Show();
        }
    }

    public partial class CImage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Create a random code and store it in the Session object.
            this.Session["CaptchaImageText"] = GenerateRandomCode();
            // Create a CAPTCHA image using the text stored in the Session object.
            RandomImage ci = new RandomImage(this.Session
                ["CaptchaImageText"].ToString(), 300, 75);
            // Change the response headers to output a JPEG image.
            this.Response.Clear();
            this.Response.ContentType = "image/jpeg";
            // Write the image to the response stream in JPEG format.
            ci.Image.Save(this.Response.OutputStream, ImageFormat.Jpeg);
            // Dispose of the CAPTCHA image object.
            ci.Dispose();
        }

        // Function to generate random string with Random class.
        private string GenerateRandomCode()
        {
            Random r = new Random();
            string s = "";
            for (int j = 0; j < 5; j++)
            {
                int i = r.Next(3);
                int ch;
                switch (i)
                {
                    case 1:
                        ch = r.Next(0, 9);
                        s = s + ch.ToString();
                        break;
                    case 2:
                        ch = r.Next(65, 90);
                        s = s + Convert.ToChar(ch).ToString();
                        break;
                    case 3:
                        ch = r.Next(97, 122);
                        s = s + Convert.ToChar(ch).ToString();
                        break;
                    default:
                        ch = r.Next(97, 122);
                        s = s + Convert.ToChar(ch).ToString();
                        break;
                }
                r.NextDouble();
                r.Next(100, 1999);
            }
            return s;
        }
    }
}