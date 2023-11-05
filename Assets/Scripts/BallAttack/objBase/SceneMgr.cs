using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneMgr : SingleBase<SceneMgr>
{
    public void LoadScene(string name,UnityAction func)
    {
        SceneManager.LoadScene(name);
        func();
    }
    public void LoadSceneAsyn(string name, UnityAction func)
    {
        MonoMgr.Instance().StartCoroutine(ILoadSceneAsyn(name, func));
    }
    private IEnumerator ILoadSceneAsyn(string name, UnityAction func)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(name);
        while(!ao.isDone)
        {
            EventCenter.Instance().EventTrigger("UpLoading", ao.progress);
            yield return ao.progress;
        }
        func();
    }
}
