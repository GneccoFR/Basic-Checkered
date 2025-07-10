using System.Collections.Generic;
using BasicCheckeredBE.Core.Domain;

namespace BasicCheckeredBE.Networking.DTOs
{
    public struct AttemptToMoveDTO
    {
        public bool Success;
        public bool IsGameOver;
        public string Message;
        public List<SquareDTO> UpdatedBoardSquares;
        public Player CurrentPlayer;
        public Player OpponentPlayer;
        
        
        public AttemptToMoveDTO(bool success,bool isGameOver, string message, List<SquareDTO> updatedBoardSquares, Player currentPlayer, Player opponentPlayer)
        {
            Success = success;
            IsGameOver = isGameOver;
            Message = message;
            UpdatedBoardSquares = updatedBoardSquares;
            CurrentPlayer = currentPlayer;
            OpponentPlayer = opponentPlayer;
        }
    }
}