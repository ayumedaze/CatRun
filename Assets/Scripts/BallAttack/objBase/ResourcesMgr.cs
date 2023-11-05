using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ResourcesMgr : SingleBase<ResourcesMgr>
{
    public T Load<T>(string name) where T : Object
    {
        T obj = Resources.Load<T>(name);
        if (obj is GameObject)
        {
            return GameObject.Instantiate(obj);
        }
        else
        {
            return obj;
        }
    }

   public void LoadAsync<T>(string name,UnityAction<T> action) where T : Object
   {
        MonoMgr.Instance().StartCoroutine(ILoadAsync(name,action));
   }
    private IEnumerator ILoadAsync<T>(string name, UnityAction<T> action) where T : Object
    {
        ResourceRequest r = Resources.LoadAsync<T>(name);
        yield return r;
        if (r.asset is GameObject)
        {
            action(GameObject.Instantiate(r.asset) as T);
        }
        else 
        { 
            action(r.asset as T);
        }
    }
}
