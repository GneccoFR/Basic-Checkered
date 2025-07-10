using BasicCheckeredBE.Core.Domain;

namespace BasicCheckeredBE.Networking.DTOs
{
    public struct PieceDTO
    {
        public GlobalFields.PieceType PieceType;
        public Player Owner;

        public PieceDTO(GlobalFields.PieceType pieceType, Player owner)
        {
            PieceType = pieceType;
            Owner = owner;
        }
    }
}