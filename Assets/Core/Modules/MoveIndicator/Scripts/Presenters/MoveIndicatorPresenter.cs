using Core.EventBus.GameEvents;
using Core.Infrastructure;
using Core.Modules.MoveIndicator.Scripts.Views;
using Core.Shared;
using UnityEngine;

namespace Core.Modules.MoveIndicator.Scripts.Presenters
{
    public class MoveIndicatorPresenter : BasePresenter
    {
        private MoveIndicatorView _view;
    
        public MoveIndicatorPresenter(IView view) : base(view)
        {
            _view = view as MoveIndicatorView;
        }

        public override void Initialize()
        {
            Debug.Log("Initializing MoveIndicatorPresenter");
            SubscribeEvents();
            _view.SetMoveIndicatorText("");
        }

        public override void SubscribeEvents()
        {
            EventBus.Subscribe<AttemptPieceMovementEventResult>(AttemptPieceMovementEvent);
            EventBus.Subscribe<NewGameEventResult>(NewGameEvent);
        }

        public override void UnSubscribeEvents()
        {
            EventBus.Unsubscribe<AttemptPieceMovementEventResult>(AttemptPieceMovementEvent);
            EventBus.Unsubscribe<NewGameEventResult>(NewGameEvent);
        }

        private void NewGameEvent(NewGameEventResult obj)
        {
            _view.SetMoveIndicatorText(obj.CurrentPlayer.OwnerType == PlayerType.Player1
                ? "White Moves..."
                : "Black Moves...");
        }

        private void AttemptPieceMovementEvent(AttemptPieceMovementEventResult obj)
        {
            _view.SetMoveIndicatorText(obj.CurrentPlayer.OwnerType == PlayerType.Player1
                ? "White Moves..."
                : "Black Moves...");
        }

        public override void Dispose()
        {
            UnSubscribeEvents();
        }
    }
}
