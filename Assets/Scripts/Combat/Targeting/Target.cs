using System;
using UnityEngine;

public class Target : MonoBehaviour
{
    public event Action<Target> OnDestroyed;

    void OnDestroy()
    {
        OnDestroyed?.Invoke(this);
    }
}