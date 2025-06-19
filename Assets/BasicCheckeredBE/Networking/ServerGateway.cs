using BasicCheckeredBE.Networking.DTOs;
using Cysharp.Threading.Tasks;

namespace BasicCheckeredBE.Networking
{
    public class ServerGateway : IGateway
    {
        public static ServerGateway Instance { get; } = new ServerGateway();
        //private readonly IJsonFacade _jsonFacade = new JsonFacade();
        private GameRules _gameRules = new GameRules();
        
        public async UniTask Initialize()
        {
            await _gameRules.Initialize();
        }
        
        public async UniTask<BoardDTO> GetNewBoard()
        {
            return await _gameRules.GetNewBoard();
        }
        
        public async UniTask<AttemptToMoveDTO> AttemptToMove(PieceDTO piece, SquareDTO originalSquare, SquareDTO targetSquare)
        {
            return await _gameRules.AttemptToMove(piece, originalSquare, targetSquare);
        }
    }

    /*
    public interface IJsonFacade
    {
    }

    public class JsonFacade : IJsonFacade
    {
    }
*/
}