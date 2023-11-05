using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FirePoint : MonoBehaviour
{
    /// <summary>
    /// �÷�����id
    /// </summary>
    public int id;
    /// <summary>
    /// �÷����λ��8���ǵ��ĸ���
    /// </summary>
    public E_FirePointPosition position;
    /// <summary>
    /// ������Ӧ����Ļ����
    /// </summary>
    private Vector3 screenPos;
    /// <summary>
    /// ȫ���ķ�������Ϣ
    /// </summary>
    private List<FirePointInfo> firePointInfos = new List<FirePointInfo>();
    /// <summary>
    /// �����ʹ�õķ������Ϣ
    /// </summary>
    private FirePointInfo firePointInfo;
    /// <summary>
    /// �������Ҫ����������Ϣ
    /// </summary>
    private BallInfo nowballInfo;
    /// <summary>
    /// ����ɢ��ʱ��ÿ����֮��ļн�
    /// </summary>
    private float changeAngle;
    /// <summary>
    /// ����ɢ���ĳ�ʼ����
    /// </summary>
    private Vector2 startDir;
    /// <summary>
    /// ����ɢ������һ�εķ��� ���ڼ���
    /// </summary>
    private Vector2 nowDir;
    /// <summary>
    /// ��λ������
    /// </summary>
    private Transform[] Towers;
    /// <summary>
    /// ��õ�һ�������Ϣ��λ��
    /// </summary>
    private int Startid;
    /// <summary>
    /// ������һ�������Ϣ��λ��
    /// </summary>
    private int Endid;
    /// <summary>
    /// ÿ�������м�����ȥ
    /// </summary>
    private int count;
    /// <summary>
    /// ��¼����ÿ�����ص����Ƿ�����
    /// </summary>
    int a = 0;
    /// <summary>
    /// ��������������м�����
    /// </summary>
    [HideInInspector]
    public int nowNum;
    /// <summary>
    /// ������������з�����ļ��
    /// </summary>
    [HideInInspector]
    public float nowCD;
    /// <summary>
    /// ����һ��ļ��ʱ��
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
    /// ����������8�����ϣ�һֱ����λ�ã���֤�ֱ�����Ӧ
    /// </summary>
    private void SetPosition()
    {
        screenPos.z = 10;
        switch (position)
        {
            case E_FirePointPosition.Left_top:
                screenPos.x = 0;
                screenPos.y = Screen.height;
                //����ʱ ɢ����ʼ����Ϊ�ұ�
                startDir = Vector2.right;
                break;
            case E_FirePointPosition.Top:
                screenPos.x = Screen.width / 2;
                screenPos.y = Screen.height;
                //��ʱ ɢ����ʼ����Ϊ�ұ�
                startDir = Vector2.right;
                break;
            case E_FirePointPosition.Right_top:
                screenPos.x = Screen.width;
                screenPos.y = Screen.height;
                //����ʱ ɢ����ʼ����Ϊ���
                startDir = Vector2.left;
                break;
            case E_FirePointPosition.Left:
                screenPos.x = 0;
                screenPos.y = Screen.height / 2;
                //��ʱ ɢ����ʼ����Ϊ�ұ�
                startDir = Vector2.right;
                break;
            case E_FirePointPosition.Right:
                screenPos.x = Screen.width;
                screenPos.y = Screen.height / 2;
                //��ʱ ɢ����ʼ����Ϊ���
                startDir = Vector2.left;
                break;
            case E_FirePointPosition.Left_bottom:
                screenPos.x = 0;
                screenPos.y = 0;
                //����ʱ ɢ����ʼ����Ϊ�ұ�
                startDir = Vector2.right;
                break;
            case E_FirePointPosition.Bottom:
                screenPos.x = Screen.width / 2;
                screenPos.y = 0;
                //��ʱ ɢ����ʼ����Ϊ�ұ�
                startDir = Vector2.right;
                break;
            case E_FirePointPosition.Right_bottom:
                screenPos.x = Screen.width;
                screenPos.y = 0;
                //����ʱ ɢ����ʼ����Ϊ���
                startDir = Vector2.left;
                break;
        }
        transform.position = Camera.main.ScreenToWorldPoint(screenPos);
    }
    /// <summary>
    /// ��÷�����Ϣ
    /// </summary>
    private void SetFireInfo()
    {
        if (!GameManager.Instance().canSetFireInfo)
            return;
        //�������������С�ڵ���0ʱ��ȥ��ȡ������Ϣ
        if (nowCD > 0 || nowNum > 0)
            return;
        //�������С�ڵ���0ʱ��ȥ��ȡ������Ϣ
        nowDelay -= Time.deltaTime;
        if (nowDelay > 0)
            return;
        //��ȫ��������ȡ��һ��
        firePointInfos = Datamanager.Instance().firePointInfos;
        firePointInfo = firePointInfos[Random.Range(0, firePointInfos.Count)];
        GameManager.Instance().ResetDic();
        if (id != firePointInfo.id)
            return;
        //����ȴ��ر���ȫ��������
        nowCD = firePointInfo.cd;
        nowDelay = firePointInfo.delay;
        nowNum = firePointInfo.num;

        string[] ids = firePointInfo.ids.Split(',');
        Startid = int.Parse(ids[0]);
        Endid = int.Parse(ids[1]);
        //�����ɢ������Ļ�
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
    /// ������
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
        //���ݷ��䷽ʽ������
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
                //���cdΪ0 ɢ��һ����
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
                        //ÿ�ζ�����һ���µĽǶ�
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
                    //ÿ�ζ�����һ���µĽǶ�
                    /*nowDir = Quaternion.AngleAxis(changeAngle * (firePointInfo.num - nowNum), Vector3.left) * startDir;
                    BallObject.transform.rotation = Quaternion.LookRotation(nowDir);*/
                    nowNum--;
                    nowCD = nowNum == 0 ? 0 : firePointInfo.cd;
                }
                break;
        }
    }
}
