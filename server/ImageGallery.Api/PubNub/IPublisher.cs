namespace Test
{
    public interface IPublisher
    {
        void Publish(string channel, string message);
    }
}
