using System;
using Android.App;
using Android.OS;
using Android.Widget;


namespace HeyApp
{
    [Activity(Label = "Contact Us!",Name ="com.HeyApp.ContactActivity", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait,Theme ="@style/Theme.AppCompat.Light")]
    public class ContactActivity :Activity
    {
        
        MainActivity ma = new MainActivity();
        mySpecialReciever my = new mySpecialReciever();
        public String Name, Message, Email;
        Button btnsubmit;
        EditText message, name, email;
        ProgressDialog pd;
       
        protected override void OnCreate(Bundle savedInstanceState)

        {
            base.OnCreate(savedInstanceState);
            if (savedInstanceState != null)
            {
                email.Text = savedInstanceState.GetString("Email");
                name.Text = savedInstanceState.GetString("Name");
                message.Text = savedInstanceState.GetString("Message");
            }
            SetContentView(Resource.Layout.Contact);

            //Register the layout eleents
             btnsubmit = (Button)FindViewById(Resource.Id.buttonsndto);
             name = (EditText)FindViewById(Resource.Id.Name);
             email = (EditText)FindViewById(Resource.Id.Email);
             message = (EditText)FindViewById(Resource.Id.Message);
            //progress dialog to display while the message is being sent
            pd = new ProgressDialog(this);
            btnsubmit.Click += Btnsubmit_Click;   //Button click listener delegate

             
            
        }
        void showToast(string message,int length)  //length should either be 0 or 1 for short and long toast length respectively
        {
            
            Toast.MakeText(this,message.ToString(),0).Show();
        }
        private bool check_entries(string name,string email,string message)
        {
            if (Name.Trim().Length == 0 || Email.Trim().Length == 0 || Message.Length == 0)
            {
                ma.Show(this, "Any of the given Fields Should not be Empty!");
                return false;
            }
            else if (Email.Contains("@") != true || Email.Contains(".") != true)
            {
                showToast("email id must be like user@domain.xyz", 1);
                return false;
            }
            else
            {
                return true;
            }

        }

        private void send_entries(string Name,string Email,String Message)

        {
            string urlpost = "https://autolligentbeta.000webhostapp.com/submitmail.php?name=" + Name + "&email=" + Email + "&message=" + Message;

            pd.SetTitle("Sending your message " + Name);
            pd.SetMessage("Please wait.");
            pd.SetCancelable(false);
            pd.SetIcon(Resource.Drawable.icon);
            pd.Show();
            try
            {
                Sendmail(urlpost);

            }


            catch (SystemException ex)
            {
                ma.Show(this, ex.Message);
            }
        }

        private void Btnsubmit_Click(object sender, EventArgs e)
        {

            Name = name.Text;
            Message = message.Text;
            Email = email.Text;

            bool is_correct = check_entries(Name, Message, Email);
            if (is_correct)
            {
                send_entries(Name, Email, Message);
            }
            else
            {

                return;
                
                
            }
            
        }

        public async void Sendmail(string addres)
        {
             string data = await my.getresponseasync(addres);
            pd.Dismiss();
            if (data == "Nully")
            {

                showToast("Please check your internet connection", 1);
            }
            else
            {
                showToast("Your feed back was sent ! ", 1);
            }
        }
        
        protected override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);
            Name = name.Text;
            Message = message.Text;
            Email = email.Text;
            outState.PutString("Name",Name);
            outState.PutString("Email", Email);
            outState.PutString("Message",Message );

        }
        protected override void OnResume()
        {
            base.OnResume();
            
            
            
        }

       
    }
}