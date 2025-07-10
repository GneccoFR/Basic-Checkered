using System;
using Core.Shared;

namespace Core.EventBus.GameEvents
{
    public class NewGameEventResult : IEvent
    {
        public GameBoard NewBoard { get; private set; }
        public Guid MatchId { get; private set; }
        public Player CurrentPlayer { get; private set; }
        public Player OpponentPlayer { get; private set; }
        public bool IsGameOver { get; private set; }
        public string WinnerPlayerId { get; private set; }
        
        public NewGameEventResult(GameBoard newBoard, Guid matchId, Player currentPlayer, Player opponentPlayer, bool isGameOver, string winnerPlayerId)
        {
            MatchId = matchId;
            CurrentPlayer = currentPlayer;
            OpponentPlayer = opponentPlayer;
            IsGameOver = isGameOver;
            WinnerPlayerId = winnerPlayerId;
            NewBoard = newBoard;
        }
    }
}