using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePointInfo
{
    /// <summary>
    /// 发射点id
    /// </summary>
    public int id;
    /// <summary>
    /// 发射点发射类型 1=>顺序 2=>散弹
    /// </summary>
    public int type;
    /// <summary>
    /// 这组球有多少个
    /// </summary>
    public int num;
    /// <summary>
    /// 每颗球的发射间隔
    /// </summary>
    public float cd;
    /// <summary>
    /// 球的id 1,10 就代表在1-10id球数据中任选一个
    /// </summary>
    public string ids;
    /// <summary>
    /// 到下一组的间隔时间
    /// </summary>
    public float delay;
}
