using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace boardchess
{
    public class ComputerPlayer : Player
    {
        public override char moveMarker
        {
            get;
            set;
        }
        private new String playerName
        {
            get
            {
                return "Player " + id.ToString();
            }
        }
        public ComputerPlayer(char marker, int id)
        {
            this.moveMarker = marker;
            this.id = id;
        }
        public override String getMoveFromPlayer(GameContext gameContext)
        {
            Random random = new Random();
            //Console.WriteLine($"Now {playerName} turn");
            int boardId = random.Next(0, gameContext.boards.Length());
            (int row, int column) = gameContext.boards.getBoard(boardId).boardsize;
            Console.WriteLine($"Waiting input from {playerName}");
            String playerInput = $"{boardId},{random.Next(0, row)},{random.Next(0, column)}";
            Console.WriteLine(playerInput);
            return playerInput;
        }

    }
}
