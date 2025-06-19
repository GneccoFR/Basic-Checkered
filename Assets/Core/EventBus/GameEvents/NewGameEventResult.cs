using Core.Shared;

namespace Core.EventBus.GameEvents
{
    public class NewGameEventResult : IEvent
    {
        public GameBoard NewBoard;
        
        public NewGameEventResult(GameBoard newBoard)
        {
            NewBoard = newBoard;
        }
    }
}