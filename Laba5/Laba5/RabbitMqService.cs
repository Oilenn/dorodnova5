using RabbitMQ.Client;
using System.Text.Json;
using System.Text;
using System.Net.Mail;
using System.Net;

namespace Laba5
{
    public class RabbitMqService : IRabbitMqService
    {
        public void SendMessage(object obj)
        {
            var message = JsonSerializer.Serialize(obj);
            SendMessage(message);
        }

        public void SendEmail(string to, string subject, string body)
         {
            var consumer = new EmailConsumer("myQueue");
            consumer.StartConsuming();
        }

    public void SendMessage(string message)
        {
            var factory = new ConnectionFactory()
            {
                Uri = new
            Uri("amqps://vkcmnhyk:xYIhRErv02190ORHTcfv8orbUupAWJCB@roedeer.rmq.cloudamqp.com/vkcmnhyk") }; //своя очередь
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "MyQueue",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);
                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(exchange: "",
                routingKey: "MyQueue",
                basicProperties: null,
                body: body);
            }

            // Отправка сообщения по электронной почте
            //SendEmail("grandluntik0@gmail.com", "New Message", message);
        }
    }
}

