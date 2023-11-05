using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public bool canHurt = true;

    public float blinkTimeOnce;
    public int blinkNum;

    private Collider2D towerCollider;
    public void Hurt(int atk)
    {
        if (!canHurt)
            return;
        HealthManager.Instance.DecreaseHealth(atk);
    }

    IEnumerator BlinkAndUnableToBeHarmed()
    {
        towerCollider.enabled = false;
        for (int i = 0; i < blinkNum * 2; i++)
        {
            towerCollider.enabled = !towerCollider.enabled;
            yield return new WaitForSeconds(blinkTimeOnce);
        }
        towerCollider.enabled = true;
        towerCollider.enabled = true;
    }
}
