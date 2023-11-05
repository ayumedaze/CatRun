using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    private Collider2D catCollider;
    private SpriteRenderer catRenderer;

    //单次闪烁时间的一半以及闪烁次数
    public float blinkTimeOnce;
    public int blinkNum;

    private void Start()
    {
        catCollider = GetComponent<Collider2D>();
        catRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        transform.rotation = Quaternion.identity;
        if(transform.parent.transform.rotation.z>-0.5&& transform.parent.transform.rotation.z < 0.5)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    public void DecreaseHealthPoints(int num)
    {
        StartCoroutine(BlinkAndUnableToBeHarmed());
        HealthManager.Instance.DecreaseHealth(num);
    }


    //无敌且闪烁
    IEnumerator BlinkAndUnableToBeHarmed()
    {
        catCollider.enabled = false;
        for(int i = 0; i < blinkNum * 2; i++)
        {
            catRenderer.enabled = !catRenderer.enabled;
            yield return new WaitForSeconds(blinkTimeOnce);
        }
        catRenderer.enabled = true;
        catCollider.enabled = true;
    }
}
