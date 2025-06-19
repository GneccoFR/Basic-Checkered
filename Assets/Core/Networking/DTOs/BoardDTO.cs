using Core.Shared;

namespace Core.Networking.DTOs
{
    public struct BoardDTO
    {
        public SquareDTO[,] Board { get; private set; }

        public BoardDTO(SquareDTO[,] board)
        {
            Board = board;
        }
    }

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

    public struct PieceDTO
    {
        public PieceType PieceType;
        public PlayerType Owner;

        public PieceDTO(PieceType pieceType, PlayerType owner)
        {
            PieceType = pieceType;
            Owner = owner;
        }
    }
}