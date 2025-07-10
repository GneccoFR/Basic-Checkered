namespace BasicCheckeredBE.Networking.DTOs
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