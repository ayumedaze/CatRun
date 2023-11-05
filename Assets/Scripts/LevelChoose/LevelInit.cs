using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInit : MonoBehaviour
{
    public Level_SO levelFinished;

    void Start()
    {
        LoadLevel();
    }

    private void LoadLevel()
    {
        //��ȡͨ�صĹؿ�
        int levelId = levelFinished.level;
        //�������������ϵ�LevelItem�ű��е�Init������ֵ
        for (int i = 0; i < transform.childCount; i++)
        {
            if (i > levelId)
            {
                //δ�����Ĺؿ�
                transform.GetChild(i).GetComponent<LevelBottom>().Init(i + 1, true);
            }
            else
            {
                //�����Ĺؿ�
                transform.GetChild(i).GetComponent<LevelBottom>().Init(i + 1, false);
            }
        }
    }
}
