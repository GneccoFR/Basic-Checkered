using System;
using Core.Infrastructure;
using Core.Modules.TitleScreen.Scripts.Presenters;
using UnityEngine;

namespace Core.Modules.TitleScreen.Scripts.Views
{
    public class TitleScreenView : MonoBehaviour, IView
    {
        [SerializeField] private RectTransform titleScreenRect;

        private TitleScreenPresenter _presenter;


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
            _presenter = new TitleScreenPresenter(this);
            _presenter.Initialize();
        }

        public void Destroy()
        {
            _presenter.Dispose();
        }
    }
}
