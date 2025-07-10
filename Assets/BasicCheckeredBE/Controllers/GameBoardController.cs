using System;
using BasicCheckeredBE.Core.Domain;
using BasicCheckeredBE.Networking;
using BasicCheckeredBE.Repositories;
using BasicCheckeredBE.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

namespace BasicCheckeredBE.Controllers
{
    public class GameBoardController : IController
    {
        private GameState _gameState;
        private GetNewBoardService _getNewBoardService;
        private AttemptToMoveCheckerService _attemptToMoveCheckerService;
        
        
        public GameBoardController(GameState gameState)
        {
            _gameState = gameState;
        }
        
        public void Initialize()
        {
            _getNewBoardService = new GetNewBoardService();
            _attemptToMoveCheckerService = new AttemptToMoveCheckerService();
        }
        
        public BoardSquare[,] GetNewBoard(int boardSize = 8)
        {
            BoardSquare[,] board = new BoardSquare[boardSize, boardSize];
            
            for (int y = 0; y < 2; y++)
            {
                for (int x = 0; x < boardSize; x++)
                {
                    Debug.Log($"GATEWAY: Setting Board[{x}, {y}] to Player1 {_gameState.CurrentPlayer.PlayerId}");
                    board[x, y] = new BoardSquare(new Piece(GlobalFields.PieceType.Pawn, _gameState.CurrentPlayer), new Vector2(x,y));
                }
            }

            for (int y = 2; y < 6; y++)
            {
                for (int x = 0; x < boardSize; x++)
                {
                    Debug.Log($"GATEWAY: Setting Board[{x}, {y}] to None");
                    board[x, y] = new BoardSquare(new Piece(GlobalFields.PieceType.None, new Player(GlobalFields.PlayerType.None, Guid.Empty)), new Vector2(x,y));
                }
            }
            
            for (int y = 6; y < boardSize; y++)
            {
                for (int x = 0; x < boardSize; x++)
                {
                    Debug.Log($"GATEWAY: Setting Board[{x}, {y}] to Player2 {_gameState.CurrentPlayer.PlayerId}");
                    board[x, y] = new BoardSquare(new Piece(GlobalFields.PieceType.Pawn, _gameState.OpponentPlayer), new Vector2(x,y));
                }
            }

            return board;
        }

        public AttemptToMove AttemptToMoveChecker(BoardSquare originalSquare, BoardSquare targetSquare)
        {
            Debug.Log($"GATEWAY: Attempting move: {originalSquare.Piece.PieceType} from [{originalSquare.Coordinates.X}, {originalSquare.Coordinates.Y}, ({originalSquare.Piece.PieceType})] to [{targetSquare.Coordinates.X}, {targetSquare.Coordinates.Y}, ({targetSquare.Piece.PieceType})]");
            
            return _attemptToMoveCheckerService.AttemptToMoveChecker(_gameState.GetCurrentBoard(), originalSquare, targetSquare);
        }
    }


    public class GetNewBoardService
    {

    }

    public interface IController
    {
        void Initialize();
    }
    
}