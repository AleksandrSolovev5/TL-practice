using Casino;
using System;

const int multiplicator = 1;
var rnd = new Random();

const string GameName = @"
 ######    ######    ######    #    #    #     ####   
#          #    #    #              ##   #    #    #
#          #    #    #         #    # #  #    #    #
#          ######    ######    #    #  # #    #    #
#          #    #         #    #    #   ##    #    #
 ######    #    #    ######    #    #    #     #### ";

PrintGameName( GameName );

Console.Write( "Please enter a starting balance: " );
if ( !int.TryParse( Console.ReadLine(), out int balance ) || balance <= 0 )
{
    Console.WriteLine( "Invalid balance value. Must be a positive integer." );
    return;
}

Operation? operation = null;

while ( operation != Operation.Exit )
{
    PrintMenu();

    operation = ReadOperation();
    if ( operation == null )
    {
        Console.WriteLine( "Invalid selection. Please enter 1, 2 or 3." );
        continue;
    }

    switch ( operation.Value )
    {
        case Operation.Play:
            PlayRound( ref balance );
            if ( balance == 0 )
            {
                Console.WriteLine( "Your balance is 0. Game over." );
                return;
            }
            break;

        case Operation.CheckBalance:
            Console.WriteLine( $"Your current balance is: {balance}" );
            break;

        case Operation.Exit:
            Console.WriteLine( "Thank you for playing!" );
            break;

        default:
            Console.WriteLine( "Unsupported operation." );
            break;
    }
}


static void PrintGameName( string gameName )
{
    Console.WriteLine( gameName );
    Console.WriteLine();
}

static Operation? ReadOperation()
{
    string opStr = Console.ReadLine()?.Trim() ?? string.Empty;
    return Enum.TryParse( opStr, out Operation op ) ? op : null;
}

void PlayRound( ref int balance )
{
    Console.Write( "Enter your bet: " );
    int bet = GetBet( balance );

    int roll = rnd.Next( 1, 21 );
    Console.WriteLine( $"You rolled: {roll}" );

    if ( roll >= 18 )
    {
        int winAmount = bet * ( 1 + ( multiplicator * ( roll % 17 ) ) );
        balance += winAmount;
        Console.WriteLine( $"Congratulations! You won {winAmount}. New balance: {balance}" );
    }
    else
    {
        balance -= bet;
        Console.WriteLine( $"Sorry, you lost {bet}. New balance: {balance}" );
    }
}

static int GetBet( int balance )
{
    while ( true )
    {
        string input = Console.ReadLine() ?? string.Empty;
        if ( int.TryParse( input, out int bet ) && bet > 0 && bet <= balance )
        {
            return bet;
        }

        if ( bet > balance )
        {
            Console.WriteLine( $"Your bet cannot exceed your balance ({balance}). Try again:" );
        }
        else
        {
            Console.WriteLine( "Invalid bet! Enter a positive integer not exceeding your balance:" );
        }
    }
}

static void PrintMenu()
{
    Console.WriteLine();
    Console.WriteLine( "MENU:" );
    Console.WriteLine( "1 - Play" );
    Console.WriteLine( "2 - Check Balance" );
    Console.WriteLine( "3 - Exit" );
    Console.Write( "Select an option: " );
}
