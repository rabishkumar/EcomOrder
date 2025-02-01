 namespace OrderService.Domain.Entities
{
 public class Order
    {
        public Guid Id { get; private set; }
        public string ProductName { get; private set; }
        public int Quantity { get; private set; }

        public Order(string productName, int quantity)
        {
            Id = Guid.NewGuid();
            ProductName = productName;
            Quantity = quantity;
        }
    }
}