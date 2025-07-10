using System;
using Core.EventBus.GameEvents;
using Core.Service_Locator;
using Core.Shared;

namespace Core.EventBus
{
    public class LocalGameStateService
    {
        public Guid MatchId { get; private set; }
        public Player CurrentPlayer { get; private set; }
        public Player OpponentPlayer { get; private set; }
        public bool IsGameOver { get; private set; }
        public string WinnerPlayerId { get; private set; }

        private IEventBus _eventBus;
        
        public void Initialize()
        {
            _eventBus = ServiceLocator.Instance.GetService<IEventBus>();
            _eventBus.Subscribe<NewGameEventResult>(NewGameEvent);
            _eventBus.Subscribe<AttemptPieceMovementEventResult>(AttemptPieceMovementEvent);
        }

        private void AttemptPieceMovementEvent(AttemptPieceMovementEventResult obj)
        {
            CurrentPlayer = obj.CurrentPlayer;
            OpponentPlayer = obj.OpponentPlayer;
        }

        private void NewGameEvent(NewGameEventResult obj)
        {
            MatchId = obj.MatchId;
            CurrentPlayer = obj.CurrentPlayer;
            OpponentPlayer = obj.OpponentPlayer;
            IsGameOver = obj.IsGameOver;
            WinnerPlayerId = obj.WinnerPlayerId;
        }
    }
}