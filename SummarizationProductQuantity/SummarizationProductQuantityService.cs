using System;

using RabbitMQ.Client;
using RabbitMQ.Client.Events;

using Messages;

namespace SummarizationProductQuantity
{
    class SummarizationProductQuantityService
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare(Constants.Exchanges.ProductQuantityUpdate, "fanout");

                    channel.QueueDeclare(queue: Constants.Queues.SummarizationProductQuantity,
                                        durable: true,
                                        exclusive: false,
                                        autoDelete: false,
                                        arguments: null);

                    channel.QueueBind(queue: Constants.Queues.SummarizationProductQuantity,
                                        exchange: Constants.Exchanges.ProductQuantityUpdate,
                                        routingKey: "");

                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body;
                        var message = Shared.MessagingUtility.Deserialize<ProductQuantityUpdateMessage>(body);

                        Console.WriteLine(" [x] Received ProductQuantityUpdateMessage Message \n\t{0}", message);

                        var nextMessage = CreateNextMessage(message);

                        if (nextMessage != null)
                        {
                            var nextBody = Shared.MessagingUtility.SerializeObject(nextMessage);

                            channel.BasicPublish(exchange: Constants.Exchanges.SummarizationComplete,
                                            routingKey: "",
                                            basicProperties: null,
                                            body: nextBody);

                            Console.WriteLine(" [x] Published SummarizationComplete Message \n\t{0}", nextMessage);
                        }

                        channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                    };

                    channel.BasicConsume(queue: Constants.Queues.SummarizationProductQuantity,
                                        autoAck: false,
                                        consumer: consumer);

                    Console.WriteLine("Press [enter] to exit.");
                    Console.ReadLine();
                }
            }
        }

        static SummarizationCompleteMessage CreateNextMessage(ProductQuantityUpdateMessage inputMessage)
        {
            var outputMessage = new SummarizationCompleteMessage()
            {
                EntityId = inputMessage.MeasurementPointId,
                EntityType = EntityType.MeasurementPoint,
                IntervalType = IntervalType.Hourly,
                ContractDayStart = inputMessage.ContractDayStart,
                ContractDayEnd = inputMessage.ContractDayEnd
            };

            return outputMessage;
        }
    }
}
