using System;
using Core.Infrastructure;
using Core.Modules.MoveIndicator.Scripts.Presenters;
using TMPro;
using UnityEngine;

namespace Core.Modules.MoveIndicator.Scripts.Views
{
    public class MoveIndicatorView : MonoBehaviour, IView
    {
        [SerializeField] private TextMeshProUGUI moveIndicatorText;

        private MoveIndicatorPresenter _presenter;

        private void Start()
        {
            Initialize();
        }

        public void Initialize()
        {
            _presenter = new MoveIndicatorPresenter(this);
            _presenter.Initialize();
        }

        public void SetMoveIndicatorText(string playerToMove)
        {
            moveIndicatorText.text = playerToMove;
        }
    }
}
