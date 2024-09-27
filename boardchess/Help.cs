using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace boardchess
{
    public class Help
    {
        public static void showHelpCommand()
        {
            Console.WriteLine("input 'help' for help");
        }
        public static void displayHelp()
        {
            Console.WriteLine("input 'history' to traverse game history");
            Console.WriteLine("  press up and down for trasversal, enter to continue from the move");
            Console.WriteLine("input 'save' to save game");
            Console.WriteLine("input 'quit' to quit game");
            Console.WriteLine("input board number, row number then column number to put marker in");
            Console.WriteLine("   e.g. 1,2,2 will put marker in board 1, bottom right position");
            Console.WriteLine("   all are zero indexed, first board is board 0, first row is row 0");
        }
    }
}
