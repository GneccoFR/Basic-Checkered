using UnityEngine;

namespace Core.Shared
{
    public struct BoardSquare
    {
        public Piece Piece;
        public Vector2 Coordinates;
        
        public BoardSquare(Piece piece, Vector2 coordinates)
        {
            Piece = piece;
            Coordinates = coordinates;
        }
    }

    public struct Piece
    {
        public PieceType PieceType;
        public PlayerType Owner;
        
        public Piece(PieceType piece, PlayerType owner)
        {
            PieceType = piece;
            Owner = owner;
        }
    }
}