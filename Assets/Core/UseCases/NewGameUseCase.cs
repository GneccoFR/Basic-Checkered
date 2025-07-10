using Core.EventBus;
using Core.EventBus.GameEvents;
using Core.Networking;
using Core.Service_Locator;
using Core.Shared;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Core.UseCases
{
    public class NewGameUseCase : IUseCase
    {
        //Here we cache the Services used by this use case
        private readonly BasicCheckeredBE.Networking.IGateway _gateway;
        private readonly IEventBus _eventBus;
        
        public NewGameUseCase()
        {
            //Here we call the service locator to get the services we need and store them
            _gateway = ServiceLocator.Instance.GetService<BasicCheckeredBE.Networking.IGateway>();
            _eventBus = ServiceLocator.Instance.GetService<IEventBus>();
        }
        
        public async UniTask Execute()
        {
            Debug.Log("NewGameUc Execute called!");
            
            var newGame = await _gateway.GetNewGame() ;
            
            Debug.Log(newGame);
            
            GameBoard board =  Networking.LinqMapper.ToModel(newGame.Board);
            
            Debug.Log(board);
            
            _eventBus.Publish(new NewGameEventResult(board, newGame.MatchId, newGame.CurrentPlayer.MapToPlayerType(), newGame.OpponentPlayer.MapToPlayerType(), newGame.IsGameOver, newGame.WinnerPlayerId));
        }
    }
}