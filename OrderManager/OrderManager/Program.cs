using OrderManager.Enums;
using OrderManager.Services;

public class Program
{
    public static void Main()
    {
        const int DeliveryTimeDays = 3;

        while ( true )
        {
            ConsoleService.PrintMenu();
            string inputChoice = Console.ReadLine();
            Enum.TryParse( inputChoice, out MenuOption choice );
            if ( !HandleMenuChoice( choice, DeliveryTimeDays ) )
                return;
        }
    }

    private static bool HandleMenuChoice( MenuOption choice, int deliveryTimeDays )
    {
        switch ( choice )
        {
            case MenuOption.Exit:
                ConsoleService.WriteGoodbye();
                return false;
            case MenuOption.Place:
                ConsoleService.WriteStartOrder();
                MakeOrder( deliveryTimeDays );
                return true;
            default:
                ConsoleService.WriteInvalidSelection();
                return true;
        }
    }

    private static void MakeOrder( int deliveryTimeDays )
    {
        try
        {
            string productName = ConsoleService.ReadNonEmptyString( "Enter the product name: " );
            int quantity = ConsoleService.ReadInt( "Enter the quantity: " );
            string name = ConsoleService.ReadNonEmptyString( "Enter your name: " );
            string address = ConsoleService.ReadNonEmptyString( "Enter the delivery address: " );
            DateTime deliveryDate = OrderService.CalculateDeliveryDate( deliveryTimeDays );

            ConsoleService.WriteOrderSummary( name, quantity, productName, address );
            Confirmation answer = ConsoleService.ReadConfirmation();

            if ( answer == Confirmation.Yes )
            {
                Order order = OrderService.CreateOrder( productName, quantity, name, address, deliveryDate );
                if ( OrderService.ProcessConfirmation( answer, order ) )
                    return;
            }
            else
            {
                ConsoleService.WriteOrderCancelled();
            }

        }
        catch ( Exception ex ) when (
                 ex is FormatException
              || ex is ArgumentOutOfRangeException
              || ex is ArgumentException )
        {
            if ( ex is FormatException )
                ConsoleService.WriteInvalidInt();
            else
                ConsoleService.WriteError( ex.Message );
        }
    }
}

