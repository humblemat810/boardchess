using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Enumeration;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;


namespace boardchess
{
    public static class HelperFunctions
    {
        

        public static string GetValidatedStr(string prompt)
        {
            string result = "";
            bool isValid = false;

            while (!isValid)
            {
                Console.WriteLine(prompt);
                string input = Console.ReadLine();

                // Try to convert the input to an integer
                if (input != null)
                {
                    isValid = true;
                    result = input;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                }
            }

            return result;
        }

        public static string GetValidatedStrOptions(string prompt, List<string> options)
        {
            string result = "";
            bool isValid = false;

            while (!isValid)
            {
                Console.WriteLine(prompt);
                string input = Console.ReadLine();

                // Try to convert the input to an integer
                if (input != null &&   options.Contains(input, StringComparer.OrdinalIgnoreCase))
                {
                    isValid = true;
                    result = input;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                }
            }

            return result;
        }
    }
    
    public class GameEngine
    {
        Player player1, player2, currentPlayer;
        List<string> availableGameOptions = new List<string> { "Notakto" };
        IRulebook RuleSet;
        String gameType;
        History history;
        GameContext gameContext;
        //GameBoard[] boards;
        GameBoardList boards; 
        public GameEngine()
        {
            
                switch (HelperFunctions.GetValidatedStrOptions("input number for \n0 : new game\n1 : load game", ["0", "1"]))
                {
                    case "0":
                        history = new History();
                        gameType = null;
                        setupGame();
                        break;
                    case "1":
                        loadGame();
                        setupGame();
                        applyHistory();
                        break;
                        String fileName = "";
                    
                        while (!File.Exists("./savedGames/" + fileName)) {
                            fileName = HelperFunctions.GetValidatedStr("Please input file name");
                            }
                        StreamReader reader = new StreamReader("./savedGames/" + fileName);
                        gameType = reader.ReadLine();
                        history = History.loadHistory(reader);
                        break;

                }
                
                gameContext = new GameContext(boards, [player1, player2], history, RuleSet, this);
                runGame();
            
        }
        public void applyHistory()
        {
            Player[] players = [player1, player2];
            foreach (Move move in history.moves)
            {
                (int boardId, int row, int col) = move.position;
                GameBoard targetBoard = boards.getBoard(boardId);
                Player player = players[move.playerId - 1];

                targetBoard.update(players, move);

            }
        }
        public void saveGame()
        {
            String fileName = HelperFunctions.GetValidatedStr("Please input file name");
            Directory.CreateDirectory("./savedGames/");
            while (File.Exists("./savedGames/" + fileName))
            {
                Console.WriteLine("file already exist, please choose another name");
                fileName = HelperFunctions.GetValidatedStr("Please input file name");
            }
            StreamWriter writer = new StreamWriter("./savedGames/" + fileName);
            writer.WriteLine(gameType);
            history.dumpHistory(writer);
        }
        public void loadGame()
        {
            String fileName = HelperFunctions.GetValidatedStr("Please input file name");
            Directory.CreateDirectory("./savedGames/");
            while (!File.Exists("./savedGames/" + fileName))
            {
                Console.WriteLine("file not exist, please choose an existing saved file");
                fileName = HelperFunctions.GetValidatedStr("Please input file name");
            }
            StreamReader reader = new StreamReader("./savedGames/" + fileName);
            gameType = reader.ReadLine();
            history = History.loadHistory(reader);
        }
        public void traverseMode()
        {
            Console.WriteLine("now entering historical tranverse mode");
            Console.WriteLine("press up arrow to undo move");
            Console.WriteLine("press down arrow to redo move");
            Console.WriteLine("press enter to continue");
            bool quitting = false;
            int currentIndex = gameContext.history.moves.Count() - 1;
            do
            {
                // Read a key without displaying it in the console immediately
                var keyInfo = Console.ReadKey(intercept: true);
                bool needRefresh = false;
                if (keyInfo.Key == ConsoleKey.UpArrow)
                {

                    if (currentIndex >= 0)
                    {
                        
                        Move currentMove = gameContext.history.moves[currentIndex];
                        (int board, int row, int column) = currentMove.position;
                        gameContext.boards.getBoard(board).update(row, column, ' ');
                        currentIndex--;
                        needRefresh = true;
                    }

                }
                if (keyInfo.Key == ConsoleKey.DownArrow)
                {

                    if (currentIndex < gameContext.history.moves.Count()-1)
                    {
                        currentIndex++;
                        Move currentMove = gameContext.history.moves[currentIndex];
                        (int board, int row, int column) = currentMove.position;
                        char markerToPut = gameContext.players[currentMove.playerId-1].moveMarker;
                        gameContext.boards.getBoard(board).update(row, column, markerToPut);
                        
                        needRefresh = true;
                    }
                    

                }
                if (keyInfo.Key == ConsoleKey.Enter)
                {
                    if (currentIndex >= 0 && currentIndex < gameContext.history.moves.Count()) { 
                        gameContext.history.removeMove(currentIndex+1);
                        needRefresh = true;
                    }
                    quitting = true;
                    
                }

                if (keyInfo.Key == ConsoleKey.Q)
                {
                    quitting = true;
                }
                if (needRefresh)
                {
                    gameContext.boards.display();
                }
            } while (!quitting);
        }
        public void setupGame() {
            while (gameType == null) { 
                gameType = HelperFunctions.GetValidatedStrOptions("input game type Notakto (Gomoku not supported): ",
                availableGameOptions
                );
            }
            if (gameType.ToLower() == "Notakto".ToLower())
            {
                this.RuleSet = new NotaktoRuleBook();
                Console.WriteLine($"Setting up for game {RuleSet.Name}" );
                boards = new GameBoardList(RuleSet.getNumGameBoard(), RuleSet.getGameBoardSize());
            }
            
            if (HelperFunctions.GetValidatedStrOptions("is player 1 human? (\"yes\", \"no\")", ["yes", "no"]) == "yes")
            {
                player1 = new HumanPlayer(RuleSet.getPlayerMarker(1), 1);
                if (HelperFunctions.GetValidatedStrOptions("is player 2 human? (\"yes\", \"no\")", ["yes", "no"]) == "yes")
                {
                    player2 = new HumanPlayer(RuleSet.getPlayerMarker(1), 2);
                }
                else
                {
                    player2 = new ComputerPlayer(RuleSet.getPlayerMarker(1), 2);
                }
            }
            else
            {
                player1 = new ComputerPlayer(RuleSet.getPlayerMarker(1), 1);
                player2 = new HumanPlayer(RuleSet.getPlayerMarker(1), 2);
            }
            currentPlayer = player1;
            
        }
        public bool checkGameEndConditionMet(VictoryState victoryState)
        {
            return (victoryState != VictoryState.neither);
        }
        private Player getNextPlayer()
        {
            if (history.moves.Count == 0)
            {
                currentPlayer = player1;
            }
            else
            {
                currentPlayer = history.moves[history.moves.Count-1].playerId==1 ? player2 : player1 ;
            }
            return currentPlayer;
        }
        private bool processPlayerInput(Player player, bool gameEnded)
        {
            return playerInputProcessor.GetAndProcessPlayerInput(gameContext, player, gameEnded);
        }
        private void displayWinnerMessage(VictoryState victoryState)
        {
            if (victoryState == VictoryState.win)
            {
                Console.WriteLine("player 1 has won");
            }
            if (victoryState == VictoryState.lose)
            {
                Console.WriteLine("player 2 has won");
            }
            if (victoryState == VictoryState.tie)
            {
                Console.WriteLine("It's a tie");
            }
        }
        public void runGame()
        {
            VictoryState victoryState;
            bool quitGame = false;
            bool gameEnded = false;
            victoryState = RuleSet.CheckWinner(gameContext);
            gameEnded = checkGameEndConditionMet(victoryState);
            do {
                
                boards.display();
                Help.showHelpCommand();
                quitGame = processPlayerInput(currentPlayer, gameEnded);
                if (quitGame)
                {
                    break;
                }
                victoryState = RuleSet.CheckWinner(gameContext);
                gameEnded = checkGameEndConditionMet(victoryState);
                Console.Clear();
                displayWinnerMessage(victoryState);
                if (!gameEnded || (currentPlayer.GetType() == typeof(ComputerPlayer)))
                {

                    currentPlayer = getNextPlayer();
                }

            } while (!quitGame);
            Console.Clear();
        }

    }
}
