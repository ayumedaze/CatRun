using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager>
{
    public Animator animator;
    public float transitionTime = 1f;

    void Update()
    {

    }

    public void ResetLevel()
    {
        //isFirstLoaded = false;
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex));
    }

    public void ReturnLevelChoose()
    {
        StartCoroutine(LoadLevelChoose());
    }
    IEnumerator LoadLevel(int levelIndex)
    {
        animator.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }
    IEnumerator LoadLevelChoose()
    {
        animator.SetTrigger("Start");
        PoolMgr.Instance().clear();
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene("LevelChoose");
    }

    public void ReturnMainMenu()
    {
        StartCoroutine(LoadMainMenu());
    }
    IEnumerator LoadMainMenu()
    {
        Debug.Log("1");
        animator.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene("MainMenu");
    }
}
