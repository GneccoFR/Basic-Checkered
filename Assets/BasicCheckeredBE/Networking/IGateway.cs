using BasicCheckeredBE.Networking.DTOs;
using Cysharp.Threading.Tasks;

namespace BasicCheckeredBE.Networking
{
    public interface IGateway
    {
        UniTask Initialize();
        UniTask<BoardDTO> GetNewBoard();
        UniTask<AttemptToMoveDTO> AttemptToMove(PieceDTO piece, SquareDTO originalSquare, SquareDTO targetSquare);
    }
}