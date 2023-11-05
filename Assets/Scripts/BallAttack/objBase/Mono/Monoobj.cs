using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Monoobj : MonoBehaviour
{
    public UnityAction UpdateAction;
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
    private void Update()
    {
        if (UpdateAction != null) UpdateAction();
    }
    public void UpdateAddListener(UnityAction action)
    {
        UpdateAction += action;
    }
    public void UpdateRemoveListener(UnityAction action)
    {
        UpdateAction -= action;
    }
}
