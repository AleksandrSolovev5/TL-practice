public class Program
{
    public static void Main()
    {
        const int DeliveryTimeDays = 3;
        DateTime deliveryDate = DateTime.Today.AddDays( DeliveryTimeDays );

        while ( true )
        {
            Console.WriteLine( "ORDER MANAGER" );
            Console.WriteLine( "1 – Place a new order" );
            Console.WriteLine( "2 – Exit" );
            Console.Write( "Select an option (1 or 2): " );

            string choice = Console.ReadLine();
            if ( choice == "2" )
            {
                Console.WriteLine( "Goodbye!" );
                return;
            }
            else if ( choice != "1" )
            {
                Console.WriteLine( "Invalid selection. Please enter 1 or 2." );
                continue;
            }

            Console.Write( "Enter the product name: " );
            string productName = Console.ReadLine();

            Console.Write( "Enter the quantity: " );
            int quantity = GetNumber();

            Console.Write( "Enter your name: " );
            string name = Console.ReadLine();

            Console.Write( "Enter the delivery address: " );
            string address = Console.ReadLine();

            Console.WriteLine();
            Console.WriteLine( $"Hello, {name}, you ordered {quantity} {productName} to be delivered to {address}. Is this correct? (yes/no)" );

            var answer = Confirm();
            if ( answer == Confirmation.Yes )
            {
                Console.WriteLine();
                Console.WriteLine( $"{name}! Your order of {quantity} {productName} has been placed! Expect delivery to {address} {deliveryDate.ToShortDateString()}." );
                return;
            }
            else
            {
                Console.WriteLine( "Order cancelled. Returning to main menu." );
            }
        }
    }

    private static int GetNumber()
    {
        while ( true )
        {
            try
            {
                string input = Console.ReadLine();
                int value = int.Parse( input );
                if ( value <= 0 )
                {
                    throw new ArgumentOutOfRangeException( nameof( value ), "Value must be positive." );
                }
                return value;
            }
            catch ( FormatException )
            {
                Console.WriteLine( "Invalid number format! Please enter a valid integer." );
            }
            catch ( ArgumentOutOfRangeException )
            {
                Console.WriteLine( "Number must be greater than zero. Try again." );
            }
        }
    }

    enum Confirmation { Yes, No }
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
}
