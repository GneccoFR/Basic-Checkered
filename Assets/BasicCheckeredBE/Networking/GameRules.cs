using BasicCheckeredBE.Controllers;
using BasicCheckeredBE.Networking.DTOs;
using Cysharp.Threading.Tasks;

namespace BasicCheckeredBE.Networking
{
    public class GameRules
    {
        private GameBoardController _gameBoardController;
        
        public async UniTask Initialize()
        {
            _gameBoardController = new GameBoardController();
        }
        
        public async UniTask<BoardDTO> GetNewBoard()
        {
            return await _gameBoardController.GetNewBoard();
        }
        
        public async UniTask<AttemptToMoveDTO> AttemptToMove(PieceDTO piece, SquareDTO originalSquare, SquareDTO targetSquare)
        {
            return await _gameBoardController.AttemptToMove(piece, originalSquare, targetSquare);
        }
    }
}
