using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace AppliRabbitConsumer.Consumer
{
    public class RabbitMQConsumer : IMessageConsumer
    {

        public static EventingBasicConsumer _consumer ;

        public readonly ILogger<RabbitMQConsumer> _logger;

        private readonly IConfiguration _configuration;


        public RabbitMQConsumer(ILogger<RabbitMQConsumer> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }


        public void InitConso()
        {
            var urlRMQ = _configuration["UrlRabbitMQ"];

            _logger.LogInformation(urlRMQ);


            var factory = new ConnectionFactory { HostName = urlRMQ };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();


            _consumer = new EventingBasicConsumer(channel);


            _consumer.Received += (model, eventArgs) =>
            {
                var body = eventArgs.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

               // Console.WriteLine(message);

                _logger.LogInformation(message);    


            };


            channel.BasicConsume(queue: "orders", autoAck: true, consumer: _consumer);


        }
    }
}
