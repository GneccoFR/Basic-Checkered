using Core.EventBus;
using Core.Service_Locator;
using Core.Shared;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Core.Modules.GameBoard.Scripts
{
    public class BoardSquareGO : MonoBehaviour, IPointerEnterHandler
    {
        public BoardSquare BoardSquare;
        
        [SerializeField] private PieceGO myPiece;
        [SerializeField] private Image squareBackground;
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (ServiceLocator.Instance.GetService<LocalGameStateService>().CurrentPlayer.PlayerId != BoardSquare.Piece.Owner.PlayerId &&
                BoardSquare.Piece.Owner.OwnerType != PlayerType.None)
                return;
            
            if (eventData.dragging)
            {
                GameBoardManager.CurrentHoveredSquare = this;
                Debug.Log($"Hovering over square [{GameBoardManager.CurrentHoveredSquare.BoardSquare.Coordinates.x}, {GameBoardManager.CurrentHoveredSquare.BoardSquare.Coordinates.y}] ({GameBoardManager.CurrentHoveredSquare.BoardSquare.Piece.PieceType}) while dragging a piece.");
            }
        }

        public void SetSquare(BoardSquare boardSquare)
        {
            BoardSquare = boardSquare;
            SetPlacedPiece(boardSquare.Piece);
            myPiece.SetSquare(BoardSquare);
        }
        
        private void SetPlacedPiece(Piece piece)
        {
            Debug.Log($"Placing Piece: {piece.PieceType} at square [{BoardSquare.Coordinates.x}, {BoardSquare.Coordinates.y}]");
            
            myPiece.SetPiece(piece);
            myPiece.ResetPosition();
        }
    }
}