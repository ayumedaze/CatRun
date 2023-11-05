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

    //�ú���������Э�� ֻ����Monoobj�ڲ��ĺ��� �м�
    #region Э�����
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
