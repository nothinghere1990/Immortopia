using System;
using UnityEngine;

public class NetworkRunnerEvent : MonoBehaviour
{
    public static Action onNetworkRunnerAdded;
    
    private void Start()
    {
        onNetworkRunnerAdded?.Invoke();
        Destroy(this);
    }
}
