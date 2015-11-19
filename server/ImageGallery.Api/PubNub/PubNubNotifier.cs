namespace Test
{
    using System.Net;
    using System.Web.Script.Serialization;

    public class PubNubNotifier : IPublisher
    {
        private const string PublishRequestFormat = "http://pubsub.pubnub.com/publish/{0}/{1}/0/{2}/0/{3}";
        private const string PublishKey = "pub-c-aa7e75ee-ca5f-40ef-95c9-fe28cd6830f6";
        private const string SubscribeKey = "sub-c-9fe7c32e-8c77-11e5-84ee-0619f8945a4f";

        public PubNubNotifier()
        {
        }

        public void Publish(string channel, string message)
        {
            var serializer = new JavaScriptSerializer();
            var url = string.Format(PublishRequestFormat, PublishKey, SubscribeKey, channel, serializer.Serialize(message));

            HttpWebRequest.Create(url).GetResponse();
        }
    }
}