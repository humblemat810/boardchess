using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace boardchess
{
    public enum VictoryState
    {
        win = 0,
        lose = 1,
        tie = 2, // game cannot go on stalemate for Notakto
        neither = 3  // game is still going on
    }
    public interface IRulebook
    {
        public VictoryState CheckWinner(GameContext gameContext);
        bool checkValidMove(GameContext gameContext, Move move);
        int getNumGameBoard();
        (int, int) getGameBoardSize();
        char getPlayerMarker(int playerNumber);
        bool checkHorizontal(GameBoard gameBoard);
        bool checkVertical(GameBoard gameBoard);
        bool checkDiagonal(GameBoard gameBoard);
        public String Name
        {
            get;
        }
    }
}
