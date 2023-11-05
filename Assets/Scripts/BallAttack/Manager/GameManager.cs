using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class GameManager : SingleMonoBase<GameManager>
{
    public Transform[] Towers;
    public bool canSetFireInfo = true;
    [HideInInspector]
    public List<Ball> BallsInScene = new List<Ball>();
    [HideInInspector]
    public Dictionary<string,int> BallToTowerDic = new Dictionary<string,int>();
    
    /// <summary>
    /// 这关是否有技能球
    /// </summary>
    public bool useRainbow;
    /// <summary>
    /// 技能球出现的概率
    /// </summary>
    public int Rainbow;
    public string FirePointerInfo;
    void Start()
    {
        Datamanager.Instance().Init("ballInfos", FirePointerInfo);
    }

    // Update is called once per frame
    void Update()
    {
    }
    /// <summary>
    /// 初始化字典
    /// </summary>
    public void InitDic()
    {
        for (int i = 0; i < Towers.Length; i++)
        {
            BallToTowerDic.Add(Towers[i].name, 0);
        }
    }
    /// <summary>
    /// 重置并初始化字典
    /// </summary>
    public void ResetDic()
    {
        BallToTowerDic.Clear();
        InitDic();
    }
    public void changeDicValue(string key ,int value)
    {
        BallToTowerDic.Remove(key);
        BallToTowerDic.Add(key,value);
    }
}
