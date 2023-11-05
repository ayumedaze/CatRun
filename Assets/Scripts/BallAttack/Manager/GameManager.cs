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
    /// ����Ƿ��м�����
    /// </summary>
    public bool useRainbow;
    /// <summary>
    /// ��������ֵĸ���
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
    /// ��ʼ���ֵ�
    /// </summary>
    public void InitDic()
    {
        for (int i = 0; i < Towers.Length; i++)
        {
            BallToTowerDic.Add(Towers[i].name, 0);
        }
    }
    /// <summary>
    /// ���ò���ʼ���ֵ�
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
