using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Net.Mail;
using System.Net;

public class EmailConsumer
{
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly string _queueName;

    public EmailConsumer(string queueName)
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _queueName = queueName;
    }

    public void StartConsuming()
    {
        _channel.QueueDeclare(queue: _queueName,
                             durable: false,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);

        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine("Received message: {0}", message);

            // Отправка сообщения на электронную почту
            SendEmail(message);
        };
        _channel.BasicConsume(queue: _queueName,
                             autoAck: true,
                             consumer: consumer);
    }

    private void SendEmail(string message)
    {
        var smtp = new SmtpClient("smtp.gmail.com")
        {
            Port = 587,
            EnableSsl = true,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential("your-email@gmail.com", "your-password")
        };

        using (var mailMessage = new MailMessage("your-email@gmail.com", "recipient-email@gmail.com")
        {
            Subject = "Message from RabbitMQ",
            Body = message
        })
        {
            smtp.Send(mailMessage);
        }
    }
}