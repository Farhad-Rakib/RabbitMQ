using System.Text;
using RabbitMQ.Client;

var factory = new ConnectionFactory() { HostName="localhost"};
var connection = factory.CreateConnection();
var channel = connection.CreateModel();

channel.QueueDeclare(queue: "messagebox",
                     durable: false,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);

var message = "Test Message";
var body = Encoding.UTF8.GetBytes(message);
channel.BasicPublish("", "messagebox", null, body);
Console.WriteLine($"Send Message:{message}");