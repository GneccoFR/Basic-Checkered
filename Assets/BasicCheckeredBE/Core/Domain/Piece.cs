using System;
using BasicCheckeredBE.Networking;

namespace BasicCheckeredBE.Core.Domain
{
    public class Piece
    {
        public GlobalFields.PieceType PieceType;
        public Player Owner;
        //public GlobalFields.PlayerType Owner;
        /*
        public Piece(GlobalFields.PieceType piece, GlobalFields.PlayerType owner)
        {
            PieceType = piece;
            Owner = owner;
        }
        */

        public Piece(GlobalFields.PieceType pieceType, Player playerOwner)
        {
            PieceType = pieceType;
            Owner = playerOwner;
        }
    }

    public struct Player
    {
        public GlobalFields.PlayerType OwnerType;
        public Guid PlayerId;

        public Player(GlobalFields.PlayerType ownerType, Guid playerId)
        {
            OwnerType = ownerType;
            PlayerId = playerId;
        }
    }
}