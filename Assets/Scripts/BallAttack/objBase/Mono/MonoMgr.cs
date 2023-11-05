using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Events;

public class MonoMgr : SingleBase<MonoMgr>
{
    private Monoobj _obj;
    public MonoMgr() 
    {
        GameObject monoObj = new GameObject("monoObj");
        _obj = monoObj.AddComponent<Monoobj>();
    }
    public void UpdateAddListener(UnityAction action)
    {
        _obj.UpdateAddListener(action);
    }
    public void UpdateRemoveListener(UnityAction action)
    {
        _obj.UpdateRemoveListener(action);
    }

    //用函数名开启协程 只能用Monoobj内部的函数 切记
    #region 协程相关
    public Coroutine StartCoroutine(string methodName)
    {
        return _obj.StartCoroutine(methodName);
    }
    public Coroutine StartCoroutine(string methodName, [DefaultValue("null")] object value)
    {
        return _obj.StartCoroutine(methodName, value);
    }
    public Coroutine StartCoroutine(IEnumerator routine)
    {
        return _obj.StartCoroutine(routine);
    }
    public Coroutine StartCoroutine_Auto(IEnumerator routine)
    {
        return _obj.StartCoroutine(routine);
    }
    public void StopCoroutine(IEnumerator routine)
    {
        _obj.StopCoroutine(routine);
    }
    public void StopCoroutine(Coroutine routine)
    {
        _obj.StopCoroutine(routine);
    }
    public void StopCoroutine(string methodName)
    {
        _obj.StopCoroutine(methodName);
    }
    #endregion

}
