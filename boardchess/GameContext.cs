using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace boardchess
{
    // data class to store a context for easy reference passing across funcitons

    public class GameContext
    {
        public GameBoardList boards;
        public Player[] players;
        public History history;
        public IRulebook RuleSet;
        public GameEngine gameEngine;
        public GameContext(GameBoardList boards, Player[] players, History history, IRulebook RuleSet, GameEngine gameEngine) { 
            this.boards = boards;
            this.history = history;
            this.players = players;
            this.RuleSet = RuleSet;
            this.gameEngine = gameEngine;
        }

    }
}
