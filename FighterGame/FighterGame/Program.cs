using Fighters.Models.Fighters;
using Fighters.Enums;
using Fighters.Utils;

namespace Fighters;

public static class Program
{
    private static List<IFighter> Fighters { get; } = [];
    private static GameManager gameManager = new();
    public static bool isFinished = false;
    public static void Main()
    {
        ConsolePrinter.PrintMenu();
        string? command = Console.ReadLine();

        while ( true )
        {
            if ( int.TryParse( command, out int commandValue ) && Enum.IsDefined( typeof( MenuCommand ), commandValue ) )
            {
                MenuCommand menuCommand = ( MenuCommand )commandValue;
                switch ( menuCommand )
                {
                    case MenuCommand.AddFighter:
                        AddFighter();
                        break;
                    case MenuCommand.StartFight:
                        Fight();
                        if ( isFinished )
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
            isFinished = gameManager.StartFight();
        }
        else
        {
            ConsolePrinter.PrintNotEnoughFighters();
        }
    }
}

