using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace boardchess
{
    public class GameBoard
    {
        
        public (int, int) boardsize = (0, 0);
        private char[][] board; // row, column
        public char at(int row, int col)
        {
            return board[row][col];
        }
        public void update(int row, int col, char marker)
        {
            board[row][col] = marker;
        }
        public void update(Player[] players, Move move)
        {
            int row = move.position.row;
            int col = move.position.column;
            board[row][col] = players[move.playerId-1].moveMarker;
        }
        public void display()
        {
            (int nrow, int ncol) = boardsize;
            string rowSeperator = new string('-', ncol * 2 + 1);
            foreach (char[] row in board) {
                
                Console.WriteLine(rowSeperator);
                foreach (char col in row)
                {
                    Console.Write("|");
                    Console.Write(col);
                }
                Console.WriteLine("|");
            ;
            }
            Console.WriteLine(rowSeperator);
        }
        public  GameBoard((int, int) boardsize) {
            
            
            (int nrow, int ncol) = boardsize;
            this.boardsize = boardsize;
            board = new char[nrow][];
            // initialise board
            for (int i = 0; i < nrow; i++)
            {
                board[i] = new char[ncol];
                for (int j = 0; j < ncol; j++)
                    board[i][j] = ' ';
                
            }
        }
    }
    

}

