using OrderManager.Enums;

public class Program
{
    public static void Main()
    {
        const int DeliveryTimeDays = 3;
        var consoleService = new OrderManager.Services.ConsoleService();
        var orderService = new OrderManager.Services.OrderService();

        while ( true )
        {
            consoleService.PrintMenu();
            string myChoice = Console.ReadLine();
            Enum.TryParse( myChoice, out MenuOption choice );
            switch ( choice )
            {
                case MenuOption.Exit:
                    consoleService.WriteLine( "Goodbye!" );
                    return;
                case MenuOption.Place:
                    consoleService.WriteLine( "Start creating a new order.\n" );
                    break;
                default:
                    consoleService.WriteLine( "Invalid selection. Please enter 1 or 2." );
                    continue;
            }

            try
            {
                string productName = consoleService.ReadNonEmptyString( "Enter the product name: " );
                int quantity = consoleService.ReadInt( "Enter the quantity: " );
                string name = consoleService.ReadNonEmptyString( "Enter your name: " );
                string address = consoleService.ReadNonEmptyString( "Enter the delivery address: " );
                DateTime deliveryDate = orderService.CalculateDeliveryDate( DeliveryTimeDays );

                consoleService.WriteLine( $" \nHello, {name}, you ordered {quantity} {productName} to be delivered to {address}. Is this correct? (yes/no)" );
                var answer = consoleService.ReadConfirmation();

                var order = orderService.CreateOrder( productName, quantity, name, address, deliveryDate );
                if ( orderService.ProcessConfirmation( answer, order, consoleService ) )
                {
                    return;
                }

            }
            catch ( Exception ex ) when (
                     ex is FormatException
                  || ex is ArgumentOutOfRangeException
                  || ex is ArgumentException )
            {
                if ( ex is FormatException )
                {
                    consoleService.WriteLine( "Please enter a positive integer. Please try again.\n" );
                }
                else
                {
                    consoleService.WriteLine( $"Error: {ex.Message} Please try again.\n" );
                }
            }
        }
    }
}
