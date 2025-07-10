using System;
using BasicCheckeredBE.Core.Domain;

namespace BasicCheckeredBE.Networking.DTOs
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
}