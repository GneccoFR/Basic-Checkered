using System;
using Core.Shared;

namespace Core.Networking.DTOs
{
    public struct NewGameDTO
    {
        public Guid MatchId { get; private set; }
        public Player CurrentPlayer { get; private set; }
        public Player OpponentPlayer { get; private set; }
        public bool IsGameOver { get; private set; }
        public string WinnerPlayerId { get; private set; }
        public BoardDTO Board { get; private set; }
        
        public NewGameDTO(Guid matchId, Player currentPlayer, Player opponentPlayer, bool isGameOver, string winnerPlayerId, BoardDTO board)
        {
            MatchId = matchId;
            CurrentPlayer = currentPlayer;
            OpponentPlayer = opponentPlayer;
            IsGameOver = isGameOver;
            WinnerPlayerId = winnerPlayerId;
            Board = board;
        }
    }
    
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