using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : Singleton<HealthManager>
{
    public int health;
    public GameObject healthGrid;
    public GameObject healthPrefab;


    public AnimationCurve showCurve;
    public float animationSpeed;
    public GameObject panel;

    private bool isDead;

    private void Start()
    {
        AddHealth(health);
    }

    public void AddHealth(int healthToAdd)
    {
        health = healthToAdd;
        for (int i = 0; i < healthToAdd; i++)
        {
            Instantiate(healthPrefab, healthGrid.transform.position, Quaternion.identity, healthGrid.transform);
        }
    }

    public void DecreaseHealth(int healthToDecrease)
    {
        health -= healthToDecrease;
        if (health > 0)
        {
            for (int i = healthGrid.transform.childCount - 1; i > healthGrid.transform.childCount - healthToDecrease - 1; i--)
            {
                Destroy(healthGrid.transform.GetChild(i).gameObject);
            }
        }
        else
        {
            for (int i = healthGrid.transform.childCount - 1; i > - 1; i--)
            {
                Destroy(healthGrid.transform.GetChild(i).gameObject);
            }
            StartCoroutine(ShowDeathPanel(panel));
        }
    }
    IEnumerator ShowDeathPanel(GameObject gameObject)
    {
        //if(PoolMgr.Instance().Pool != null)
        //    PoolMgr.Instance().clear();
        float timer = 0;
        while (timer <= 1)
        {
            gameObject.transform.localScale = Vector3.one * showCurve.Evaluate(timer);
            timer += Time.deltaTime * animationSpeed;
            yield return null;
        }
        Time.timeScale = 0;
        isDead = true;
    }

    private void Update()
    {
        if (isDead&&Input.anyKeyDown)
        {
            Time.timeScale = 1;
            LevelManager.Instance.ReturnLevelChoose();
        }
    }
}
