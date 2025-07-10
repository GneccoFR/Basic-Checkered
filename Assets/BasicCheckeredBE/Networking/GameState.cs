using System;
using System.Collections.Generic;
using BasicCheckeredBE.Core.Domain;
using BasicCheckeredBE.Repositories;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace BasicCheckeredBE.Networking
{
    public class GameState
    {
        public Guid MatchId { get; private set; }
        public Player CurrentPlayer { get; private set; }
        public Player OpponentPlayer { get; private set; }
        public bool IsGameOver { get; private set; }
        public string WinnerPlayerId { get; private set; }
        
        
        private readonly IJsonFacade _jsonFacade;
        private readonly MemoryRepositoryManager _repositoryManager;

        
        public GameState(Player player1Id, Player player2Id, IJsonFacade jsonFacade, MemoryRepositoryManager repositoryManager)
        {
            MatchId = Guid.NewGuid();
            CurrentPlayer = player1Id;
            OpponentPlayer = player2Id;

            _jsonFacade = jsonFacade;
            _repositoryManager = repositoryManager;
            
            IsGameOver = false;
            WinnerPlayerId = null;
        }
        
        public GameState(Guid matchId, Player player1Id, Player player2Id, IJsonFacade jsonFacade, MemoryRepositoryManager repositoryManager)
        {
            MatchId = matchId;
            CurrentPlayer = player1Id;
            OpponentPlayer = player2Id;

            _jsonFacade = jsonFacade;
            _repositoryManager = repositoryManager;
            
            IsGameOver = false;
            WinnerPlayerId = null;
        }
        
        public BoardSquare[,] GetCurrentBoard()
        {
            return _repositoryManager.GameBoardRepository.GetCurrentBoard();
        }

        public void SwitchTurn()
        {
            (CurrentPlayer, OpponentPlayer) = (OpponentPlayer, CurrentPlayer);
        }

        public void LoadBoard(BoardSquare[,] gameBoard)
        {
            _repositoryManager.GameBoardRepository.LoadBoard(gameBoard);
        }

        public void UpdateBoardSquares(List<BoardSquare> attemptUpdatedBoardSquares)
        {
            _repositoryManager.GameBoardRepository.UpdateBoardSquares(attemptUpdatedBoardSquares);
        }

        public void ShowGameData()
        {
            Debug.Log("GameState Data:");
            Debug.Log($"MatchId: {MatchId}");
            Debug.Log($"CurrentPlayer: {CurrentPlayer.OwnerType} ({CurrentPlayer.PlayerId})");
            Debug.Log($"OpponentPlayer: {OpponentPlayer.OwnerType} ({OpponentPlayer.PlayerId})");
            Debug.Log($"IsGameOver: {IsGameOver}");
            Debug.Log($"WinnerPlayerId: {WinnerPlayerId}");
        }

        public void UpdateTurn()
        {
            (CurrentPlayer, OpponentPlayer) = (OpponentPlayer, CurrentPlayer);
        }
    }
}