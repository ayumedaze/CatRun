using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FirePoint : MonoBehaviour
{
    /// <summary>
    /// 该发射点的id
    /// </summary>
    public int id;
    /// <summary>
    /// 该发射点位于8个角的哪个角
    /// </summary>
    public E_FirePointPosition position;
    /// <summary>
    /// 发射点对应的屏幕坐标
    /// </summary>
    private Vector3 screenPos;
    /// <summary>
    /// 全部的发射点的信息
    /// </summary>
    private List<FirePointInfo> firePointInfos = new List<FirePointInfo>();
    /// <summary>
    /// 抽出来使用的发射点信息
    /// </summary>
    private FirePointInfo firePointInfo;
    /// <summary>
    /// 抽出来的要发射的球的信息
    /// </summary>
    private BallInfo nowballInfo;
    /// <summary>
    /// 发射散弹时，每个球之间的夹角
    /// </summary>
    private float changeAngle;
    /// <summary>
    /// 发射散弹的初始方向
    /// </summary>
    private Vector2 startDir;
    /// <summary>
    /// 发射散弹的上一次的方向 用于计算
    /// </summary>
    private Vector2 nowDir;
    /// <summary>
    /// 塔位置数组
    /// </summary>
    private Transform[] Towers;
    /// <summary>
    /// 获得第一个球的信息的位置
    /// </summary>
    private int Startid;
    /// <summary>
    /// 获得最后一个球的信息的位置
    /// </summary>
    private int Endid;
    /// <summary>
    /// 每个基地有几个球去
    /// </summary>
    private int count;
    /// <summary>
    /// 记录到达每个基地的球是否满了
    /// </summary>
    int a = 0;
    /// <summary>
    /// 抽出来的那组中有几个球
    /// </summary>
    [HideInInspector]
    public int nowNum;
    /// <summary>
    /// 抽出来的那组中发射球的间隔
    /// </summary>
    [HideInInspector]
    public float nowCD;
    /// <summary>
    /// 到下一组的间隔时间
    /// </summary>
    [HideInInspector]
    public float nowDelay;
    void Start()
    {
        Towers = GameManager.Instance().Towers;
    }
    void Update()
    {
        SetPosition();
        SetFireInfo();
        Fire();
    }
    /// <summary>
    /// 将发射点放在8个角上，一直计算位置，保证分辨率适应
    /// </summary>
    private void SetPosition()
    {
        screenPos.z = 10;
        switch (position)
        {
            case E_FirePointPosition.Left_top:
                screenPos.x = 0;
                screenPos.y = Screen.height;
                //左上时 散弹初始方向为右边
                startDir = Vector2.right;
                break;
            case E_FirePointPosition.Top:
                screenPos.x = Screen.width / 2;
                screenPos.y = Screen.height;
                //上时 散弹初始方向为右边
                startDir = Vector2.right;
                break;
            case E_FirePointPosition.Right_top:
                screenPos.x = Screen.width;
                screenPos.y = Screen.height;
                //右上时 散弹初始方向为左边
                startDir = Vector2.left;
                break;
            case E_FirePointPosition.Left:
                screenPos.x = 0;
                screenPos.y = Screen.height / 2;
                //左时 散弹初始方向为右边
                startDir = Vector2.right;
                break;
            case E_FirePointPosition.Right:
                screenPos.x = Screen.width;
                screenPos.y = Screen.height / 2;
                //右时 散弹初始方向为左边
                startDir = Vector2.left;
                break;
            case E_FirePointPosition.Left_bottom:
                screenPos.x = 0;
                screenPos.y = 0;
                //左下时 散弹初始方向为右边
                startDir = Vector2.right;
                break;
            case E_FirePointPosition.Bottom:
                screenPos.x = Screen.width / 2;
                screenPos.y = 0;
                //下时 散弹初始方向为右边
                startDir = Vector2.right;
                break;
            case E_FirePointPosition.Right_bottom:
                screenPos.x = Screen.width;
                screenPos.y = 0;
                //右下时 散弹初始方向为左边
                startDir = Vector2.left;
                break;
        }
        transform.position = Camera.main.ScreenToWorldPoint(screenPos);
    }
    /// <summary>
    /// 获得发射信息
    /// </summary>
    private void SetFireInfo()
    {
        if (!GameManager.Instance().canSetFireInfo)
            return;
        //当间隔和数量都小于等于0时才去获取发射信息
        if (nowCD > 0 || nowNum > 0)
            return;
        //当组间间隔小于等于0时才去获取发射信息
        nowDelay -= Time.deltaTime;
        if (nowDelay > 0)
            return;
        //从全部数据中取出一条
        firePointInfos = Datamanager.Instance().firePointInfos;
        firePointInfo = firePointInfos[Random.Range(0, firePointInfos.Count)];
        GameManager.Instance().ResetDic();
        if (id != firePointInfo.id)
            return;
        //将冷却相关变量全部存下来
        nowCD = firePointInfo.cd;
        nowDelay = firePointInfo.delay;
        nowNum = firePointInfo.num;

        string[] ids = firePointInfo.ids.Split(',');
        Startid = int.Parse(ids[0]);
        Endid = int.Parse(ids[1]);
        //如果是散弹发射的话
        if (firePointInfo.type == 2)
        {
            switch (position)
            {
                case E_FirePointPosition.Left_top:
                case E_FirePointPosition.Right_top:
                case E_FirePointPosition.Right_bottom:
                case E_FirePointPosition.Left_bottom:
                    changeAngle = 90 / (nowNum + 1);
                    break;
                case E_FirePointPosition.Top:
                case E_FirePointPosition.Left:
                case E_FirePointPosition.Right:
                case E_FirePointPosition.Bottom:
                    changeAngle = 180 / (nowNum + 1);
                    break;
            }
        }
    }
    /// <summary>
    /// 发射球
    /// </summary>
    private void Fire()
    {
        if (firePointInfo == null)
            return;
        if (firePointInfo.id != id)
            return;
        if (nowNum <= 0 && nowCD <= 0)
            return;
        nowCD -= Time.deltaTime;
        if (nowCD > 0)
            return;
        count = firePointInfo.num / Towers.Length;
        int r = Random.Range(1, 101);
        if(r <= GameManager.Instance().Rainbow && GameManager.Instance().useRainbow)
        {
            nowballInfo = Datamanager.Instance().ballInfos[4];
        }
        else
        {
            int Ballid = Random.Range(Startid - 1, Endid);
            nowballInfo = Datamanager.Instance().ballInfos[Ballid];
        }
        GameObject BallObject;
        Ball ball;
        r = 0;
        if (GameManager.Instance().BallToTowerDic[Towers[r].name] >= count)
            if (r < Towers.Length - 1)
                r++;
        Transform tower = Towers[r];
        a = GameManager.Instance().BallToTowerDic[Towers[r].name];
        a++;
        GameManager.Instance().changeDicValue(tower.name,a);
        //根据发射方式创建球
        switch (firePointInfo.type)
        {
            case 1:
                BallObject = PoolMgr.Instance().Popobj(nowballInfo.resName);
                if(BallObject.GetComponent<Ball>() == null)
                   ball = BallObject.AddComponent<Ball>();
                else
                    ball = BallObject.GetComponent<Ball>();
                GameManager.Instance().BallsInScene.Add(ball);
                ball.Init(nowballInfo, tower);
                BallObject.transform.position = new Vector2(transform.position.x,transform.position.y);
                BallObject.transform.localEulerAngles = Vector3.zero;
                nowNum--;
                nowCD = nowNum == 0 ? 0 : firePointInfo.cd;
                break;
            case 2:
                //如果cd为0 散弹一起发射
                if (firePointInfo.cd == 0)
                {
                    for (int i = 0; i < nowNum; i++)
                    {
                        BallObject = PoolMgr.Instance().Popobj(nowballInfo.resName);
                        if (BallObject.GetComponent<Ball>() == null)
                            ball = BallObject.AddComponent<Ball>();
                        else
                            ball = BallObject.GetComponent<Ball>();
                        GameManager.Instance().BallsInScene.Add(ball);
                        ball.Init(nowballInfo,tower);
                        BallObject.transform.position = new Vector2(transform.position.x, transform.position.y);
                        BallObject.transform.localEulerAngles = Vector3.zero;
                        //每次都会获得一个新的角度
                        /*nowDir = Quaternion.AngleAxis(changeAngle * i, Vector3.left) * startDir;
                        BallObject.transform.rotation = Quaternion.LookRotation(nowDir);*/
                    }
                    nowNum = 0;
                }
                else
                {
                    BallObject = PoolMgr.Instance().Popobj(nowballInfo.resName);
                    if (BallObject.GetComponent<Ball>() == null)
                        ball = BallObject.AddComponent<Ball>();
                    else
                        ball = BallObject.GetComponent<Ball>();
                    GameManager.Instance().BallsInScene.Add(ball);
                    ball.Init(nowballInfo,tower);
                    BallObject.transform.position = new Vector2(transform.position.x, transform.position.y);
                    BallObject.transform.localEulerAngles = Vector3.zero;
                    //每次都会获得一个新的角度
                    /*nowDir = Quaternion.AngleAxis(changeAngle * (firePointInfo.num - nowNum), Vector3.left) * startDir;
                    BallObject.transform.rotation = Quaternion.LookRotation(nowDir);*/
                    nowNum--;
                    nowCD = nowNum == 0 ? 0 : firePointInfo.cd;
                }
                break;
        }
    }
}
