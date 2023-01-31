// See https://aka.ms/new-console-template for more information
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

Console.WriteLine("Hello, World!");

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

var consumer = new EventingBasicConsumer(channel);

consumer.Received += (model, eventArgs) => 
{
    var body = eventArgs.Body.ToArray();

    var message = Encoding.UTF8.GetString(body);

    Console.WriteLine($"Message: {message}");
};

channel.BasicConsume("bookings", true, consumer);

Console.ReadLine();