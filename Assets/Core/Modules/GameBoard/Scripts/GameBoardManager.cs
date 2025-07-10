using Core.EventBus;
using Core.EventBus.GameEvents;
using Core.Service_Locator;
using Core.Shared;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Modules.GameBoard.Scripts
{
    public class GameBoardManager : MonoBehaviour
    {
        public static BoardSquareGO CurrentHoveredSquare;
        
        [SerializeField] private BoardSquareGO boardSquarePrefab;
        [SerializeField] private RectTransform boardContainer;
        [SerializeField] private RectTransform boardBackground;
        [SerializeField] private RectTransform floatingPiece;
        [SerializeField] private Image floatingPieceImage;
        public static RectTransform FloatingPiece;
        public static Image FloatingPieceImage;
        
        private BoardSquareGO[,] _boardGO;
        private IEventBus _eventBus;
        
        
        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            _eventBus = ServiceLocator.Instance.GetService<IEventBus>();
            SubscribeEvents();
            FloatingPiece = floatingPiece;
            FloatingPieceImage = floatingPieceImage;
        }

        private void SubscribeEvents()
        {
            _eventBus.Subscribe<NewGameEventResult>(NewGameEvent);
            _eventBus.Subscribe<AttemptPieceMovementEventResult>(AttemptPieceMovementEvent);
            _eventBus.Subscribe<IllegalMovementEventResult>(IllegalMovementEvent);
        }

        private void UnSubscribeEvents()
        {
            _eventBus.Unsubscribe<NewGameEventResult>(NewGameEvent);
            _eventBus.Unsubscribe<AttemptPieceMovementEventResult>(AttemptPieceMovementEvent);
            _eventBus.Unsubscribe<IllegalMovementEventResult>(IllegalMovementEvent);
        }

        private void IllegalMovementEvent(IllegalMovementEventResult obj)
        {
            Debug.Log(obj.ErrorMessage);
        }


        private void NewGameEvent(NewGameEventResult obj)
        {
            InitializeBoard(obj.NewBoard.Board);
        }

        public void Dispose()
        {
            UnSubscribeEvents();
        }

        private void OnDestroy()
        {
            Dispose();
        }

        public void InitializeBoard(BoardSquare[,] board)
        {
            Debug.Log("UNITY: GameBoardManager InitializeBoard called!");
            Debug.Log($"UNITY: GameBoardManager Initializing Board Sized [{board.GetLength(0)},{board.GetLength(1)}]");
            
            boardBackground.gameObject.SetActive(true);
            
            _boardGO = new BoardSquareGO[board.GetLength(0), board.GetLength(1)];
            
            for (int y = 0; y < board.GetLength(0); y++)
            {
                for (int x = 0; x < board.GetLength(1); x++)
                {
                    _boardGO[x, y] = Instantiate(boardSquarePrefab, boardContainer);
                    _boardGO[x, y].SetSquare(board[x, y]);
                }
            }
        }
        
        private void AttemptPieceMovementEvent(AttemptPieceMovementEventResult obj)
        {
            Debug.Log("UNITY: GameBoardManager AttemptPieceMovementEvent called!");
            Debug.Log($"UNITY: GameBoardManager Attempting to update board with {obj.UpdatedBoardSquares.Count} squares.");

            for (var i = 0; i < obj.UpdatedBoardSquares.Count; i++)
            {
                var square = obj.UpdatedBoardSquares[i];
                Debug.Log($"UNITY: GameBoardManager Updating square at [{square.Coordinates.x}, {square.Coordinates.y}] with piece {square.Piece.PieceType}");
                _boardGO[(int)square.Coordinates.x, (int)square.Coordinates.y].SetSquare(square);
            }
        }
    }
}
