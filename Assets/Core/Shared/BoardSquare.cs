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
}