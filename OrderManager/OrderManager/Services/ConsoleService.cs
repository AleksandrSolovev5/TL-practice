using OrderManager.Enums;

namespace OrderManager.Services
{
    public class ConsoleService
    {
        public void PrintMenu()
        {
            Console.WriteLine( "ORDER MANAGER" );
            Console.WriteLine( "1 – Place a new order" );
            Console.WriteLine( "2 – Exit" );
            Console.Write( "Select an option (1 or 2): " );
        }

        public string ReadNonEmptyString( string prompt )
        {
            Console.Write( prompt );
            string? input = Console.ReadLine();
            if ( string.IsNullOrWhiteSpace( input ) )
                throw new ArgumentException( "Input cannot be empty." );
            return input!;
        }

        public int ReadInt( string prompt )
        {
            Console.Write( prompt );
            string input = Console.ReadLine()!;
            int value = int.Parse( input );
            if ( value <= 0 )
                throw new ArgumentOutOfRangeException( nameof( value ), "Number must be greater than zero." );
            return value;
        }

        public void WriteLine( string message ) => Console.WriteLine( message );
        public void Write( string message ) => Console.Write( message );

        public Confirmation ReadConfirmation()
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
}