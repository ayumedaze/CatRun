using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Menu : MonoBehaviour
{
    public GameObject PausePanel;

    public void ChooseLevel()
    {
        LevelManager.Instance.ReturnLevelChoose();
    }

    public void BackMainMenu()
    {
        LevelManager.Instance.ReturnMainMenu();
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void ClickEscWindow()
    {
        if(JumpWindow.Instance.Getifopen()==false)
        {
            JumpWindow.Instance.panelNow = PausePanel;
            JumpWindow.Instance.OpenPanel(PausePanel);
        }
    }

    public void CloseEscWindow()
    {
        if (JumpWindow.Instance.Getifopen() == true)
        {
            JumpWindow.Instance.panelNow = PausePanel;
            JumpWindow.Instance.ClosePanel(PausePanel);
        }
    }


}
