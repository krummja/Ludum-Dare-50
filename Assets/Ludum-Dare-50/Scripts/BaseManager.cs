using System;
using UnityEngine;
using Sirenix.OdinInspector;


public abstract class BaseManager<T> : SerializedMonoBehaviour
    where T : Component
{
    public static T Instance { get; private set; }

    public virtual void Awake()
    {
        if ( Instance == null )
        {
            Instance = this as T;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }

        OnAwake();
    }

    protected abstract void OnAwake();
}
