using System;

using RabbitMQ.Client;

using Messages;

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
                    channel.ExchangeDeclare(Constants.Exchanges.ProductQuantityUpdate, "fanout");
                    channel.ExchangeDeclare(Constants.Exchanges.SummarizationComplete, "fanout");

                    var message = new ProductQuantityUpdateMessage()
                    {
                        MeasurementPointId = "MP001",
                        ContractDayStart = new DateTime(2018, 8, 1),
                        ContractDayEnd = new DateTime(2018, 8, 31)
                    };

                    var body = Shared.MessagingUtility.SerializeObject(message);

                    channel.BasicPublish(exchange: Constants.Exchanges.ProductQuantityUpdate,
                        routingKey: "",
                        basicProperties: null,
                        body: body);
                }
            }
        }
    }
}
