using System.Numerics;
using BasicCheckeredBE.Repositories;

namespace BasicCheckeredBE.Core.Domain
{
    public class BoardSquare
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