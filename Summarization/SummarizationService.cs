using System;

using RabbitMQ.Client;
using RabbitMQ.Client.Events;

using Messages;

namespace Summarization
{
    class SummarizationService
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare(Constants.Exchanges.SummarizationComplete, "fanout");

                    channel.QueueDeclare(queue: Constants.Queues.Summarization,
                                        durable: true,
                                        exclusive: false,
                                        autoDelete: false,
                                        arguments: null);

                    channel.QueueBind(queue: Constants.Queues.Summarization,
                                        exchange: Constants.Exchanges.SummarizationComplete,
                                        routingKey: "");

                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body;
                        var message = Shared.MessagingUtility.Deserialize<SummarizationCompleteMessage>(body);

                        Console.WriteLine(" [x] Received SummarizationComplete Message \n\t{0}", message);

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

                    channel.BasicConsume(queue: Constants.Queues.Summarization,
                                        autoAck: false,
                                        consumer: consumer);

                    Console.WriteLine("Press [enter] to exit.");
                    Console.ReadLine();
                }
            }
        }

        static SummarizationCompleteMessage CreateNextMessage(SummarizationCompleteMessage inputMessage)
        {
            IntervalType? intervalType = SummarizationUtility.GetNextInterval(inputMessage.IntervalType);

            if(intervalType == null)
                return null;

            var outputMessage = new SummarizationCompleteMessage()
            {
                EntityId = inputMessage.EntityId,
                EntityType = inputMessage.EntityType,
                IntervalType = intervalType ?? IntervalType.Monthly,
                ContractDayStart = inputMessage.ContractDayStart,
                ContractDayEnd = inputMessage.ContractDayEnd
            };

            return outputMessage;
        }
    }
}
