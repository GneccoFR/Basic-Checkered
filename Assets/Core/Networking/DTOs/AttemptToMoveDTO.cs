using System.Collections.Generic;
using Core.Networking.DTOs;
using Core.Shared;

namespace Core.Networking
{
    public struct AttemptToMoveDTO
    {
        public bool Success;
        public List<SquareDTO> UpdatedBoardSquares;

        public AttemptToMoveDTO(bool success, List<SquareDTO> updatedBoardSquares)
        {
            Success = success;
            UpdatedBoardSquares = updatedBoardSquares;
        }
    }
}