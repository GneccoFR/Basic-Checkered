using BasicCheckeredBE.Networking.DTOs;

namespace BasicCheckeredBE.Networking
{
    public struct BoardDTO
    {
        public SquareDTO[,] Board { get; private set; }

        public BoardDTO(SquareDTO[,] board)
        {
            Board = board;
        }
    }
}