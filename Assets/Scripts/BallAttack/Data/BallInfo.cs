using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallInfo : MonoBehaviour
{
    /// <summary>
    /// ���ID
    /// </summary>
    public int id;
    /// <summary>
    /// �����ɫ
    /// </summary>
    public int color;
    /// <summary>
    /// ��Ĺ�����
    /// </summary>
    public int atk;
    /// <summary>
    /// ���κ�����a
    /// a>0 �����߿������ϣ�a<0 �����߿������¡�|a| Խ�󿪿�ԽС��
    /// </summary>
    public int a;
    /// <summary>
    /// ֱ���˶��ٶ�
    /// </summary>
    public float straightSpeed;
    /// <summary>
    /// �������˶��ٶ�
    /// </summary>
    public float parabolaSpeed;
    /// <summary>
    /// ���ͼƬλ��
    /// </summary>
    public string resName;
    /// <summary>
    /// ���������Ч
    /// </summary>
    public string deadEffRes;
    /// <summary>
    /// �����������
    /// </summary>
    public float lifeTime;
    /// <summary>
    /// ���������غ�ȶ೤ʱ�䱬ը
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
