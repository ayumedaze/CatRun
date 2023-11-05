using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadEff : MonoBehaviour
{
    float timer = 0;
    public string path;
    private Animator effectAnimator;

    // Start is called before the first frame update
    void Start()
    {
        effectAnimator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        timer = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer += Time.deltaTime;
        if (timer > 0.5f )
        {
            
            PoolMgr.Instance().Pushobj(path, this.gameObject);
        }
    }
    public void reset()
    {
        timer = 0;
    }
}
