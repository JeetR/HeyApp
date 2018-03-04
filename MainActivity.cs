using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;

using System.Threading.Tasks;
using System;
using System.Net.Http;
using Android;
using Android.Content.PM;

using Android.Support.V7.App;
using Android.Runtime;
using static Android.Support.V4.App.ActivityCompat;
using Android.Support.V4.Content;
using Android.Support.V4.App;

namespace HeyApp

{
    public static class Globals
    {
        public static string urlpost;
        public static string res;
        public static bool has_displayedAlert = false;
        public static string Tagid;
        public static int taglen;
        public static int state = -1;
        public static bool is_tag_set = false;
    }



    [Activity(Label = "HeyApp ", MainLauncher = false, Icon = "@drawable/icon", Theme = "@style/Theme.AppCompat.Light.DarkActionBar", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class MainActivity : AppCompatActivity, IOnRequestPermissionsResultCallback

    {
        static readonly string[] permission = { "android.permission.READ_PHONE_STATE" };
        public MainActivity MainA()
        {
            return this;
        }
        //public WebView Wv1;

        int count = 0;
        public string bttext = "Your clicker!";

        //string location;



        Button btn1, btn2;
        TextView t1;
        EditText Tagid;

        void check_perms()
        {
            if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.ReadPhoneState) != (int)Permission.Granted)
            {
                Show_for_perms(this, "Permission required to get the call details please click allow on next screen");
            }
            else
            {
                //Nothing
            }
        }
        public void req_Permission()
        {
            if (Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.M)
            {
                
                    if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.ReadPhoneState) != (int)Permission.Granted)
                    {
                    
                        ActivityCompat.RequestPermissions(this,permission,101);
                        
                    }
                else
                {
                    Toast.MakeText(this, "permisiion is granted", ToastLength.Long).Show();
                }
                
            }
        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);


            if (savedInstanceState != null)
            {
                count = savedInstanceState.GetInt("count");
                bttext = savedInstanceState.GetString("bttext");
                Globals.taglen = savedInstanceState.GetInt("TagLenght");


            }

            // Set our view from the "main" layout resources
            SetContentView(Resource.Layout.Main);
            
            if (!Globals.has_displayedAlert)
            {
                
                Show(this, "Your phone details will be shared to our server you can see the details in the desktop client");

                count = 1;
                Globals.has_displayedAlert = true;

            }


            btn1 = (Button)FindViewById(Resource.Id.buttonSet);
            t1 = (TextView)FindViewById(Resource.Id.textView1);
            Tagid = (EditText)FindViewById(Resource.Id.Tagid);
            btn2 = (Button)FindViewById(Resource.Id.buttonSendMessage);



            btn2.Click += Btn2_Click;
            t1.Text = bttext;
            btn1.Click += Btn1_Click;
            check_perms();



        }


        protected override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);
            outState.PutInt("count", count);
            outState.PutString("bttext", bttext);
            // outState.PutInt("TagLenght", Globals.Tagid.Length);
        }

        private void Btn2_Click(object sender, EventArgs e)
        {
            // throw new NotImplementedException();
            StartActivity(new Intent(Application.Context, typeof(ContactActivity)));


        }



        public void Show_for_perms(Activity a, String message)
        {
            Android.Support.V7.App.AlertDialog.Builder dlgAlert = new Android.Support.V7.App.AlertDialog.Builder(a);
            dlgAlert.SetMessage(message);
            dlgAlert.SetTitle("Hey App");
            dlgAlert.SetPositiveButton("Ok Proceed", delegate { req_Permission(); });
            dlgAlert.SetCancelable(true);
            dlgAlert.Create().Show();
        }

        public void Show(Activity a, String message)
        {
            Android.Support.V7.App.AlertDialog.Builder dlgAlert = new Android.Support.V7.App.AlertDialog.Builder(a);
            dlgAlert.SetMessage(message);
            dlgAlert.SetTitle("Hey App");
            dlgAlert.SetPositiveButton("Ok Proceed", delegate { On_Clicker(); });
            dlgAlert.SetCancelable(true);
            dlgAlert.Create().Show();
        }


        private void On_Clicker()
        {
            //throw new NotImplementedException();
            
        }

        private void Btn1_Click(object sender, System.EventArgs e)
        {
            //throw new System.NotImplementedException();
            //int action = e.GetHashCode();

            count++;

            bttext = "Your total Click count is " + count.ToString();
            t1.Text = bttext;
            Globals.taglen = Tagid.Text.Length;
            if (Globals.taglen == 4)
            {
                Globals.Tagid = Tagid.Text;
                Show(this, "please remember  your Id for the Desktop Client \"" + Globals.Tagid + "\"");
                Tagid.Visibility = Android.Views.ViewStates.Invisible;
                btn1.Visibility = Android.Views.ViewStates.Invisible;
                t1.Text = "Your Tag is set \"" + Globals.Tagid + " \"";
                Globals.is_tag_set = true;
                check_perms();
                
            }
            else
            {
                Show(this, "Please enter the tagid again");
                Tagid.Hint = "Please make sure the Id is 4 characters long";
                t1.Text = "Wrong tagid";
                Globals.is_tag_set = false;
            }
            Toast.MakeText(this, "La..La..!!", ToastLength.Short).Show();
            //Show("Hello");

        }
        protected override void OnDestroy()
        {
            //base.OnDestroy();


        }
        protected override void OnResume()
        {
            base.OnResume();
            
            if (Globals.is_tag_set)
            {
                t1.Text = Globals.Tagid;
                Tagid.Visibility = Android.Views.ViewStates.Gone;
            }
            else
            {
                Tagid.Visibility = Android.Views.ViewStates.Visible;
            }
        }
        public override void OnBackPressed()
        {
            base.OnBackPressed();

        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {

            if (requestCode == 1)
            {
                if (grantResults[0] == Permission.Granted)
                {
                    Toast.MakeText(this, permissions[0], ToastLength.Long).Show();
                }

                else
                {
                    base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
                }

            }

        }
    }





    [BroadcastReceiver(Enabled = true)]
    [IntentFilter(new[] { "Info" })]
    public class mySpecialReciever : BroadcastReceiver

    {
        MainActivity obj = new MainActivity();
        Context ctx;
        
       /* public static string GetResponseText(string address)
        {
            var request = (HttpWebRequest)WebRequest.Create(address);

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                
                var encoding = Encoding.GetEncoding(response.CharacterSet);

                using (var responseStream = response.GetResponseStream())
                using (var reader = new StreamReader(responseStream, encoding))
                    //return reader.ReadLine();
                    return response.StatusCode.ToString();
            }
        }*/
        public async Task<string> getresponseasync(string addr)
        { var data = "";
            try
            {
                var httpclient = new HttpClient();
                var response = await httpclient.GetAsync(addr);
                var resp = response.Content;
                //var data = await resp.ReadAsStringAsync();
                data = response.StatusCode.ToString();
                 
            }
            catch(Exception e)
            {
                ctx = obj.BaseContext;
                
                data = "Nully";
            }
            return data;
        }
        public async void Sendmail(string addres)
        {
            string data = await getresponseasync(addres);
            if (data == "Nully")
            {
               return;
            }
            else
            {
                Globals.res = data;
            }

        }
            public override void OnReceive(Context context, Intent intent)

      
        {

            // throw new NotImplementedException();


            if (intent.Action == "Info")
            {
                if (Globals.Tagid != "")
                {

                    Globals.urlpost = intent.GetStringExtra("url");
                    string data_int = intent.GetStringExtra("data");
                    // Toast.MakeText(context, Globals.urlpost, ToastLength.Long).Show();

                    /* var uri = new System.Uri(Globals.urlpost);
                     var request = new HttpWebRequest(uri);
                     request.Method = "Post";*/

                  
                    // var reader = new System.IO.StreamReader(, encoding);

                    try
                    {
                        Sendmail(Globals.urlpost);
                    }
                    catch (Exception e)
                    {
                        
                        // obj.Show(e.Message);

                        Toast.MakeText(context, "something wrong with internet please check your connection or you may restart the app", ToastLength.Long).Show();

                    }
               
                    /*string resspl = res.Replace(" ", "");
                    if(resspl == data_int)
                    {
                        Toast.MakeText(context, "lovely", ToastLength.Long).Show();
                    }*/

                    Toast.MakeText(context, Globals.res, ToastLength.Long).Show();




                    //Log.Info("MyactionTag", "recieved the action");

                    //Toast.MakeText(context, Globals.urlpost, ToastLength.Long).Show();


                }
            }
            else
            {
                MainActivity MainActivity = new MainActivity();
                MainActivity.Show(MainActivity,"The tagid is not set please set the tag id");
            }
            
        }

       
    }

}

