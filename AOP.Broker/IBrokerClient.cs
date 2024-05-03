namespace Broker
{
    public interface IBrokerClient
    {
        Task PublishMessage(string actorId, object message);    
    }
}
