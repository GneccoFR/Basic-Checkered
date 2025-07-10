using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using BasicCheckeredBE.Core.Domain;
using BasicCheckeredBE.Networking.DTOs;
using BasicCheckeredBE.Repositories;

namespace BasicCheckeredBE.Networking
{
    public static class Mapper
    {
        public static PieceDTO ToPieceDTO(this Piece piece)
        {
            return new PieceDTO(piece.PieceType, piece.Owner);
        }
        
        public static SquareDTO ToSquareDTO(this BoardSquare square)
        {
            return new SquareDTO(square.Piece.ToPieceDTO(), (int)square.Coordinates.X, (int)square.Coordinates.Y);
        }

        public static BoardDTO ToBoardDTO(this BoardSquare[,] board)
        {
            int width = board.GetLength(0);
            int height = board.GetLength(1);
            SquareDTO[,] squareDTOs = new SquareDTO[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    squareDTOs[x, y] = board[x, y].ToSquareDTO();
                }
            }

            return new BoardDTO(squareDTOs);
        }
        
        public static List<SquareDTO> ToSquareDTOList(this List<BoardSquare> boardSquares)
        {
            return boardSquares.Select(square => square.ToSquareDTO()).ToList();
        }
        
        public static Piece ToPiece(this PieceDTO pieceDTO)
        {
            return new Piece(pieceDTO.PieceType, pieceDTO.Owner);
        }
        
        public static BoardSquare ToBoardSquare(this SquareDTO squareDTO)
        {
            return new BoardSquare(squareDTO.Piece.ToPiece(), new Vector2(squareDTO.X, squareDTO.Y));
        }
        
        public static BoardSquare[,] ToBoardSquareArray(this SquareDTO[,] squareDTOs)
        {
            int width = squareDTOs.GetLength(0);
            int height = squareDTOs.GetLength(1);
            BoardSquare[,] boardSquares = new BoardSquare[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    boardSquares[x, y] = squareDTOs[x, y].ToBoardSquare();
                }
            }

            return boardSquares;
        }
        
        public static List<BoardSquare> ToBoardSquareList(this List<SquareDTO> squareDTOs)
        {
            return squareDTOs.Select(squareDTO => squareDTO.ToBoardSquare()).ToList();
        }
    }
}