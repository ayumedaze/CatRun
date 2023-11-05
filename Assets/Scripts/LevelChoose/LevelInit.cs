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
        //获取通关的关卡
        int levelId = levelFinished.level;
        //向所有子物体上的LevelItem脚本中的Init方法传值
        for (int i = 0; i < transform.childCount; i++)
        {
            if (i > levelId)
            {
                //未开启的关卡
                transform.GetChild(i).GetComponent<LevelBottom>().Init(i + 1, true);
            }
            else
            {
                //开启的关卡
                transform.GetChild(i).GetComponent<LevelBottom>().Init(i + 1, false);
            }
        }
    }
}
