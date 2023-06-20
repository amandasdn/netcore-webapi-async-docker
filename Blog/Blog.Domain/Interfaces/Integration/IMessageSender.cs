namespace Blog.Domain.Interfaces.Integration
{
    public interface IMessageSender
    {
        void SendMessage(byte[] message, string queueKey);
    }
}
