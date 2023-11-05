using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallInfo : MonoBehaviour
{
    /// <summary>
    /// 球的ID
    /// </summary>
    public int id;
    /// <summary>
    /// 球的颜色
    /// </summary>
    public int color;
    /// <summary>
    /// 球的攻击力
    /// </summary>
    public int atk;
    /// <summary>
    /// 二次函数的a
    /// a>0 抛物线开口向上，a<0 抛物线开口向下。|a| 越大开口越小。
    /// </summary>
    public int a;
    /// <summary>
    /// 直线运动速度
    /// </summary>
    public float straightSpeed;
    /// <summary>
    /// 抛物线运动速度
    /// </summary>
    public float parabolaSpeed;
    /// <summary>
    /// 球的图片位置
    /// </summary>
    public string resName;
    /// <summary>
    /// 球的死亡特效
    /// </summary>
    public string deadEffRes;
    /// <summary>
    /// 球的生命周期
    /// </summary>
    public float lifeTime;
    /// <summary>
    /// 球碰到基地后等多长时间爆炸
    /// </summary>
    public float WaitTime;
    public E_BallColor BallColor()
    {
        switch (color)
        {
            case 0:
                return E_BallColor.Red;
            case 1:
                return E_BallColor.Green;
            case 2:
                return E_BallColor.Blue;
            case 3:
                return E_BallColor.Yellow;
            case 4:
            default:
                return E_BallColor.Rainbow;
        }
    }
}
