using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowSkill : MonoBehaviour
{
    private float waitTime = 5.0f;
    // <summary>
    /// ɾ�����������е���
    /// </summary>
    /// <returns></returns>
    public IEnumerator IyellowSkill()
    {
        Transform[] Towers = GameManager.Instance().Towers;
        for (int i = 0; i < Towers.Length; i++)
        {
            Towers[i].gameObject.GetComponent<Tower>().canHurt = false;
        }
        //���������벻��������
        while (waitTime > 0)
        {
            waitTime -= Time.deltaTime;
            yield return new WaitForSeconds(0.02f);
        }
        for (int i = 0; i < Towers.Length; i++)
        {
            Towers[i].gameObject.GetComponent<Tower>().canHurt = true;
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            MonoMgr.Instance().StartCoroutine(IyellowSkill());
        }
    }
}
