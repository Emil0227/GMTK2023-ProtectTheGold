using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class rope : MonoBehaviour
{
    //��¼���ӵ���ת�������ٶ�
    public float rotateSpeed;
    public float stretchSpeed;
    public float shortenSpeed;
    //�ڱ༭���︳ֵ
    public GameObject player;
    public GameObject addGoldEffect;
    public GameObject rebirthEffect;
    //
    private Camera cam;
    private Timer timer; 
    private RopeState state;
    private Vector3 dir;
    private Transform transHooker;
    private GameObject fx;
    private GameObject rebirth;
    private float stretchDelay;
    private float length; 
    public RopeState GetState
    {
        set{ state = value; }
        get{ return state; }
    }
    void Start()
    {
        //��ʼ��ҡ�ڼ�ʱ
        state = RopeState.Swing;
        stretchDelay = 2;
        timer = gameObject.GetComponent<Timer>();
        timer.Duration = stretchDelay;
        timer.Run();
        dir = Vector3.back;

        cam = GameObject.FindObjectOfType<Camera>();
        transHooker = transform.GetChild(0);   
        length = 1;
    }
    void Update()
    {
        if (state == RopeState.Swing)
        {
            swing();
        }
        else if (state == RopeState.Stretch)
        {
            stretch();
        }
        else if (state == RopeState.Shorten)
        {
            shorten();
        }
    }
    private void swing()
    {   
        if (timer.Finished)//ҡ��״̬ʱ�ȴ�һ����ʱ�䣬������ʱ���Զ�����
        {
            state = RopeState.Stretch;
            return;
        }
        transHooker.GetComponent<Collider2D>().enabled = false;
        if (transform.localRotation.z <= -0.5f)//-60��
        {
            dir = Vector3.forward;
        }
        else if (transform.localRotation.z >= 0.5f)//60��
        {
            dir = Vector3.back;
        }
        transform.Rotate(dir * rotateSpeed * Time.deltaTime);
    }
    private void stretch()
    {  
        if (length >= 10)//�쳤����׶ˣ��Զ�����
        {
            state = RopeState.Shorten;
            return;
        }
        transHooker.GetComponent<Collider2D>().enabled = true;
        length += Time.deltaTime * stretchSpeed;
        transform.localScale = new Vector3(transform.localScale.x, length, transform.localScale.z);
        transHooker.localScale = new Vector3(transHooker.localScale.x, 1/length, transHooker.localScale.z);
    }
    private void shorten()
    {
        if (length <= 1)//�ִ����Ϸ����Զ�ҡ��
        {
            length = 1;
            transHooker.GetComponent<hooker>().SetSprite(true);//�ı乳��ͼ��Ϊ�ſ�
            if (transHooker.childCount != 0)//ץ���˶���
            {
                Destroy(transHooker.GetChild(0).gameObject);//����ץ������Ʒ 
                if (ScreenUtils.isPlayerCatched == true)//���ץ������ң����������״̬���������µ����
                {
                    ScreenUtils.isPlayerCatched = false;
                    rebirth = GameObject.Instantiate(rebirthEffect);
                    rebirth.transform.position = new Vector3(0.2f, -3.3f,0);
                    Destroy(rebirth, 0.3f);
                    GameObject tempPlayer = GameObject.Instantiate(player);
                    return;
                }
                addGold(transHooker.GetChild(0).tag);//������Ӧ���            
            }
            //����ҡ�ڼ�ʱ
            stretchDelay = Random.Range(0.2f, 4);
            timer.Duration = stretchDelay;
            timer.Run();
            state = RopeState.Swing;
            return;
        }
        transHooker.GetComponent<Collider2D>().enabled = false;//�ر���ײ���
        length -= Time.deltaTime * shortenSpeed;
        transform.localScale = new Vector3(transform.localScale.x, length, transform.localScale.z);
        transHooker.localScale = new Vector3(transHooker.localScale.x, 1 / length, transHooker.localScale.z);
    }
    private void addGold(string tag)
    {
        GetComponent<AudioSource>().Play();//������Ч
        switch (tag)
        {
            case "goldL":
                ScreenUtils.CumulativeGoldCount += ScreenUtils.GoldLscore;
                cam.GetComponent<gameManager>().textGold.text = "GOLD: " + ScreenUtils.CumulativeGoldCount.ToString();
                //������Ч
                fx = GameObject.Instantiate(addGoldEffect);
                fx.transform.GetChild(0).GetComponent<Text>().text = "+"+ ScreenUtils.GoldLscore.ToString();
                Destroy(fx, 0.8f);
                break;
            case "goldM":
                ScreenUtils.CumulativeGoldCount += ScreenUtils.GoldMscore;
                cam.GetComponent<gameManager>().textGold.text = "GOLD: " + ScreenUtils.CumulativeGoldCount.ToString();
                //������Ч
                fx = GameObject.Instantiate(addGoldEffect);
                fx.transform.GetChild(0).GetComponent<Text>().text = "+" + ScreenUtils.GoldMscore.ToString();
                Destroy(fx, 0.8f);
                break;
            case "goldS":
                ScreenUtils.CumulativeGoldCount += ScreenUtils.GoldSscore;
                cam.GetComponent<gameManager>().textGold.text = "GOLD: " + ScreenUtils.CumulativeGoldCount.ToString();
                //������Ч
                fx = GameObject.Instantiate(addGoldEffect);
                fx.transform.GetChild(0).GetComponent<Text>().text = "+" + ScreenUtils.GoldSscore.ToString();
                Destroy(fx, 0.8f);
                break;
            case "stone":
                ScreenUtils.CumulativeGoldCount += ScreenUtils.StoneScore;
                cam.GetComponent<gameManager>().textGold.text = "GOLD: " + ScreenUtils.CumulativeGoldCount.ToString();
                //������Ч
                fx = GameObject.Instantiate(addGoldEffect);
                fx.transform.GetChild(0).GetComponent<Text>().text = "+" + ScreenUtils.StoneScore.ToString();
                Destroy(fx, 0.8f);
                break;
        }
    }

}
