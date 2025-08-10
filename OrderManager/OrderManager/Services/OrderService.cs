namespace OrderManager.Services
{
    public class OrderService
    {
        public DateTime CalculateDeliveryDate( int days )
        {
            return DateTime.Today.AddDays( days );
        }

        public Order CreateOrder( string productName, int quantity, string customerName, string address, DateTime deliveryDate )
        {
            return new Order( productName, quantity, customerName, address, deliveryDate );
        }

        public bool ProcessConfirmation( Enums.Confirmation answer, Order order, ConsoleService console )
        {
            if ( answer == Enums.Confirmation.Yes )
            {
                console.WriteLine( "" );
                console.WriteLine( $"{order.CustomerName}! Your order of {order.Quantity} {order.ProductName} has been placed! Expect delivery to {order.Address} {order.DeliveryDate:dd.MM.yyyy}." );
                return true;
            }
            else
            {
                console.WriteLine( "Order cancelled. Returning to main menu. \n" );
                return false;
            }
        }
    }
}