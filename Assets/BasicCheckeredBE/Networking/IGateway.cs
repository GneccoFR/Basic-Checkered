using BasicCheckeredBE.Networking.DTOs;
using Cysharp.Threading.Tasks;

namespace BasicCheckeredBE.Networking
{
    public interface IGateway
    {
        UniTask Initialize();
        UniTask<NewGameDTO> GetNewGame();
        UniTask<AttemptToMoveDTO> AttemptToMove(SquareDTO originalSquare, SquareDTO targetSquare);
    }
}