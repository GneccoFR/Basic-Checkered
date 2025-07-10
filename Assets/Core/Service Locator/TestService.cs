using UnityEngine;

namespace Core.Service_Locator
{
    public class TestService : MonoBehaviour, IService  
    {
        public void Register()
        {
            ServiceLocator.Instance.RegisterService(this);
        }
    
        public void DoSomething() 
        {
            Debug.Log("Doing something in TestService");
        }
    }
}
