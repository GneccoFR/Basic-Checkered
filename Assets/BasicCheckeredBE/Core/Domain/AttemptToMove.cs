using System.Collections.Generic;

namespace BasicCheckeredBE.Core.Domain
{
    public class AttemptToMove
    {
        public bool Success;
        public string Message;
        public List<BoardSquare> UpdatedBoardSquares;
        //public Player CurrentPlayer;
        //public Player OpponentPlayer;
        
        public AttemptToMove(bool success, string message, List<BoardSquare> updatedBoardSquares/*, Player currentPlayer, Player opponentPlayer*/)
        {
            Success = success;
            Message = message;
            UpdatedBoardSquares = updatedBoardSquares;
            //CurrentPlayer = currentPlayer;
            //OpponentPlayer = opponentPlayer;
        }
    }
}