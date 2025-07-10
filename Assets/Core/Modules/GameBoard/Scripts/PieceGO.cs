using System;
using Core.EventBus;
using Core.Service_Locator;
using Core.Shared;
using Core.UseCases;
using Core.Utils;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Core.Modules.GameBoard.Scripts
{
    public class PieceGO : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private Image pieceImage;
        [SerializeField] private Color darkerColor;
        
        private Piece _piece;
        private Canvas _mainCanvas;
        private Vector2 _originalAnchoredPosition;
        private BoardSquare _mySquare;
           
    
        private void Awake()
        {
            _mainCanvas = UICanvasRegistry.MainCanvas;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (ServiceLocator.Instance.GetService<LocalGameStateService>().CurrentPlayer.PlayerId != _piece.Owner.PlayerId)
                return;
            
            _originalAnchoredPosition = rectTransform.anchoredPosition;
            
            DragGhostManager.Instance.ShowGhost(_originalAnchoredPosition, _piece.Owner.OwnerType);
            
            GameBoardManager.CurrentHoveredSquare = null; // Clear any stale state
            pieceImage.raycastTarget = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (ServiceLocator.Instance.GetService<LocalGameStateService>().CurrentPlayer.PlayerId != _piece.Owner.PlayerId)
                return;
            
            DragGhostManager.Instance.MoveGhost(eventData.position);
            
            //rectTransform.anchoredPosition += eventData.delta / _mainCanvas.scaleFactor;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (ServiceLocator.Instance.GetService<LocalGameStateService>().CurrentPlayer.PlayerId != _piece.Owner.PlayerId)
                return;
            
            DragGhostManager.Instance.HideGhost();
            
            var targetSquare = GameBoardManager.CurrentHoveredSquare;
            
            if (targetSquare == null)
                Debug.Log("No valid target square");
            else
            {
                Debug.Log("UNITY: Attempting to move piece: " + _piece.PieceType + 
                          " from [" + _mySquare.Coordinates.x + ", " + _mySquare.Coordinates.y + 
                          "] to ( " + targetSquare.BoardSquare.Piece.PieceType +" )[" + targetSquare.BoardSquare.Coordinates.x + ", " + targetSquare.BoardSquare.Coordinates.y + "]");
                
                var tryMove = new AttemptPieceMovementUseCase(_piece, _mySquare, targetSquare.BoardSquare);
                tryMove.Execute();
            }
            
            pieceImage.raycastTarget = true;
            ResetPosition();
        }

        public void ResetPosition()
        {
            rectTransform.anchoredPosition = Vector2.zero;
        }

        public void SetPiece(Piece piece)
        {
            Debug.Log("Setting Piece: " + piece.PieceType + " for Player: " + piece.Owner);
            _piece = piece;
            SetPlayerOwnership(_piece.Owner.OwnerType);
            SetPieceType(_piece.PieceType);
            Debug.Log("Set Piece: " + _piece.PieceType + " for Player: " + _piece.Owner);
        }

        public void SetSquare(BoardSquare square)
        {
            _mySquare = square;
        }
        
        private void SetPieceType(PieceType pieceType)
        {
            // Optionally set the sprite or other properties based on piece type
            // pieceImage.sprite = GetSpriteForPieceType(pieceType);
        }
        
        private void SetPlayerOwnership(PlayerType playerType)
        {
            switch (playerType)
            {
                case PlayerType.None:
                    gameObject.SetActive(false);
                    break;
                case PlayerType.Player1:
                    gameObject.SetActive(true);
                    pieceImage.color = Color.white; // Default color for Player1
                    break;
                case PlayerType.Player2:
                    gameObject.SetActive(true);
                    pieceImage.color = darkerColor; // Example color for Player2
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(playerType), playerType, null);
            }
        }
    }
}
