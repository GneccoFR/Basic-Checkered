using Core.EventBus.GameEvents;
using Core.Infrastructure;
using Core.Modules.GameMenu.Scripts.Views;
using Core.UseCases;

namespace Core.Modules.GameMenu.Scripts.Presenters
{
    public class GameMenuPresenter : BasePresenter
    {
        private GameMenuView _view;
    
        public GameMenuPresenter(IView view) : base(view)
        {
            _view = view as GameMenuView;
        }

        public override void Initialize()
        {
            SubscribeEvents();
        }

        public override void SubscribeEvents()
        {
            EventBus.Subscribe<NewGameEventResult>(NewGameEvent);
        }

        public override void UnSubscribeEvents()
        {
            EventBus.Unsubscribe<NewGameEventResult>(NewGameEvent);
        }

        private void NewGameEvent(NewGameEventResult obj)
        {
            _view.gameObject.SetActive(false);
        }

        public override void Dispose()
        {
            UnSubscribeEvents();
        }

        public void StartClicked()
        {
            UnityEngine.Debug.Log("Start Clicked!");
            NewGameUseCase newGameUseCase = new NewGameUseCase();
            newGameUseCase.Execute();
        }
    }
}