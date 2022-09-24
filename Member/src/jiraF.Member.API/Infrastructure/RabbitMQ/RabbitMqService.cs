using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace jiraF.Member.API.Infrastructure.RabbitMQ;

public class RabbitMqService : IRabbitMqService
{
	public void SendMessage(object obj)
	{
		var message = JsonSerializer.Serialize(obj);
		SendMessage(message);
	}

	public void SendMessage(string message)
	{
        ConnectionFactory factory = new() { HostName = "localhost" };
        using IConnection connection = factory.CreateConnection();
        using IModel channel = connection.CreateModel();
        channel.QueueDeclare(queue: "MemberQueue",
                       durable: false,
                       exclusive: false,
                       autoDelete: false,
                       arguments: null);
        byte[] body = Encoding.UTF8.GetBytes(message);
        channel.BasicPublish(exchange: "",
                       routingKey: "MemberQueue",
                       basicProperties: null,
                       body: body);
    }
}
