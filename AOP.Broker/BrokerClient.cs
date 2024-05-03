using Broker;

namespace AOP.Broker
{
    public class BrokerClient : IBrokerClient
    {
        public async Task PublishMessage(string actorId, object message)
        {
        }
    }
}
