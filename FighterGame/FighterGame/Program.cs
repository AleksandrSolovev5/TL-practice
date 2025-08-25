using Fighters.Models.Fighters;
using Fighters.Enums;
using Fighters.Utils;

namespace Fighters;

public static class Program
{
    private static List<IFighter> Fighters { get; } = [];
    private static GameManager gameManager = new();
    public static bool isFightFinished = false;

    public static void Main()
    {
        ConsolePrinter.PrintMenu();
        string? command = Console.ReadLine();

        while ( true )
        {
            if ( IsValidCommand( command, out MenuCommand menuCommand ) )
            {
                switch ( menuCommand )
                {
                    case MenuCommand.AddFighter:
                        AddFighter();
                        break;
                    case MenuCommand.StartFight:
                        if ( HandleStartFight() )
                            return;
                        break;
                    case MenuCommand.Exit:
                        ConsolePrinter.PrintGoodbye();
                        return;
                    default:
                        ConsolePrinter.PrintIncorrectCommand();
                        break;
                }
            }
            else
            {
                ConsolePrinter.PrintIncorrectCommand();
            }

            ConsolePrinter.PrintMenu();
            command = Console.ReadLine();
        }
    }

    private static void AddFighter()
    {
        if ( Fighters.Count >= 2 )
        {
            ConsolePrinter.PrintFighterLimit();
            return;
        }
        IFighter fighter = FighterFactory.CreateFighter();
        Fighters.Add( fighter );
        ConsolePrinter.PrintFighterAdded();
    }

    private static void Fight()
    {
        if ( Fighters.Count == 2 )
        {
            gameManager.SetFighters( Fighters[ 0 ], Fighters[ 1 ] );
            isFightFinished = gameManager.StartFight();
            return;
        }
        ConsolePrinter.PrintNotEnoughFighters();
    }

    private static bool HandleStartFight()
    {
        Fight();
        return isFightFinished;
    }

    private static bool IsValidCommand( string? command, out MenuCommand menuCommand )
    {
        menuCommand = default;

        if ( int.TryParse( command, out int commandValue ) &&
            Enum.IsDefined( typeof( MenuCommand ), commandValue ) )
        {
            menuCommand = ( MenuCommand )commandValue;
            return true;
        }

        return false;
    }
}

