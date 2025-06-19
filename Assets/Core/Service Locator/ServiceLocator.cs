using System;
using System.Collections.Generic;

namespace Core.Service_Locator
{
    public class ServiceLocator 
    {
        public static ServiceLocator Instance => _instance ??= new ServiceLocator();
        private static ServiceLocator _instance;
        private Dictionary<Type, object> _services = new();

        
        private ServiceLocator()
        {
            _services = new Dictionary<Type, object>();
        }
        
        public Type GetService<Type>()
        {
            var serviceType = typeof(Type);
            if (!_services.TryGetValue(serviceType, out var service))
            {
                throw new Exception($"Service of type {serviceType} not found");
            }
            return (Type)service;
        }

        public void RegisterService<Type>(Type service)
        {
            var serviceType = typeof(Type);
            //Assert.IsFalse(_services.ContainsKey(serviceType), $"Service of type {serviceType} already registered");
            if (_services.ContainsKey(serviceType))
                _services.Remove(serviceType);
            _services.Add(serviceType, service);
        }

        public void UnregisterService<Type>()
        {
            var serviceType = typeof(Type);
            //Assert.IsTrue(_services.ContainsKey(serviceType), $"Service of type {serviceType} not registered");
            if (_services.ContainsKey(serviceType))
                _services.Remove(serviceType);
        }
    }
}
