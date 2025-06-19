using System.Collections.Generic;
using Core.Shared;

namespace Core.EventBus.GameEvents
{
    public class AttemptPieceMovementEventResult : IEvent
    {
        public  List<BoardSquare> UpdatedBoardSquares;

        public AttemptPieceMovementEventResult(List<BoardSquare> updatedBoardSquares)
        {
            UpdatedBoardSquares = updatedBoardSquares;
        }
    }
}