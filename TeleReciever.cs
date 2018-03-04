using Android.App;
using Android.Content;
using Android.Widget;
using Android.Telephony;



namespace HeyApp
{
   
    
    [BroadcastReceiver(Enabled = true) ]
    [IntentFilter(new[] { "android.intent.action.PHONE_STATE" })]

    public class TeleReciever : BroadcastReceiver
    {

        
        
       
        public override void OnReceive(Context context, Intent intent)
        {
           
               //check to see if intent is not empty
            if (intent.Extras != null)
            {
                //getting the extra states from telephonyManager
               

                string state = intent.GetStringExtra(TelephonyManager.ExtraState);
                // check if the phone is ringing
                if (state == TelephonyManager.ExtraStateRinging)
                {
                    //get the calller number
                    string callerNum = intent.GetStringExtra(TelephonyManager.ExtraIncomingNumber);
                    
                    Toast.MakeText(context, callerNum, ToastLength.Long).Show();
                    Globals.state = 0;
                    string urlpost = "https://autolligentbeta.000webhostapp.com/action.php?select=Incoming " + callerNum;
                    //Wv1.LoadUrl(urlpost);
                    Intent i = new Intent("Info");
                    i.SetAction("Info");
                    i.PutExtra("url",urlpost+" "+Globals.Tagid);
                    i.PutExtra("data","Incoming"+callerNum+Globals.Tagid);
                    context.SendBroadcast(i);
                }
                if(state == TelephonyManager.ExtraStateOffhook)
                {
                    string callerNum = intent.GetStringExtra(TelephonyManager.ExtraIncomingNumber);
                    if (Globals.state == 0)
                    {
                        Globals.state = 01; //answered the incoming call
                        Toast.MakeText(context, "answered" + callerNum, ToastLength.Long).Show();
                        Globals.urlpost = "https://autolligentbeta.000webhostapp.com/action.php?select=" + "answering " + callerNum;
                    }
                    else
                    {
                        Globals.state = 1;  //state 1 for outgoing calls

                        Toast.MakeText(context, callerNum, ToastLength.Long).Show();
                        Globals.urlpost = "https://autolligentbeta.000webhostapp.com/action.php?select=" + "Outgoing " + callerNum;

                        //Wv1.LoadUrl(urlpost);
                       
                    }
                     Intent i = new Intent("Info");
                        i.SetAction("Info");
                        i.PutExtra("url", Globals.urlpost+" "+Globals.Tagid);
                        i.PutExtra("data", "Outgoing" + callerNum);
                        context.SendBroadcast(i);
                }
                if(state == TelephonyManager.ExtraStateIdle)
                {

                    Globals.urlpost = "https://autolligentbeta.000webhostapp.com/action.php?select= "+ "Idle __";
                    //Wv1.LoadUrl(urlpost);
                    Intent i = new Intent("Info");
                    i.SetAction("Info");
                    i.PutExtra("url", Globals.urlpost+" "+Globals.Tagid);
                    i.PutExtra("data", "Idle__");
                    context.SendBroadcast(i);
                   
                }

            }
            else
            {
                string stru = "No data in the phone state";
                Toast.MakeText(context, stru, ToastLength.Short).Show();
            }
        }
    }
   /*
    [BroadcastReceiver(Enabled = true)]
    [IntentFilter(new[] { "myAction" })]
    public class mySpecialReciever : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            // throw new NotImplementedException();
            if (intent.Action == "myAction")
            {
                Log.Info("MyactionTag", "recieved the action");
                Toast.MakeText(context, "Recieved your action", ToastLength.Long).Show();
            }
        }
    }*/
}