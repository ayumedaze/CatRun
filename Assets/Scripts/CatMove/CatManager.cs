using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class CatManager : MonoBehaviour
{
    //蛇头和蛇身间距
    [SerializeField] float distanceBetween = 0.2f;
    //移动速度
    [SerializeField] float speed = 200;
    //转向速度
    [SerializeField] float turnSpeed = 180;
    //待生成身体列表,报错处理把SerializeField去了
    List<GameObject> bodyParts = new List<GameObject>();
    public GameObject cat;
    public GameObject BallFollowCat1;
    public GameObject BallFollowCat2;
    public GameObject BallFollowCat3;
    public GameObject BallFollowCat4;
    public GameObject BallFollowCat5;

    public TMP_Text tooMuchBall;

    List<GameObject> snakeBody = new List<GameObject>();

    float countUp=0;

    public UnityAction<int> AddBodyPartsEvent;

    private bool isDestory;
    private void OnEnable()
    {
        AddBodyPartsEvent = AddBodyParts;
        EventCenter.Instance().AddEventListener<int>("AddBall",AddBodyPartsEvent);
    }

    private void OnDisable()
    {
        EventCenter.Instance().RemoveEventListener<int>("AddBall", AddBodyPartsEvent);
    }

    private void Start()
    {
        bodyParts.Add(cat);
        CreateBodyparts();
    }

    private void FixedUpdate()
    {
        ManegeSnakeBody();
        SnakeMovement();

    }

    private void Update()
    {
        //测试用
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            AddBodyParts(1);
        }else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            AddBodyParts(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            AddBodyParts(3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            AddBodyParts(4);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            AddBodyParts(5);
        }
    }

    private void SnakeMovement()
    {
        //Moves the heads with inputs
        snakeBody[0].GetComponent<Rigidbody2D>().velocity = snakeBody[0].transform.right * speed * Time.deltaTime;

        Vector2 playerInput;
        playerInput.x = Input.GetAxis("Horizontal");
        playerInput.y = Input.GetAxis("Vertical");
        playerInput = Vector2.ClampMagnitude(playerInput, 1f);
        float angle = Vector3.Angle(new Vector3(1, 0, 0), playerInput);
        angle = playerInput.y > 0 ? angle : -angle;
        Quaternion targetDirect = Quaternion.Euler(0, 0, angle);
        if (playerInput != new Vector2(0, 0))
        {
            snakeBody[0].transform.rotation = Quaternion.RotateTowards(snakeBody[0].transform.rotation, targetDirect, turnSpeed*Time.deltaTime);
        }


        if (snakeBody.Count > 1)
        {
            for (int i = 1; i < snakeBody.Count; i++)
            {
                MarkerManager markM = snakeBody[i - 1].GetComponent<MarkerManager>();
                snakeBody[i].transform.position = markM.markerList[0].position;
                snakeBody[i].transform.rotation = markM.markerList[0].rotation;
                markM.markerList.RemoveAt(0);
            }
        }

    }

    //按初始列表创建。先照着教程搞一个，其实应该没这么复杂
    private void CreateBodyparts()
    {
        if (snakeBody.Count == 0)
        {
            GameObject temp1 = Instantiate(bodyParts[0], transform.position, transform.rotation, transform);
            if (!temp1.GetComponent<MarkerManager>())
            {
                temp1.AddComponent<MarkerManager>();
            }

            if (!temp1.GetComponent<Rigidbody2D>())
            {
                temp1.AddComponent<Rigidbody2D>();
                temp1.GetComponent<Rigidbody2D>().gravityScale = 0;
            }

            snakeBody.Add(temp1);
            if (bodyParts.Count > 1)
            {
                bodyParts.RemoveAt(0);
            }
            else
            {
                bodyParts.Clear();
            }
            return;
        }
        MarkerManager markM = snakeBody[snakeBody.Count - 1].GetComponent<MarkerManager>();
        if (countUp == 0)
        {
            markM.ClearmarkerList();
        }
        countUp += Time.deltaTime;
        if (countUp >= distanceBetween)
        {
            //根据当前列表创建
            GameObject temp = Instantiate(bodyParts[0], markM.markerList[0].position, markM.markerList[0].rotation, transform);

            if (!temp.GetComponent<MarkerManager>())
            {
                temp.AddComponent<MarkerManager>();
            }

            if (!temp.GetComponent<Rigidbody2D>())
            {
                temp.AddComponent<Rigidbody2D>();
                temp.GetComponent<Rigidbody2D>().gravityScale = 0;
            }
            snakeBody.Add(temp);
            if (bodyParts.Count > 1)
            {
                bodyParts.RemoveAt(0);
            }
            else
            {
                bodyParts.Clear();
            }

            temp.GetComponent<MarkerManager>().ClearmarkerList();
            countUp = 0;
        }
    }

    //初始化列表，并且如果body中间空了自动衔接
    private void ManegeSnakeBody()
    {
        if (bodyParts.Count > 0)
        {

            CreateBodyparts();
        }
        for (int i = 0; i < snakeBody.Count; i++)
        {
            if (snakeBody[i] == null)
            {
                snakeBody.RemoveAt(i);
                i = i - 1;
            }
        }
        if (snakeBody.Count == 0)
        {
            Destroy(this);
        }
    }


    IEnumerator ShowTooMuchBall()
    {
        tooMuchBall.enabled = true;
        yield return new WaitForSeconds(2);
        tooMuchBall.enabled = false;
    }

    //在蛇尾添加物体
    public void AddBodyParts(int i)
    {
        if (isDestory)
        {
            if (tooMuchBall.enabled)
            {
                return;
            }
            else
            {
                StartCoroutine(ShowTooMuchBall());
            }
            return;
        }
        switch (i)
        {
            case 1: bodyParts.Add(BallFollowCat1); break;
            case 2: bodyParts.Add(BallFollowCat2); break;
            case 3: bodyParts.Add(BallFollowCat3); break;
            case 4: bodyParts.Add(BallFollowCat4); break;
            case 5: bodyParts.Add(BallFollowCat5); break;
        }
        StartCoroutine(Eliminate());
    }
    //三消
    IEnumerator Eliminate()
    {
        isDestory = true;
        yield return new WaitForSeconds(distanceBetween+0.05f);
        //通过tag分辨进行更多操作
        if (snakeBody.Count > 3)
        {
            if (snakeBody.Count == 4)
            {
                if (snakeBody[snakeBody.Count - 2].CompareTag("BallFollowCat5") && snakeBody[snakeBody.Count - 3].CompareTag("BallFollowCat5") && !snakeBody[snakeBody.Count - 1].CompareTag("BallFollowCat5"))
                {
                    //根据tag获得技能
                    SkillManager.Instance.ChangeSkillTo(int.Parse(snakeBody[snakeBody.Count - 1].tag.Substring(snakeBody[snakeBody.Count - 1].tag.Length - 1, 1)));

                    Destroy(snakeBody[snakeBody.Count - 1]);
                    Destroy(snakeBody[snakeBody.Count - 2]);
                    Destroy(snakeBody[snakeBody.Count - 3]);
                    MusicManager.Instance.SoundEffectTrigger("三消");
                    ScoreManager.Instance.AddScore(100);
                }
            }
            if (snakeBody[snakeBody.Count - 1].tag == snakeBody[snakeBody.Count - 2].tag && snakeBody[snakeBody.Count - 2].tag == snakeBody[snakeBody.Count - 3].tag)
            {
                Destroy(snakeBody[snakeBody.Count - 1]);
                Destroy(snakeBody[snakeBody.Count - 2]);
                Destroy(snakeBody[snakeBody.Count - 3]);
                MusicManager.Instance.SoundEffectTrigger("三消");
                ScoreManager.Instance.AddScore(100);
            } else if (snakeBody[snakeBody.Count - 1].tag=="BallFollowCat5")
            {
                if(snakeBody[snakeBody.Count - 2].tag == snakeBody[snakeBody.Count - 3].tag)
                {
                    //根据tag获得技能
                    SkillManager.Instance.ChangeSkillTo(int.Parse(snakeBody[snakeBody.Count - 2].tag.Substring(snakeBody[snakeBody.Count - 2].tag.Length - 1, 1)));

                    Destroy(snakeBody[snakeBody.Count - 1]);
                    Destroy(snakeBody[snakeBody.Count - 2]);
                    Destroy(snakeBody[snakeBody.Count - 3]);
                    MusicManager.Instance.SoundEffectTrigger("三消");
                    ScoreManager.Instance.AddScore(100);
                }else if (snakeBody[snakeBody.Count-2].CompareTag("BallFollowCat5"))
                {
                    //根据tag获得技能
                    SkillManager.Instance.ChangeSkillTo(int.Parse(snakeBody[snakeBody.Count - 3].tag.Substring(snakeBody[snakeBody.Count - 3].tag.Length - 1, 1)));

                    Destroy(snakeBody[snakeBody.Count - 1]);
                    Destroy(snakeBody[snakeBody.Count - 2]);
                    Destroy(snakeBody[snakeBody.Count - 3]);
                    MusicManager.Instance.SoundEffectTrigger("三消");
                    ScoreManager.Instance.AddScore(100);
                }else if (snakeBody[snakeBody.Count - 3].CompareTag("BallFollowCat5"))
                {
                    //根据tag获得技能
                    SkillManager.Instance.ChangeSkillTo(int.Parse(snakeBody[snakeBody.Count - 2].tag.Substring(snakeBody[snakeBody.Count - 2].tag.Length - 1, 1)));

                    Destroy(snakeBody[snakeBody.Count - 1]);
                    Destroy(snakeBody[snakeBody.Count - 2]);
                    Destroy(snakeBody[snakeBody.Count - 3]);
                    MusicManager.Instance.SoundEffectTrigger("三消");
                    ScoreManager.Instance.AddScore(100);
                }

            }else if (snakeBody[snakeBody.Count - 2].CompareTag("BallFollowCat5") && snakeBody[snakeBody.Count - 1].tag == snakeBody[snakeBody.Count - 3].tag)
            {
                //根据tag获得技能
                SkillManager.Instance.ChangeSkillTo(int.Parse(snakeBody[snakeBody.Count - 1].tag.Substring(snakeBody[snakeBody.Count - 1].tag.Length - 1, 1)));

                Destroy(snakeBody[snakeBody.Count - 1]);
                Destroy(snakeBody[snakeBody.Count - 2]);
                Destroy(snakeBody[snakeBody.Count - 3]);
                MusicManager.Instance.SoundEffectTrigger("三消");
                ScoreManager.Instance.AddScore(100);
            }else if (snakeBody[snakeBody.Count - 3].CompareTag("BallFollowCat5") && snakeBody[snakeBody.Count - 1].tag == snakeBody[snakeBody.Count - 2].tag)
            {
                //根据tag获得技能
                SkillManager.Instance.ChangeSkillTo(int.Parse(snakeBody[snakeBody.Count - 1].tag.Substring(snakeBody[snakeBody.Count - 1].tag.Length - 1, 1)));

                Destroy(snakeBody[snakeBody.Count - 1]);
                Destroy(snakeBody[snakeBody.Count - 2]);
                Destroy(snakeBody[snakeBody.Count - 3]);
                MusicManager.Instance.SoundEffectTrigger("三消");
                ScoreManager.Instance.AddScore(100);
            }
        }
        isDestory = false;
    }
}
