
using System.Runtime.InteropServices;

namespace boardchess
{
    public class playerInputProcessor
    {
        static public bool GetAndProcessPlayerInput(GameContext gameContext, Player player, bool gameEnded)
        {
            bool quitGame = false;
            bool inputCorrect = false;
            while (!inputCorrect)
            {
                String playerInput = player.getMoveFromPlayer(gameContext);
                switch (playerInput)
                {
                    case "help":
                        inputCorrect = true;
                        Console.Clear();
                        Help.displayHelp();
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        gameContext.boards.display();
                        break;
                    case "history":
                        inputCorrect = true;
                        gameContext.gameEngine.traverseMode();
                        break;
                    case "quit":
                    case "q":
                        quitGame = true;
                        inputCorrect = true;
                        break;
                    case "save":
                    case "s":
                        inputCorrect = true;
                        gameContext.gameEngine.saveGame();
                        break;
                    default: // move input
                        if (gameEnded)
                        {
                            Console.WriteLine("You cannot continue playing after the game ends");
                            // does not allow more moves after game ended, only allow save,
                            // wind back or rage quit game
                            break;
                        }
                        String[] moveCommand = playerInput.Split(',');
                        int boardId, row, col;
                        if (int.TryParse(moveCommand[0].Trim(), out boardId))
                        {
                            if (int.TryParse(moveCommand[1].Trim(), out row))
                            {
                                if (int.TryParse(moveCommand[2].Trim(), out col))
                                {
                                    Move newMove = new Move(player.id, (boardId, row, col));

                                    if (gameContext.RuleSet.checkValidMove(gameContext, newMove)) { 
                                        GameBoard targetBoard = gameContext.boards.getBoard(boardId);
                                        if (targetBoard.at(row, col) == ' ')
                                        {
                                            inputCorrect = true;
                                            targetBoard.update(row, col, player.moveMarker);
                                            gameContext.history.addMove(new Move(player.id, (boardId, row, col)));
                                        }
                                    }
                                    // update board
                                    // add move to history



                                }
                            }
                        }
                        break;
                }
                if (!inputCorrect)
                {
                    Console.WriteLine("Incorrect input, please try again");
                }
            }
            return quitGame;
        }
    }
}
