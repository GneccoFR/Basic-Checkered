using Core.Shared;
using Core.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Modules.GameBoard.Scripts
{
    public class DragGhostManager : MonoBehaviour
    {
        public static DragGhostManager Instance { get; private set; }

        [SerializeField] private RectTransform ghostRectTransform;
        [SerializeField] private Image ghostImage;
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private Color darkerColor;
        
        private void Awake()
        {
            Instance = this;
            ghostImage.gameObject.SetActive(false);
        }

        public void ShowGhost(Vector2 startPosition, PlayerType playerType)
        {
            if (playerType == PlayerType.Player2)
            {
                ghostImage.color = darkerColor;
            }
            else
            {
                ghostImage.color = Color.white;
            }
            
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                ghostRectTransform.parent as RectTransform,
                startPosition,
                UICanvasRegistry.MainCanvas.worldCamera,
                out Vector2 localPos
            );

            ghostRectTransform.anchoredPosition = localPos;
            ghostImage.gameObject.SetActive(true);
        }

        public void MoveGhost(Vector2 pointerPosition)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                ghostRectTransform.parent as RectTransform,
                pointerPosition,
                UICanvasRegistry.MainCanvas.worldCamera,
                out Vector2 localPos
            );

            ghostRectTransform.anchoredPosition = localPos;
        }

        public void HideGhost()
        {
            ghostRectTransform.gameObject.SetActive(false);
        }
    }
}