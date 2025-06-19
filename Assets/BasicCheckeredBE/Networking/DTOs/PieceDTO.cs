namespace BasicCheckeredBE.Networking
{
    public struct PieceDTO
    {
        public GlobalFields.PieceType PieceType;
        public GlobalFields.PlayerType Owner;

        public PieceDTO(GlobalFields.PieceType pieceType, GlobalFields.PlayerType owner)
        {
            PieceType = pieceType;
            Owner = owner;
        }
    }
}