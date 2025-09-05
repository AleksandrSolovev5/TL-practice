using Fighters.Models.Fighters;
using Fighters.Enums;
using Fighters.Utils;

namespace Fighters;

public static class Program
{
    private static readonly GameManager gameManager = new();
    private static bool isFightFinished = false;

    public static void Main()
    {
        ConsolePrinter.PrintMenu();
        string? command = Console.ReadLine();

        while ( true )
        {
            MenuCommand? parsedCommand = ParseCommand( command );

            if ( parsedCommand is MenuCommand menuCommand )
            {
                switch ( menuCommand )
                {
                    case MenuCommand.AddFighter:
                        HandleAddFighter();
                        break;
                    case MenuCommand.StartFight:
                        HandleStartFight();
                        if ( isFightFinished )
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

    private static void HandleAddFighter()
    {
        if ( gameManager.IsMaxFightersReached() )
        {
            return;
        }
        IFighter fighter = FighterFactory.CreateFighter();

        if ( gameManager.AddFighter( fighter ) )
        {
            ConsolePrinter.PrintFighterAdded();
        }
    }

    private static void HandleStartFight()
    {
        isFightFinished = gameManager.StartFight();
    }

    private static MenuCommand? ParseCommand( string? command )
    {
        if ( int.TryParse( command, out int commandValue ) &&
            Enum.IsDefined( typeof( MenuCommand ), commandValue ) )
        {
            return ( MenuCommand )commandValue;
        }

        return null;
    }
}
