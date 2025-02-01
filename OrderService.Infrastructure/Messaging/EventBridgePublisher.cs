using Amazon.EventBridge;
using Amazon.EventBridge.Model;
using Amazon.EventBridge.Endpoints;
using Amazon.EventBridge.Internal;
using OrderService.Application.Interfaces;
using OrderService.Domain.Entities;

namespace OrderService.Infrastructure.Messaging
{
    public class EventBridgePublisher : IEventPublisher
    {
    
        private readonly AmazonEventBridgeClient _eventBridgeClient;

        public EventBridgePublisher()
        {
            _eventBridgeClient = new AmazonEventBridgeClient();
        }

        public async Task PublishOrderCreatedAsync(Order order)
        {
            var putEventsRequest = new PutEventsRequest
            {
                Entries = new List<PutEventsRequestEntry>
                {
                    new PutEventsRequestEntry
                    {
                        EventBusName = "default",
                        Source = "OrderService",
                        DetailType = "OrderCreated",
                        Detail = $"{{ \"OrderId\": \"{order.Id}\", \"ProductName\": \"{order.ProductName}\", \"Quantity\": {order.Quantity} }}"
                    }
                }
            };

            var response = await _eventBridgeClient.PutEventsAsync(putEventsRequest);

            if (response.FailedEntryCount > 0)
            {
                throw new Exception($"Failed to publish event: {response.Entries[0].ErrorMessage}");
            }
        }
    }
}
