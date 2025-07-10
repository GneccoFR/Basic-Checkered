using System.Collections.Generic;
using System.Threading.Tasks;
using BasicCheckeredBE.Controllers;
using BasicCheckeredBE.Core.Domain;
using BasicCheckeredBE.Networking.DTOs;
using BasicCheckeredBE.Repositories;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace BasicCheckeredBE.Networking
{
    public class GameRules
    {
        private GameState _gameState;
        private GameBoardController _gameBoardController;
        
        
        public void Initialize(GameState gameState)
        {
            Debug.Log("GameRules Initializing");
            _gameState = gameState;
            _gameState.ShowGameData();
            _gameBoardController = new GameBoardController(_gameState);
            _gameBoardController.Initialize();
        }
        
        public void CreateNewBoard()
        {
            var gameBoard = _gameBoardController.GetNewBoard();
            _gameState.LoadBoard(gameBoard);
        }
        
        public async UniTask<AttemptToMove> AttemptToMove(BoardSquare originalSquare, BoardSquare targetSquare)
        {
            var attempt = _gameBoardController.AttemptToMoveChecker(originalSquare, targetSquare);
            _gameState.UpdateBoardSquares(attempt.UpdatedBoardSquares);
            if (attempt.Success)
                _gameState.UpdateTurn();
            return attempt;
        }

        public bool CheckForGameOver()
        {
            return IsGameEnded();
        }
        
        private bool IsGameEnded()
        {
            bool currentPlayerHasPieces = HasAnyPieces(_gameState.GetCurrentBoard(), _gameState.CurrentPlayer.PlayerId.ToString());
            bool opponentHasPieces = HasAnyPieces(_gameState.GetCurrentBoard(), _gameState.OpponentPlayer.PlayerId.ToString());

            return !opponentHasPieces; // Or "!currentPlayerHasPieces" for draws or variants
        }

        private bool HasAnyPieces(BoardSquare[,] board, string playerId)
        {
            foreach (var square in board)
            {
                if (square.Piece.Owner.PlayerId.ToString() == playerId && square.Piece.PieceType != GlobalFields.PieceType.None)
                    return true;
            }
            return false;
        }
    }
}
