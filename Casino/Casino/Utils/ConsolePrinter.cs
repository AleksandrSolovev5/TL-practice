namespace Casino.Utils;

public class ConsolePrinter
{
    private const string GameName = @"
 ######    ######    ######    #    #    #     ####   
#          #    #    #              ##   #    #    #
#          #    #    #         #    # #  #    #    #
#          ######    ######    #    #  # #    #    #
#          #    #         #    #    #   ##    #    #
 ######    #    #    ######    #    #    #     #### ";

    public static void PrintGameName()
    {
        Console.WriteLine( GameName + '\n' );
    }

    public static void PrintMenu()
    {
        Console.WriteLine( "\nMENU:" );
        Console.WriteLine( "1 - Play" );
        Console.WriteLine( "2 - Check Balance" );
        Console.WriteLine( "3 - Exit" );
        Console.Write( "Select an option: " );
    }
    public static void PrintRequestBalance()
    {
        Console.Write( "Please enter a starting balance: " );
    }

    public static void PrintInvalidBalance()
    {
        Console.WriteLine( "Invalid balance value. Must be a positive integer." );
    }

    public static void PrintInvalidSelection()
    {
        Console.WriteLine( "Invalid selection. Please enter 1, 2 or 3." );
    }

    public static void PrintGameOver()
    {
        Console.WriteLine( "Your balance is 0. Game over." );
    }

    public static void PrintCurrentBalance( int balance )
    {
        Console.WriteLine( $"\nYour current balance is: {balance}" );
    }

    public static void PrintThankYou()
    {
        Console.WriteLine( "Thank you for playing!" );
    }

    public static void PrintRequestBet()
    {
        Console.Write( "Enter your bet: " );
    }

    public static void PrintRoll( int roll )
    {
        Console.WriteLine( $"You rolled: {roll}" );
    }

    public static void PrintWin( int winAmount, int balance )
    {
        Console.WriteLine( $"Congratulations! You won {winAmount}. New balance: {balance}" );
    }

    public static void PrintLose( int bet, int balance )
    {
        Console.WriteLine( $"Sorry, you lost {bet}. New balance: {balance}" );
    }

    public static void PrintBetExceedsBalance( int balance )
    {
        Console.WriteLine( $"Your bet cannot exceed your balance ({balance}). Try again:" );
    }

    public static void PrintInvalidBet( int balance )
    {
        Console.WriteLine( "Invalid bet! Enter a positive integer not exceeding your balance:" );
    }
}




