using System.Collections.Generic;
using BasicCheckeredBE.Core.Domain;
using Cysharp.Threading.Tasks;

namespace BasicCheckeredBE.Repositories
{
    public class GameBoardRepository
    {
        private BoardSquare[,] _boardCache;

        public void LoadBoard(BoardSquare[,] board)
        {
            _boardCache = board;
        }
        
        public BoardSquare[,] GetCurrentBoard()
        {
            return _boardCache;
        }
        
        public void UpdateBoardSquares(List<BoardSquare> updatedBoardSquares)
        {
            foreach (var square in updatedBoardSquares)
            {
                _boardCache[(int)square.Coordinates.X, (int)square.Coordinates.Y] = square;
            }
        }
    }
}