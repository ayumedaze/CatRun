using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleMonoBase<T> : MonoBehaviour where T : class
{
    private static T instance;
    public static T Instance()
    {
        if (instance == null)
        {
            GameObject obj = new GameObject();
            obj.name = typeof(T).ToString();
            DontDestroyOnLoad(obj);
            instance = obj.GetComponent<T>();
        }
        return instance;
    }
    private void Awake()
    {
        instance = this as T;
    }
}
