using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace boardchess
{
    public class Move
    {
        public int playerId;
        public (int board, int row, int column) position; // boardId, index on board
        public Move(int playerId, (int boardId, int row, int col) position )
        {
            this.playerId = playerId;
            this.position = position;
        }

        public override String ToString()
        {
            return playerId.ToString()+','+position.ToString();
        }
        public static Move FromString(String input)
        {
            int boardId = 0, row = 0, column = 0, playerId = 0;
            string[] splitted = input.Split(',');
            if (int.TryParse(splitted[0], out boardId)) { };
            if (int.TryParse(splitted[1], out row)) { };
            if (int.TryParse(splitted[2], out column)) { };
            if (int.TryParse(splitted[3], out playerId)) { };
            Move move = new Move(playerId, (boardId, row, column));
            return move;
        }
    }
}
