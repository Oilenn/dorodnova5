namespace Laba5
{
    public interface IRabbitMqService
    {
        void SendMessage(object obj);
        void SendMessage(string message);
        void SendEmail(string to, string subject, string body);
    }
}
