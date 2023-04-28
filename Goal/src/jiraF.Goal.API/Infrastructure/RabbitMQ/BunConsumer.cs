using jiraF.Goal.API.GlobalVariables;
using jiraF.Goal.API.Infrastructure.Data.Contexts;
using jiraF.Goal.API.Infrastructure.Data.Entities;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace jiraF.Goal.API.Infrastructure.RabbitMQ;

public class BunConsumer : BackgroundService
{
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly IConfiguration _configuration;
    private readonly AppDbContext _appDbContext;

    public BunConsumer(
        IConfiguration configuration,
        AppDbContext appDbContext)
    {
        _configuration = configuration;
        _appDbContext = appDbContext;
        string userName = _configuration["RABBITMQ_DEFAULT_USER"] ?? Environment.GetEnvironmentVariable("RABBITMQ_DEFAULT_USER");
        string password = _configuration["RABBITMQ_DEFAULT_PASS"] ?? Environment.GetEnvironmentVariable("RABBITMQ_DEFAULT_PASS");
        string hostName = _configuration.GetValue<string>("RabbitMQ:HostName");
        int port = 5671;
        ConnectionFactory factory = new()
        {
            Uri = new Uri($"amqps://{userName}:{password}@{hostName}/{userName}"),
            HostName = hostName,
            Port = port,
            UserName = userName,
            Password = password,
        };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.QueueDeclare(queue: "MemberQueue", durable: false, exclusive: false, autoDelete: false, arguments: null);
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();
        EventingBasicConsumer consumer = new(_channel);
        consumer.Received += (chanel, eventArguments) =>
        {
            string content = Encoding.UTF8.GetString(eventArguments.Body.ToArray());
            Guid memberId = new(content);
            List<GoalEntity> goals = _appDbContext.Goals
                .Where(x =>
                    x.ReporterId == memberId ||
                    x.AssigneeId == memberId)
                .ToList();
            goals.Where(x => x.ReporterId == memberId).ToList().ForEach(x => x.ReporterId = new Guid(DefaultMemberVariables.Id));
            goals.Where(x => x.AssigneeId == memberId).ToList().ForEach(x => x.AssigneeId = new Guid(DefaultMemberVariables.Id));
            _channel.BasicAck(eventArguments.DeliveryTag, false);
        };
        _channel.BasicConsume("MemberQueue", false, consumer);
        return Task.CompletedTask;
    }

    public override void Dispose()
    {
        _channel.Close();
        _connection.Close();
        base.Dispose();
    }
}
