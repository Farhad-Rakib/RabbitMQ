using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

var factory  = new ConnectionFactory() {HostName="localhost"};
var connection = factory.CreateConnection();
var channel = connection.CreateModel();


channel.QueueDeclare(queue: "messagebox",
                     durable: false,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);

var consumer = new EventingBasicConsumer(channel);

consumer.Received += (model, ea) =>
{
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine($"Recieved new message: {message}");
};

channel.BasicConsume(queue: "messagebox", autoAck: true, consumer: consumer);

Console.WriteLine("Consuming");

Console.ReadKey();
