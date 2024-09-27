using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace boardchess
{
    public class HumanPlayer : Player
    {
        public override char moveMarker
        {
            get;
            set;
        }
        private new String playerName
        {
            get {
                return "Player " + id.ToString();
            }
        }
        public HumanPlayer(char marker, int id) { 
            this.moveMarker = marker;
            this.id = id;
        }
        public override String getMoveFromPlayer(GameContext gameContext)
        {
            //Console.WriteLine($"Now {playerName} turn");
            String playerInput = HelperFunctions.GetValidatedStr($"Waiting input from {playerName}");

            return playerInput;
        }

    }
}
