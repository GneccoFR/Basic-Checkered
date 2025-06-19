namespace Core.EventBus
{
    public interface IEventBus
    {
        public void Subscribe<T>(System.Action<T> action) where T : IEvent;
        public void Unsubscribe<T>(System.Action<T> action) where T : IEvent;
        public void Publish<T>(T @event) where T : IEvent;
    }
}