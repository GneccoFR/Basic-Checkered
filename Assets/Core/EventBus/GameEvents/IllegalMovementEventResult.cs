namespace Core.EventBus.GameEvents
{
    public class IllegalMovementEventResult : IEvent
    {
        public string ErrorMessage;

        public IllegalMovementEventResult(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
    }
}