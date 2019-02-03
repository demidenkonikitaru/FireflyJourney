using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{

    private static T _instance;
    public static T Instance
    {
        get
        {
            return _instance;
        }
    }

    public bool DestroyOnLoad = true;

    #region Unity methods

    protected virtual void Awake()
    {
        if (!DestroyOnLoad)
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    protected virtual void OnEnable()
    {
        _instance = GetComponent<T>();
    }

    protected virtual void OnDisable()
    {

    }

    protected virtual void OnDestroy()
    {
        _instance = null;
    }

    #endregion
}
