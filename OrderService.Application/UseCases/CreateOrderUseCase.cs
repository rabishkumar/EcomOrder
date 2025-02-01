using OrderService.Application.Interfaces;
using OrderService.Domain.Entities;
namespace OrderService.Application.UseCases
{
    public class CreateOrderUseCase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IEventPublisher _eventPublisher;

        public CreateOrderUseCase(IOrderRepository orderRepository, IEventPublisher eventPublisher)
        {
            _orderRepository = orderRepository;
            _eventPublisher = eventPublisher;
        }

        public async Task ExecuteAsync(string productName, int quantity)
        {
            // Create the order
            var order = new Order(productName, quantity);

            // Save it to the database
            await _orderRepository.SaveOrderAsync(order);

            // Publish the event
            await _eventPublisher.PublishOrderCreatedAsync(order);
        }
    }
}