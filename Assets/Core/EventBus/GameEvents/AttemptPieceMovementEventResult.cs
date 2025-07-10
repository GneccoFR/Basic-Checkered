using System.Collections.Generic;
using Core.Shared;

namespace Core.EventBus.GameEvents
{
    public class AttemptPieceMovementEventResult : IEvent
    {
        public List<BoardSquare> UpdatedBoardSquares;
        public Player CurrentPlayer;
        public Player OpponentPlayer;
        
        
        public AttemptPieceMovementEventResult(List<BoardSquare> updatedBoardSquares, Player currentPlayer, Player opponentPlayer)
        {
            UpdatedBoardSquares = updatedBoardSquares;
            CurrentPlayer = currentPlayer;
            OpponentPlayer = opponentPlayer;
        }
    }
}