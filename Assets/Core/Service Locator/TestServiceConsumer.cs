using Core.Service_Locator;
using UnityEngine;

public class TestServiceConsumer : MonoBehaviour 
{
    private void Start() 
    {
        TestService service = ServiceLocator.Instance.GetService<TestService>();
        service.DoSomething();
    }
}
