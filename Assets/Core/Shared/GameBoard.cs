namespace Core.Shared
{
    public struct GameBoard
    {
        public BoardSquare[,] Board;

        public GameBoard(BoardSquare[,] boardSquares)
        {
            Board = boardSquares;
        }
    }
}