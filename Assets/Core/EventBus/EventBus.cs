using System;
using System.Collections.Generic;
using Core.Service_Locator;
using UnityEngine;

namespace Core.EventBus
{
    public class EventBus : IService, IEventBus
    {
        private static Dictionary<Type, List<object>> _subscribers = new ();
    
        public void Subscribe<T>(Action<T> callback) where T : IEvent
        {
            Type eventType = typeof(T);
            if (!_subscribers.ContainsKey(eventType))
            {
                _subscribers[eventType] = new List<object>();
            }

            _subscribers[eventType].Add(callback);
        }

        public void Unsubscribe<T>(Action<T> callback) where T : IEvent
        {
            Type eventType = typeof(T);
            if (_subscribers.ContainsKey(eventType))
            {
                _subscribers[eventType].Remove(callback);
            }
        }

        public void Publish<T>(T @event) where T : IEvent
        {
            Type eventType = typeof(T);
            if (_subscribers.ContainsKey(eventType))
            {
                foreach (var subscription in _subscribers[eventType])
                {
                    if (subscription is Action<T> typedCallback)
                    {
                        typedCallback(@event);
                    }
                    else if (subscription is Action<object> untypedCallback)
                    {
                        untypedCallback(@event);
                    }
                    else
                    {
                        // Handle other subscription types if needed
                    }
                }
            }
        }

        public void Register()
        {
            ServiceLocator.Instance.RegisterService(this);
        }
    }
}
