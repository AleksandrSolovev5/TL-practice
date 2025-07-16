Main();

void Main()
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
        Console.WriteLine( $"Hello, {name}, you ordered {quantity} {productName} to be delivered to {address}. Is this correct? (y/n)" );
        if ( Confirm() )
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

int GetNumber()
{
    while ( true )
    {
        string input = Console.ReadLine();
        if ( int.TryParse( input, out int value ) && value > 0 )
            return value;

        Console.WriteLine( "Invalid number! Please enter a valid positive integer." );
    }
}

bool Confirm()
{
    while ( true )
    {
        string input = Console.ReadLine();
        switch ( input )
        {
            case "y":
                return true;
            case "n":
                return false;
            default:
                Console.WriteLine( "Invalid input! Please enter 'y' or 'n'." );
                break;
        }
    }
}
