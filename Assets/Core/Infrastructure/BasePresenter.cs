using System;
using Core.EventBus;
using Core.Service_Locator;
using Core.UseCases;

namespace Core.Infrastructure
{
    public abstract class BasePresenter : IDisposable
    {
    
        protected readonly IView View;
        protected readonly IEventBus EventBus;
        protected IUseCase UseCase;
    
        protected BasePresenter(IView view)
        {
            View = view;
            EventBus = ServiceLocator.Instance.GetService<IEventBus>();
        }
        
        public abstract void Initialize();
        public abstract void SubscribeEvents();
        public abstract void UnSubscribeEvents();
        public abstract void Dispose();
    }
}