using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class ScoreManager : Singleton<ScoreManager>
{
    private int score;
    public int targetScore;

    public bool isBonus;
    public TMP_Text ScoreOnUI;
    public TMP_Text TargetOnUI;

    public Level_SO levelFinished;
    public AnimationCurve showCurve;
    public float animationSpeed;
    public GameObject panel;

    private bool isFinished;

    private void Start()
    {
        score = 0;
        ScoreOnUI.text = "";
        TargetOnUI.text = "Target:" + targetScore;
    }

    private void Update()
    {
        ScoreOnUI.text = "Score:" + score.ToString();

        if (isFinished && Input.anyKeyDown)
        {
            Time.timeScale = 1;
            LevelManager.Instance.ReturnLevelChoose();
        }
    }

    public void AddScore(int ScoreToAdd)
    {
        if (isBonus)
        {
            ScoreToAdd *= 2;
        }
        score += ScoreToAdd;
        if (score >= targetScore)
        {
            MusicManager.Instance.SoundEffectTrigger("นýนุ");
            StartCoroutine(ShowDeathPanel(panel));
        }
    }
    public void DecreaseScore(int ScoreToDecrease)
    {
        score -= ScoreToDecrease;
    }

    IEnumerator ShowDeathPanel(GameObject gameObject)
    {
        float timer = 0;
        while (timer <= 1)
        {
            gameObject.transform.localScale = Vector3.one * showCurve.Evaluate(timer);
            timer += Time.deltaTime * animationSpeed;
            yield return null;
        }
        Time.timeScale = 0;
        isFinished = true;
        if (levelFinished.level < SceneManager.GetActiveScene().buildIndex - 1)
        {
            levelFinished.level++;
        }
    }
}
