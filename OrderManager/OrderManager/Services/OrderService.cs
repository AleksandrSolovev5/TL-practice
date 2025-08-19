using OrderManager.Enums;

namespace OrderManager.Services
{
    public static class OrderService
    {
        public static DateTime CalculateDeliveryDate( int days )
        {
            return DateTime.Today.AddDays( days );
        }

        public static Order CreateOrder( string productName, int quantity, string customerName, string address, DateTime deliveryDate )
        {
            return new Order( productName, quantity, customerName, address, deliveryDate );
        }

        public static bool ProcessConfirmation( Confirmation answer, Order order )
        {
            if ( answer == Confirmation.Yes )
            {
                ConsoleService.WriteOrderPlaced( order.CustomerName, order.Quantity, order.ProductName, order.Address, order.DeliveryDate );
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}