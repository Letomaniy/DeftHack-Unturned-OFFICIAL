using System;
using System.Collections.Generic;
using UnityEngine;



[Component]
public class MainThreadDispatcherComponent : MonoBehaviour
{
    public void Update()
    {
        Queue<Action> methodQueue = MainThreadDispatcherComponent.MethodQueue;
        lock (methodQueue)
        {
            while (MainThreadDispatcherComponent.MethodQueue.Count > 0)
            {
                MainThreadDispatcherComponent.MethodQueue.Dequeue()();
            }
        }
    }

    public static void InvokeOnMain(Action a)
    {
        Queue<Action> methodQueue = MainThreadDispatcherComponent.MethodQueue;
        lock (methodQueue)
        {
            MainThreadDispatcherComponent.MethodQueue.Enqueue(a);
        }
    }

    public static readonly Queue<Action> MethodQueue = new Queue<Action>();
}

