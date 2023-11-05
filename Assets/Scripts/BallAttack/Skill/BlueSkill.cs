using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueSkill : MonoBehaviour
{
    /// <summary>
    /// 出球的等待时间
    /// </summary>
    private float waitTime = 5.0f;
    public IEnumerator IblueSkill()
    {
        GameManager.Instance().canSetFireInfo = false;
        float a = 0;
        for (int i = 0; i < GameManager.Instance().BallsInScene.Count; i++)
        {
            Ball ball = GameManager.Instance().BallsInScene[i];
            //减半球的移动速度
            print(ball.straightSpeed);
            a = ball.info.straightSpeed;
            ball.straightSpeed = a / 2.0f;
            print(ball.straightSpeed);
            yield return ball;
        }
        //持续多少秒不出现新球
        while (waitTime > 0)
        {
            waitTime -= Time.deltaTime;
            yield return new WaitForSeconds(0.02f);
        }
        GameManager.Instance().canSetFireInfo = true;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            MonoMgr.Instance().StartCoroutine(IblueSkill());
        }
    }
}
