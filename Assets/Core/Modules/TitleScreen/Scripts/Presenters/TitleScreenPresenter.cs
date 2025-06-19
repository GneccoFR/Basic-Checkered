using Core.EventBus.GameEvents;
using Core.Infrastructure;
using Core.Modules.GameMenu.Scripts.Views;
using Core.Modules.TitleScreen.Scripts.Views;

namespace Core.Modules.TitleScreen.Scripts.Presenters
{
    public class TitleScreenPresenter : BasePresenter
    {
        private TitleScreenView _view;
        
        public TitleScreenPresenter(IView view) : base(view)
        {
            _view = view as TitleScreenView;
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
    }
}
