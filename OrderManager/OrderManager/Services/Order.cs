namespace OrderManager.Services
{
    public class Order
    {
        public string ProductName { get; }
        public int Quantity { get; }
        public string CustomerName { get; }
        public string Address { get; }
        public DateTime DeliveryDate { get; }

        public Order( string productName, int quantity, string customerName, string address, DateTime deliveryDate )
        {
            ProductName = productName;
            Quantity = quantity;
            CustomerName = customerName;
            Address = address;
            DeliveryDate = deliveryDate;
        }
    }
}