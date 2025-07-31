using OrderManager.Enums;

public class Program
{
    public static void Main()
    {
        const int DeliveryTimeDays = 3;
        DateTime deliveryDate = DateTime.Today.AddDays( DeliveryTimeDays );

        while ( true )
        {
            PrintMenu();
            string myChoice = Console.ReadLine();
            Enum.TryParse( myChoice, out MenuOption choice );
            switch ( choice )
            {
                case MenuOption.Exit:
                    Console.WriteLine( "Goodbye!" );
                    return;
                case MenuOption.Place:
                    Console.WriteLine( "Start creating a new order.\n" );
                    break;
                default:
                    Console.WriteLine( "Invalid selection. Please enter 1 or 2." );
                    continue;
            }

            try
            {
                string productName = ReadNonEmptyString( "Enter the product name: " );
                int quantity = GetQuantity( "Enter the quantity: " );
                string name = ReadNonEmptyString( "Enter your name: " );
                string address = ReadNonEmptyString( "Enter the delivery address: " );

                Console.WriteLine( $" \nHello, {name}, you ordered {quantity} {productName} to be delivered to {address}. Is this correct? (yes/no)" );
                var answer = Confirm();
                if ( ProcessConfirmation( answer, name, quantity, productName, address, deliveryDate ) )
                    return;
            }
            catch ( Exception ex ) when (
                     ex is FormatException
                  || ex is ArgumentOutOfRangeException
                  || ex is ArgumentException )
            {
                if ( ex is FormatException )
                {
                    Console.WriteLine( "Please enter a positive integer. Please try again.\n" );
                }
                else
                {
                    Console.WriteLine( $"Error: {ex.Message} Please try again.\n" );
                }
            }
        }
    }

    private static int GetQuantity( string prompt )
    {
        Console.Write( prompt );
        string input = Console.ReadLine()!;
        int value = int.Parse( input );
        if ( value <= 0 )
            throw new ArgumentOutOfRangeException( nameof( value ), "Number must be greater than zero." );
        return value;
    }

    private static Confirmation Confirm()
    {
        while ( true )
        {
            string input = Console.ReadLine()?.Trim().ToLower();
            switch ( input )
            {
                case "yes":
                    return Confirmation.Yes;
                case "no":
                    return Confirmation.No;
                default:
                    Console.WriteLine( "Invalid input! Please enter 'yes' or 'no'." );
                    break;
            }
        }
    }

    private static string ReadNonEmptyString( string prompt )
    {
        Console.Write( prompt );
        string? input = Console.ReadLine();
        if ( string.IsNullOrWhiteSpace( input ) )
            throw new ArgumentException( "Input cannot be empty." );
        return input!;
    }

    private static void PrintMenu()
    {
        Console.WriteLine( "ORDER MANAGER" );
        Console.WriteLine( "1 – Place a new order" );
        Console.WriteLine( "2 – Exit" );
        Console.Write( "Select an option (1 or 2): " );
    }

    private static bool ProcessConfirmation( Confirmation answer, string name, int quantity, string productName, string address, DateTime deliveryDate )
    {
        if ( answer == Confirmation.Yes )
        {
            Console.WriteLine();
            Console.WriteLine( $"{name}! Your order of {quantity} {productName} has been placed! Expect delivery to {address} {deliveryDate:yyyy-MM-dd}." );
            return true;
        }
        else
        {
            Console.WriteLine( "Order cancelled. Returning to main menu. \n" );
            return false;
        }
    }
}
