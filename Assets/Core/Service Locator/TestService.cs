using Core.Service_Locator;
using UnityEngine;

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
