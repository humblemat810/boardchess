using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace boardchess
{
    public class History
    {
        public List<Move> moves = new List<Move>();
        public History() { 
        }
        
        public static History loadHistory(StreamReader reader)
        {
            History history = new History();
            int movesToRead;
            int.TryParse(reader.ReadLine(), out movesToRead);
            for (int i = 0; i<movesToRead; i++)
            {
                Move newMove = Move.FromString(reader.ReadLine());
                history.addMove(newMove, history.moves.Count());
            }
            return history;
        }

        public void dumpHistory(StreamWriter writer)
        {
            using (writer)
            {
                writer.WriteLine(moves.Count());
                foreach (Move move in moves)
                {
                    (int b, int r, int c) = move.position;
                    writer.WriteLine($"{b},{r},{c},{move.playerId}");
                }
            }
        }
        public void addMove(Move move, int index)
        {
            removeMove(index);   
            moves.Add(move);
        }
        public void addMove(Move move)
        {
            moves.Add(move);
        }
        public void removeMove(int index) // remove all moves from index onwards
        {
            moves = moves.GetRange(0, index);
        }

    }
}
