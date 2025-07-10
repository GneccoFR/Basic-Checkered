using Core.Networking.DTOs;
using Core.Shared;
using Cysharp.Threading.Tasks;

namespace Core.Networking
{
    public interface IGateway
    {
        UniTask Initialize();
        UniTask<NewGameDTO> GetNewGame();
        UniTask<AttemptToMoveDTO> AttemptToMove(PieceDTO piece, SquareDTO originalSquare, SquareDTO targetSquare);
    }
}