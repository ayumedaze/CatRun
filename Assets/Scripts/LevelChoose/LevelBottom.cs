using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelBottom : MonoBehaviour
{
    // �ؿ�ID
    private int LevelId;
    // ������ť
    private Button btn;

    void Awake()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(OnClick);
    }

    // Update is called once per frame

    public void Init(int id, bool isLock)
    {
        LevelId = id;
        if (isLock)
        {
            btn.interactable = false;
        }
        else
        {
            btn.interactable = true;
        }
    }

    public void OnClick()
    {
        SceneManager.LoadScene(LevelId+1);
    }

}
