using Fighters.Models;
using Fighters.Models.Fighters;

namespace Fighters.Utils;

public static class ConsolePrinter
{
    public static void PrintMessage( string message )
    {
        Console.WriteLine( message );
    }

    public static void PrintMenu()
    {
        Console.WriteLine( "==== Menu ====" );
        Console.WriteLine( "1. Add fighter" );
        Console.WriteLine( "2. Start fight" );
        Console.WriteLine( "3. Exit\n" );
    }

    public static void PrintGoodbye()
    {
        Console.WriteLine( "Goodbye!" );
    }

    public static void PrintIncorrectCommand()
    {
        Console.WriteLine( "The command entered was incorrect." );
    }

    public static void PrintFighterLimit()
    {
        Console.WriteLine( "\nThere are already enough fighters for the fight.\n" );
    }

    public static void PrintFighterAdded()
    {
        Console.WriteLine( "\nFighter added successfully.\n" );
    }

    public static void PrintNotEnoughFighters()
    {
        Console.WriteLine( "Add at least two fighters first.\n" );
    }

    public static void PrintOptionsList<T>( string category, IReadOnlyList<T> options ) where T : IModel
    {
        Console.WriteLine( $"Select a {category}:" );
        for ( int i = 0; i < options.Count; i++ )
        {
            Console.WriteLine( $"{i + 1}. {options[ i ].Name}" );
        }
    }

    public static void PrintFighterInfo( IFighter fighter )
    {
        Console.WriteLine( $"{fighter.Name} - Base damage: {fighter.CalculateDamage()}, Health: {fighter.MaxHealth}, Armor: {fighter.CalculateArmor()}" );
    }

    public static void PrintFighterStart( string name )
    {
        Console.WriteLine( $"{name} starts the fight!!!" );
    }

    public static void PrintRound( int round )
    {
        Console.WriteLine( $"\n=== Round {round} ===" );
    }

    public static void PrintAttack( string attacker, string target, double damage, double targetHealth )
    {
        Console.WriteLine( $"{attacker} attacks {target} with {damage} damage, {target}'s health: {targetHealth}" );
    }

    public static void PrintDraw()
    {
        Console.WriteLine( "Draw! Round limit exceeded." );
    }

    public static void PrintWinner( string name )
    {
        Console.WriteLine( $"\n{name} wins the fight!" );
    }

    public static void PrintFighterInfoHeader()
    {
        Console.WriteLine( "\n=== Fighters Information ===" );
    }
}