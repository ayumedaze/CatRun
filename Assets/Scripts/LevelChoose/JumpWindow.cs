using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class JumpWindow : Singleton<JumpWindow>
{
	public AnimationCurve showCurve;
    public AnimationCurve hideCurve;
	public float animationSpeed;
    public GameObject pausePanel;
    public GameObject tutorialPanel;
    public GameObject panelNow;
    private bool ifopen = false;

    private bool isAnimating;


    [SerializeField] private int tutorialNum = 0;
    public Sprite[] tutorialSprite;


    IEnumerator ShowPanel(GameObject gameObject)
	{
        isAnimating = true;
        ifopen = true;

        float timer = 0;
		while (timer <= 1)
		{
			gameObject.transform.localScale = Vector3.one * showCurve.Evaluate(timer);
			timer += Time.deltaTime * animationSpeed;
			yield return null;
        }
        Time.timeScale = 0;

        isAnimating = false;
    }

    IEnumerator HidePanel(GameObject gameObject)
    {
        isAnimating = true;

        float timer = 0;

        while (timer <= 1)
        {
            gameObject.transform.localScale = Vector3.one * hideCurve.Evaluate(timer);
            timer += Time.deltaTime * animationSpeed;
            yield return null;
        }
        if (gameObject == tutorialPanel)
        {
            tutorialNum = 0;
            tutorialPanel.GetComponent<Image>().sprite = tutorialSprite[0];
        }
        ifopen = false;
        isAnimating = false;
        Time.timeScale = 1;
    }

    public bool Getifopen()
    {
        return this.ifopen;
    }

    public void OpenPanel(GameObject panel)
    {
        MusicManager.Instance.SoundEffectTrigger("按钮");
        panelNow = panel;
        StartCoroutine(ShowPanel(panel));
    }

    public void ClosePanel(GameObject panel)
    {
        MusicManager.Instance.SoundEffectTrigger("按钮");
        panelNow = panel;
        StartCoroutine(HidePanel(panel));
        Time.timeScale = 1;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (ifopen == false)
            {
                OpenPanel(pausePanel);
            }
            else if (ifopen == true)
            {
                ClosePanel(panelNow);
            }
        }else if (panelNow == tutorialPanel&&!isAnimating)
        {
            if (ifopen && tutorialNum < tutorialSprite.Length && Input.anyKeyDown)
            {
                tutorialPanel.GetComponent<Image>().sprite = tutorialSprite[tutorialNum];
                tutorialNum++;
            }
            else if (ifopen && tutorialNum == tutorialSprite.Length && Input.anyKeyDown)
            {
                ClosePanel(panelNow);
            }
        }else if (panelNow==pausePanel&&!isAnimating)
        {
            if (ifopen&&Input.anyKey)
            {
                ClosePanel(panelNow);
            }
        }

    }

    public void ShowTutorial()
    {
        if (ifopen == false&&!isAnimating)
        {
            //ifopen = true;
            tutorialNum++;
            panelNow = tutorialPanel;
            OpenPanel(tutorialPanel);
        }
    }
}

