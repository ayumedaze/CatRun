using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallFollowCat : MonoBehaviour
{
    private Collider2D ballCollider;

    // Start is called before the first frame update
    void Start()
    {
        ballCollider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("CatHurt"))
        {
            collision.gameObject.GetComponent<Cat>().DecreaseHealthPoints(1);
        }
    }
}
