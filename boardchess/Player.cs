using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace boardchess
{
    public abstract class Player
    {
        protected String playerName = "";
        public int id; //e.g player 1, player 2 
        abstract public String getMoveFromPlayer(GameContext gameContext);
        public abstract char moveMarker
        {
            get;
            set;
        }

    }
}
