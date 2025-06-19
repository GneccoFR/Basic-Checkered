using System.Collections.Generic;

namespace BasicCheckeredBE.Networking.DTOs
{
    public struct AttemptToMoveDTO
    {
        public bool Success;
        public string Message;
        public List<SquareDTO> UpdatedBoardSquares;

        public AttemptToMoveDTO(bool success, string message, List<SquareDTO> updatedBoardSquares)
        {
            Success = success;
            Message = message;
            UpdatedBoardSquares = updatedBoardSquares;
        }
    }
}