namespace Core.Shared
{
    public struct Piece
    {
        public PieceType PieceType;
        public Player Owner;
        
        public Piece(PieceType piece, Player owner)
        {
            PieceType = piece;
            Owner = owner;
        }
    }
}