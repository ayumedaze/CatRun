using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Datamanager : SingleMonoBase<Datamanager>
{
    public List<BallInfo> ballInfos = new List<BallInfo>();
    public List<FirePointInfo> firePointInfos = new List<FirePointInfo>();
    void Start()
    {
    }
    /// <summary>
    /// 读取传入的地址的数据
    /// </summary>
    /// <param name="ballinfo"></param>
    /// <param name="firePointInfo"></param>
    public void Init(string ballinfo,string firePointInfo)
    {
        ballInfos = JsonMgr.Instance.LoadData<List<BallInfo>>(ballinfo);
        firePointInfos = JsonMgr.Instance.LoadData<List<FirePointInfo>>(firePointInfo);
    }
    void Update()
    {
        
    }
}
