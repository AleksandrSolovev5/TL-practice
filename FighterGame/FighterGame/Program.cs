using Fighters.Models.Fighters;

namespace Fighters;

public static class Program
{
    private static List<IFighter> Fighters { get; } = [];
    private static GameManager gameManager = new();
    public static void Main()
    {
        PrintMenu();
        string? command = Console.ReadLine();

        while ( command != "3" )
        {
            switch ( command )
            {
                case "1":
                    AddFighter();
                    break;
                case "2":
                    Fight();
                    break;
                default:
                    Console.WriteLine( "The command entered was incorrect." );
                    break;
            }

            PrintMenu();
            command = Console.ReadLine();
        }
        Console.WriteLine( "Goodbye!" );
    }

    private static void PrintMenu()
    {
        Console.WriteLine( "==== Menu ====" );
        Console.WriteLine( "1. Add fighter" );
        Console.WriteLine( "2. Start fight" );
        Console.WriteLine( "3. Exit\n" );
    }

    private static void AddFighter()
    {
        if ( Fighters.Count >= 2 )
        {
            Console.WriteLine( "\nThere are already enough fighters for the fight.\n" );
            return;
        }
        IFighter fighter = FighterFactory.CreateFighter();
        Fighters.Add( fighter );
        Console.WriteLine( "\nFighter added successfully.\n" );
    }

    private static void Fight()
    {
        if ( Fighters.Count == 2 )
        {
            gameManager.SetFighters( Fighters[ 0 ], Fighters[ 1 ] );
            gameManager.StartFight();
        }
        else
        {
            Console.WriteLine( "Add at least two fighters first." );
        }
    }
}

