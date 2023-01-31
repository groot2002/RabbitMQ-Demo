using System.Text;
using System.Text.Json;
using RabbitMQ.Client;

namespace FormulaAirline.API.Services;
public class MessageProducer: IMessageProducer
{
    public void SendMessage<T>(T message) 
    {
        var factory = new ConnectionFactory
        {
            HostName = "localhost",
            UserName = "groot2002",
            Password = "groot2002",
            VirtualHost = "/"
        };

        var connection = factory.CreateConnection();

        var channel = connection.CreateModel();
        channel.QueueDeclare("bookings", durable: true, exclusive: false, autoDelete: false, arguments: null);

        var jsonString = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(jsonString);

        channel.BasicPublish("", "bookings", body: body);
    }
}