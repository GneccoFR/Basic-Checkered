using System;
using BasicCheckeredBE.Core.Domain;
using BasicCheckeredBE.Networking.DTOs;
using BasicCheckeredBE.Repositories;
using Cysharp.Threading.Tasks;

namespace BasicCheckeredBE.Networking
{
    public class ServerGateway : IGateway
    {
        public static ServerGateway Instance { get; } = new ServerGateway();
        private readonly IJsonFacade _jsonFacade = new JsonFacade();
        private MemoryRepositoryManager _repositoryManager;
        private GameRules _gameRules;
        private GameState _gameState;
        
        public async UniTask Initialize()
        {
            _repositoryManager = new MemoryRepositoryManager();
            _repositoryManager.Initialize();
        }
        
        public async UniTask<NewGameDTO> GetNewGame()
        {
            _gameState = new GameState(new Player(GlobalFields.PlayerType.Player1, Guid.NewGuid()), new Player(GlobalFields.PlayerType.Player2, Guid.NewGuid()), _jsonFacade, _repositoryManager);
            _gameState.ShowGameData();
            _gameRules = new GameRules();
            _gameRules.Initialize(_gameState);
            _gameRules.CreateNewBoard();
            return new NewGameDTO(_gameState.MatchId, _gameState.CurrentPlayer, _gameState.OpponentPlayer, _gameState.IsGameOver, _gameState.WinnerPlayerId, Mapper.ToBoardDTO(_gameState.GetCurrentBoard()));
        }
        
        public async UniTask<AttemptToMoveDTO> AttemptToMove(SquareDTO originalSquare, SquareDTO targetSquare)
        {
            var attempt = await _gameRules.AttemptToMove(
                originalSquare.ToBoardSquare(),
                targetSquare.ToBoardSquare()
            );

            var isGameEnded = false;
            
            if (attempt.Success)
                isGameEnded = _gameRules.CheckForGameOver();

            return new AttemptToMoveDTO(attempt.Success, isGameEnded, attempt.Message, attempt.UpdatedBoardSquares.ToSquareDTOList(), _gameState.CurrentPlayer, _gameState.OpponentPlayer);
        }
    }


    public interface IJsonFacade
    {
    }

    public class JsonFacade : IJsonFacade
    {
    }

}