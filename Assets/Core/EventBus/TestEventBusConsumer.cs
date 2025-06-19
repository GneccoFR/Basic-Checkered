using System;
using System.Collections;
using System.Collections.Generic;
using Core.EventBus;
using Core.Service_Locator;
using UnityEngine;

public class TestEventBusConsumer : MonoBehaviour
{
    private EventBus _eventBus;
    void Start()
    {
        _eventBus = ServiceLocator.Instance.GetService<EventBus>();
        SubscribeEvents();
        _eventBus.Publish(new TestEvent());
    }

    private void SubscribeEvents()
    {
        _eventBus.Subscribe<TestEvent>(OnTestEvent);
    }

    private void OnTestEvent(TestEvent obj)
    {
        Debug.Log("Test Event Triggered");
    }

    private void OnDestroy()
    {
        _eventBus.Unsubscribe<TestEvent>(OnTestEvent);
    }
}
