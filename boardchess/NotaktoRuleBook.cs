using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace boardchess
{

    public class NotaktoRuleBook : IRulebook
    {
        private const String _name = "Notako";
        
        public String Name
        {
            get {
                return _name;
            }
        }

        public int getNumGameBoard()
        {
            return 3;
        }
        public (int,int) getGameBoardSize()
        {
            return (3, 3);
        }
        public char getPlayerMarker(int playerNumber)
        {
            // this game both player same marker
            return 'x';
        }
        public bool checkValidMove(GameContext gameContext, Move move)
        {
            (int boardId, int row, int column) = move.position;
            return gameContext.boards.getBoard(boardId).at(row, column) == ' ';
        }
        public bool checkHorizontal(GameBoard gameBoard)
        {
            
            for (int i = 0; i< gameBoard.boardsize.Item1; i++)
            {
                int sum = 0;
                for (int j = 0; j < gameBoard.boardsize.Item2; j++)
                {
                    if (gameBoard.at(i,j) != ' ')
                    {
                        sum++;
                    }
                }
                if (sum == gameBoard.boardsize.Item2)
                {
                    return true;
                }
            }
            return false;
        }
        public bool checkVertical(GameBoard gameBoard)
        {
            for (int i = 0; i < gameBoard.boardsize.Item1; i++)
            {
                int sum = 0;
                for (int j = 0; j < gameBoard.boardsize.Item2; j++)
                {
                    if (gameBoard.at(j, i) != ' ')
                    {
                        sum++;
                    }
                }
                if (sum == gameBoard.boardsize.Item2)
                {
                    return true;
                }
            }
            return false;
        }
        public bool checkDiagonal(GameBoard gameBoard)
        {
            int sum = 0;
            for (int i = 0; i < gameBoard.boardsize.Item1; i++)
            {
                if (gameBoard.at(i, i) != ' ')
                {
                    sum++;
                }
                if (sum == gameBoard.boardsize.Item2)
                {
                    return true;
                }
            }
            sum = 0;
            for (int i = 0; i < gameBoard.boardsize.Item1; i++)
            {
                if (gameBoard.at(i, gameBoard.boardsize.Item1 - i - 1 ) != ' ')
                {
                    sum++;
                }
                if (sum == gameBoard.boardsize.Item2)
                {
                    return true;
                }
            }
            return false;
        }
        public  VictoryState CheckWinner(GameContext gameContext)
        {
            
            for (int i = 0; i< gameContext.boards.Length();i++)
            {
                GameBoard board = gameContext.boards.getBoard(i);
                if (checkHorizontal(board) || checkVertical(board) || checkDiagonal(board))
                {
                    if (gameContext.history.moves[gameContext.history.moves.Count()-1].playerId == 1)
                    {
                        return VictoryState.lose;
                    }
                    else
                    {
                        return VictoryState.win;
                    }
                }
            }
            
            return VictoryState.neither;

        }
        public  bool checkValidMove()
        {
            return true;
        }
    }
    
}
