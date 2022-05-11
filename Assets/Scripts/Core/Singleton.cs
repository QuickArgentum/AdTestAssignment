﻿using System.Collections;
using UnityEngine;

/// <summary>
/// A basic wrapper to create singleton MonoBehaviours
/// </summary>
public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance { get; private set; }

    protected virtual void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this as T;
    }
}