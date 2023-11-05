using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Pooldata
{
    public List<GameObject> PoolList;
    public GameObject fatherObj;
    /// <summary>
    /// 延时多少时间放入pool
    /// </summary>
    public float waitTime;
    public Pooldata(GameObject obj,GameObject poolObj) 
    {
        fatherObj = new GameObject(obj.name);
        fatherObj.transform.parent = poolObj.transform;
        PoolList = new List<GameObject>();
        Pushobj(obj);
    }
    public void Pushobj(GameObject obj)
    {
        obj.SetActive(false);
        PoolList.Add(obj);
        obj.transform.parent = fatherObj.transform;
    }
    public GameObject Popobj()
    {
        GameObject obj = null;
        if (PoolList.Count > 0)
        {
            obj = PoolList[0];
            PoolList.RemoveAt(0);
            obj.SetActive(true);
            obj.transform.parent = null;
        }
        return obj;
    }
}
public class PoolMgr : SingleBase<PoolMgr>
{
    public Dictionary<string, Pooldata> PoolDic = new Dictionary<string, Pooldata>();
    public GameObject Pool;

    public GameObject Popobj(string Path)
    {
        GameObject obj = null;
        if (PoolDic.ContainsKey(Path) && PoolDic[Path].PoolList.Count > 0)
        {
            obj = PoolDic[Path].Popobj();
        }
        else
        {
            obj = GameObject.Instantiate(Resources.Load<GameObject>(Path));
        }
        return obj;
    }
    public void Pushobj(string Path,GameObject obj)
    {
        if (Pool == null)
            Pool = new GameObject("Pool");
        if (PoolDic.ContainsKey(Path))
        {
            PoolDic[Path].Pushobj(obj);
        }
        else
        {
            PoolDic.Add(Path,new Pooldata(obj, Pool));
        }
    }
    public void clear()
    {
        PoolDic.Clear();
        Pool = null;
    }
}
