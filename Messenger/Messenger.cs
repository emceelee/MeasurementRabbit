using System;

using RabbitMQ.Client;

namespace Messenger
{
    class Messenger
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare("ProductQuantityUpdate", "fanout");
                    channel.ExchangeDeclare("SummarizationComplete", "fanout");
                }
            }
        }
    }
}
