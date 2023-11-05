using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillManager : Singleton<SkillManager>
{
    public Sprite[] sprites;
    public Image skillImage;

    //0��ʾû�м��ܣ�1��2��3��4�ֱ��ʾ�죬�̣�������
    private int currentSkill = 0;

    public Image[] skillBar;

    [SerializeField]private bool isUsingSkill;

    private float redTime = 1.5f;
    private float yellowTime=5;
    private float blueTime=5;
    private float greenTime=10;
    // Start is called before the first frame update
    void Start()
    {
        skillImage.sprite = sprites[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)&&!isUsingSkill)
        {
            Debug.Log("ʹ�ü���");
            switch (currentSkill)
            {
                case 1:
                    currentSkill = 0;
                    skillImage.sprite = sprites[0];
                    StartCoroutine(IredSkill());
                    break;
                case 2:
                    currentSkill = 0;
                    skillImage.sprite = sprites[0];
                    StartCoroutine(IgreenSkill());
                    greenTime = 0;
                    break;
                case 3:
                    currentSkill = 0;
                    skillImage.sprite = sprites[0];
                    StartCoroutine(IblueSkill());
                    blueTime = 0;
                    break;
                case 4:
                    currentSkill = 0;
                    skillImage.sprite = sprites[0];
                    StartCoroutine(IyellowSkill());
                    yellowTime = 0;
                    break;
            }
        }
    }
    public void ChangeSkillTo(int i)
    {
        switch (i)
        {
            case 1: 
                currentSkill = 1; 
                skillImage.sprite = sprites[1]; 
                break;
            case 2: 
                currentSkill = 2; 
                skillImage.sprite = sprites[2]; 
                break;
            case 3: 
                currentSkill = 3; 
                skillImage.sprite = sprites[3]; 
                break;
            case 4: 
                currentSkill = 4; 
                skillImage.sprite = sprites[4]; 
                break;
        }
    }

    //��ɫ����
    public IEnumerator IredSkill()
    {
        isUsingSkill = true;
        skillBar[0].enabled = true;
        for (int i = 0; i < GameManager.Instance().BallsInScene.Count; i++)
        {
            Ball ball = GameManager.Instance().BallsInScene[i];
            //ɾ�������
            PoolMgr.Instance().Pushobj(ball.info.resName, ball.gameObject);
            yield return ball;
        }
        GameManager.Instance().BallsInScene.Clear();
        yield return new WaitForSeconds(redTime);

        skillBar[0].enabled = false;
        isUsingSkill = false;
    }

    //��ɫ����
    public IEnumerator IyellowSkill()
    {
        isUsingSkill = true;
        skillBar[3].enabled = true;
        Transform[] Towers = GameManager.Instance().Towers;
        for (int i = 0; i < Towers.Length; i++)
        {
            Towers[i].gameObject.GetComponent<Tower>().canHurt = false;
        }
        //���������벻��������
        yield return new WaitForSeconds(yellowTime);
        Debug.Log("���ܽ���");
        for (int i = 0; i < Towers.Length; i++)
        {
            Towers[i].gameObject.GetComponent<Tower>().canHurt = true;
        }
        skillBar[3].enabled = false;
        isUsingSkill = false;
    }

    //��ɫ����
    public IEnumerator IblueSkill()
    {
        isUsingSkill = true;
        skillBar[2].enabled = true;

        GameManager.Instance().canSetFireInfo = false;
        float a = 0;
        for (int i = 0; i < GameManager.Instance().BallsInScene.Count; i++)
        {
            Ball ball = GameManager.Instance().BallsInScene[i];
            //��������ƶ��ٶ�
            //print(ball.straightSpeed);
            a = ball.info.straightSpeed;
            ball.straightSpeed = a / 2.0f;
            //print(ball.straightSpeed);
            yield return ball;
        }
        //���������벻��������
        yield return new WaitForSeconds(blueTime);
        Debug.Log("���ܽ���");
        GameManager.Instance().canSetFireInfo = true;

        skillBar[2].enabled = false;
        isUsingSkill = false;
    }

    // ��ɫ����
    public IEnumerator IgreenSkill()
    {
        isUsingSkill = true;
        skillBar[1].enabled = true;

        ScoreManager.Instance.isBonus = true;
        yield return new WaitForSeconds(greenTime);
        Debug.Log("���ܽ���");
        ScoreManager.Instance.isBonus = false;

        skillBar[1].enabled = false;
        isUsingSkill = false;
    }
}
