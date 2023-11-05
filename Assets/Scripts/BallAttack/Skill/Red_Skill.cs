using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Red_Skill : MonoBehaviour
{
    /// <summary>
    /// ɾ�����������е���
    /// </summary>
    /// <returns></returns>
    public IEnumerator IredSkill()
    {
        for (int i = 0; i < GameManager.Instance().BallsInScene.Count; i++)
        {
            Ball ball = GameManager.Instance().BallsInScene[i];
            //ɾ�������
            PoolMgr.Instance().Pushobj(ball.info.resName, ball.gameObject);
            yield return ball;
        }
        GameManager.Instance().BallsInScene.Clear();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            MonoMgr.Instance().StartCoroutine(IredSkill());
        }
    }
}
