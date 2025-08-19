using OrderManager.Enums;

namespace OrderManager.Services
{
    public static class ConsoleService
    {
        public static void PrintMenu()
        {
            Console.WriteLine( "ORDER MANAGER" );
            Console.WriteLine( "1 � Place a new order" );
            Console.WriteLine( "2 � Exit" );
            Console.Write( "Select an option (1 or 2): " );
        }

        public static string ReadNonEmptyString( string prompt )
        {
            Console.Write( prompt );
            string? input = Console.ReadLine();
            if ( string.IsNullOrWhiteSpace( input ) )
                throw new ArgumentException( "Input cannot be empty." );
            return input!;
        }

        public static int ReadInt( string prompt )
        {
            Console.Write( prompt );
            string input = Console.ReadLine()!;
            int value = int.Parse( input );
            if ( value <= 0 )
                throw new ArgumentOutOfRangeException( nameof( value ), "Number must be greater than zero." );
            return value;
        }

        public static Confirmation ReadConfirmation()
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

        public static void WriteGoodbye() => Console.WriteLine( "Goodbye!" );
        public static void WriteStartOrder() => Console.WriteLine( "Start creating a new order.\n" );
        public static void WriteInvalidSelection() => Console.WriteLine( "Invalid selection. Please enter 1 or 2." );
        public static void WriteOrderCancelled() => Console.WriteLine( "Order cancelled. Returning to main menu.\n" );
        public static void WriteInvalidInt() => Console.WriteLine( "Please enter a positive integer. Please try again.\n" );
        public static void WriteError( string message ) => Console.WriteLine( $"Error: {message} Please try again.\n" );
        public static void WriteOrderSummary( string name, int quantity, string productName, string address )
        {
            Console.WriteLine( $"\nHello, {name}, you ordered {quantity} {productName} to be delivered to {address}. Is this correct? (yes/no)" );
        }

        public static void WriteOrderPlaced( string name, int quantity, string productName, string address, DateTime deliveryDate )
        {
            Console.WriteLine( $"\n{name}! Your order of {quantity} {productName} has been placed! Expect delivery to {address} {deliveryDate:yyyy-MM-dd}.\n" );
        }
    }
}