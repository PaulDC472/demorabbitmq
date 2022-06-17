using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace AppliRabbit1.Producer
{
    public class RabbitMQProducer : IMessageProducer
    {

        private readonly IConfiguration _configuration;


        public RabbitMQProducer(IConfiguration configuration)
        {
            _configuration = configuration;
        }




        public void SendMessage<T>(T message)
        {
            var urlRMQ = _configuration["UrlRabbitMQ"];

            var factory = new ConnectionFactory { HostName = urlRMQ };

            var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            // créé dans inteface RabbitMQ
            // channel.QueueDeclare("orders");

            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);


            channel.BasicPublish(exchange: "", routingKey: "orders", body: body);


        }
    }
}
