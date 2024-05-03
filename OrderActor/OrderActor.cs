using Dapr.Actors.Runtime;

namespace OrderActor
{
    [Actor(TypeName = "OrderActor")]
    public class OrderActor : Actor, IOrderActor
    {
        public OrderActor(ActorHost host) : base(host)
        {
        }

        public async Task<string> GetOrder()
        {
            return await StateManager.GetStateAsync<string>("CURRENT_SNAPSHOT");
        }

        public async Task SetOrder(string orderId)
        {
            await StateManager.GetOrAddStateAsync("CURRENT_SNAPSHOT", orderId);
        }
    }
}
