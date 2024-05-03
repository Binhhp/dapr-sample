using Dapr.Actors;

namespace OrderActor
{
    public interface IOrderActor : IActor
    {
        Task SetOrder(string orderId);
        Task<string> GetOrder();
    }
}
