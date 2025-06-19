namespace BasicCheckeredBE.Networking.DTOs
{
    public struct SquareDTO
    {
        public PieceDTO Piece;
        public int X;
        public int Y;


        public SquareDTO(PieceDTO piece, int x, int y)
        {
            Piece = piece;
            X = x;
            Y = y;
        }
    }
}