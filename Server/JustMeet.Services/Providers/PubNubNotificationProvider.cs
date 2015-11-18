namespace JustMeet.Services.Providers
{
    using PubNubMessaging.Core;

    public class PubNubNotificationProvider
    {
        private const string Channel = "TeamIrisJustMeetProject";
        private const string publishKey = "pub-c-1af3bc49-5fd0-46f3-922e-7bf959e7f9d4";
        private const string subscribekey = "sub-c-0ce36a44-8de7-11e5-9320-02ee2ddab7fe";

        private static Pubnub pubnub = new Pubnub(publishKey, subscribekey);

        public static string LastMessage { get; private set; }

        public static string LastError { get; private set; }

        public static void Notify(string notification)
        {
            pubnub.Publish(Channel, notification, 
                (x) => LastMessage = x.ToString(), 
                (e) => LastError = e.ToString());
        }

    }
}