using Core.Modules.GameMenu.Scripts.Presenters;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Modules.GameMenu.Scripts.Views
{
    public class GameMenuView : MonoBehaviour, IView
    {
        [SerializeField] private Button startButton;

        private GameMenuPresenter _presenter;

        private void Awake()
        {
            Initialize();
        }

        private void OnDestroy()
        {
            Destroy();
        }

        public void Initialize()
        {
            _presenter = new GameMenuPresenter(this);
            _presenter.Initialize();
            startButton.onClick.AddListener(OnStartClicked);
        }

        private void OnStartClicked()
        {
            Debug.Log("Button Clicked!");
            _presenter.StartClicked();
        }

        public void Destroy()
        {
            _presenter.Dispose();
        }
    }
}
