using Dapr.Actors.Client;
using OrderActor;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Broker
{
    public class BrokerRabbitmqService : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var connectionFactory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest",
                Port = 15672,
                DispatchConsumersAsync = true,
            };
            var queue_name = "queue_order";
            var queue_exchange = "queue_exchange";
            IConnection conn = await connectionFactory.CreateConnectionAsync();
            var chanel = await conn.CreateChannelAsync();
            await chanel.ExchangeDeclareAsync(queue_exchange, type: ExchangeType.Direct);
            var resp = await chanel.QueueDeclareAsync(durable: true, exclusive: false, autoDelete: false, arguments: null, queue: queue_name);
            await chanel.QueueBindAsync(queue_name, queue_exchange, string.Empty);

            var consumer = new AsyncEventingBasicConsumer(chanel);
            var eventFunc = new AsyncEventHandler<BasicDeliverEventArgs>(ProcessMessage);
            consumer.Received += eventFunc;
            await chanel.BasicConsumeAsync(queue: queue_name, autoAck: false, consumer: consumer);
        }

        public async Task ProcessMessage(object sender, BasicDeliverEventArgs eventArgs)
        {
            byte[] body = eventArgs.Body.ToArray();
            var binary = new BinarySerializer();
            var data = binary.Deserialize<string>(body);
            var actorProxyFactory = new ActorProxyFactory();
            var actorProxy = actorProxyFactory.CreateActorProxy<IOrderActor>(new Dapr.Actors.ActorId("test"), "OrderActor");
            if(actorProxy != null)
            {
                await actorProxy.SetOrder(data);
            }
        }
    }
}
