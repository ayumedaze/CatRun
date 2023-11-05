using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ball : MonoBehaviour
{
    /// <summary>
    /// ��Ļ�����Ϣ
    /// </summary>
    public BallInfo info;
    /// <summary>
    /// ��ΪĿ�����
    /// </summary>
    private Transform Tower;
    /// <summary>
    /// �������غ��Ѿ��ȴ���ʱ��
    /// </summary>
    private float NowWaitTime = 0;
    /// <summary>
    /// ���Ƿ�����
    /// </summary>
    private bool isdead = false;
    /// <summary>
    /// �ƶ��ٶ�
    /// </summary>
    public float straightSpeed;

    private bool canMove = true;
    void Update()
    {
        if (canMove)
        {
            Move();
        }
        if (isdead)
            Death();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cat"))
        {
            Debug.Log(1);
            //èè�������
            EventCenter.Instance().EventTrigger<int>("AddBall", info.id);
            //�����Լ�
            isdead = true;
        }
        else if (collision.gameObject.tag.Contains("BallFollowCat"))
        {
            Debug.Log("猫身子");
            //��è�����ʱ
            collision.gameObject.transform.parent.transform.GetChild(1).transform.GetChild(0).GetComponent<Cat>().DecreaseHealthPoints(info.atk);
            //�����Լ�
            isdead = true;
        }

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isdead)
            return;
        if (collision.gameObject.CompareTag("Tower"))
        {
            canMove = false;
            NowWaitTime += Time.deltaTime;
            if (NowWaitTime < info.WaitTime)
                return;
            //����Ѫ
            collision.gameObject.GetComponent<Tower>().Hurt(info.atk);
            //�����Լ�
            isdead = true;
        }
    }
    private void FixedUpdate()
    {
    }
    /// <summary>
    /// ��ʼ���������Ϣ
    /// </summary>
    /// <param name="info">�������Ϣ</param>
    public void Init(BallInfo info,Transform tower)
    {
        this.info = info;
        Tower = tower;
        straightSpeed = info.straightSpeed;
        //���û�н�����ײ��������ʱ��֮���Զ�����
        //Invoke("DealyDestroy", info.lifeTime);
    }
    private void DealyDestroy()
    {
        PoolMgr.Instance().Pushobj(info.resName, gameObject);
    }
    public void Death()
    {
        //����������Ч
        GameObject deadEff = PoolMgr.Instance().Popobj(info.deadEffRes);
        deadEff.transform.position = transform.position;
        deadEff.transform.rotation = transform.rotation;
        DeadEff eff = GetComponent<DeadEff>();
        if (eff == null)
        {
            eff = deadEff.AddComponent<DeadEff>();
        }
        eff.path = info.deadEffRes;
        eff.reset();
        //ɾ�������
        GameManager.Instance().BallsInScene.Remove(GetComponent<Ball>());
        PoolMgr.Instance().Pushobj(info.resName,gameObject);
        isdead = false;
    }
    public void Move()
    {
        //transform.Translate(straightSpeed * (Tower.position - transform.position) * Time.deltaTime);
        transform.position = Vector2.MoveTowards(transform.position, Tower.position, straightSpeed * Time.deltaTime * 10);
    }
    private void OnEnable()
    {
        if(!canMove)
            canMove = true;
    }
}
