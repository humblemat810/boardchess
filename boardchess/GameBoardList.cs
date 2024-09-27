namespace boardchess
{
    public class GameBoardList
    {
        GameBoard[] gameBoardList;
        public void display()
        {
            for (int i = 0; i < gameBoardList.Length; i++) {
                Console.WriteLine($"board {i}");
                gameBoardList[i].display();
                Console.WriteLine("\n=====================================================================");
            }
            
        }
        public GameBoardList(int numGameBoard, (int row, int col) boardSize)
        {
            this.gameBoardList = new GameBoard[numGameBoard];
            for(int i =0; i<gameBoardList.Length; i++)
            {
                this.gameBoardList[i] = new GameBoard(boardSize);
            }
        }
        public int Length() { return gameBoardList.Length; }
        public GameBoard getBoard(int index)
        {
            return gameBoardList[index];
        }

    }
}
