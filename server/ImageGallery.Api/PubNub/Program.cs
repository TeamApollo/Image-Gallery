namespace Test
{
    using System;

    public class Program
    {
        public static void Main()
        {
            var chanel = "channelName";
            PubNubNotifier publisher = new PubNubNotifier();

            while (true)
            {
                Console.Write("Enter message: ");
                var msg = Console.ReadLine();

                publisher.Publish(chanel, msg);
            }
        }
    }
}
