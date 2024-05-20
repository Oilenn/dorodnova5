using System.Text;
using System.Threading.Channels;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

var factory = new ConnectionFactory()
{
    Uri = new
    Uri("amqps://vkcmnhyk:xYIhRErv02190ORHTcfv8orbUupAWJCB@roedeer.rmq.cloudamqp.com/vkcmnhyk") 
}; //подставить свой ключ!!!
using (var connection = factory.CreateConnection())
using (var channel = connection.CreateModel())
{ 
    channel.QueueDeclare(queue: "MyQueue",
    durable: false,
    exclusive: false,
    autoDelete: false,
    arguments: null);
    var consumer = new EventingBasicConsumer(channel);
    consumer.Received += (model, ea) =>
    {
        var body = ea.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);
        Console.WriteLine(" [x] Received {0}", message);
    };
    channel.BasicConsume(queue: "MyQueue",
    autoAck: true,
    consumer: consumer);
    Console.WriteLine(" Press [enter] to exit.");
    Console.ReadLine();
};