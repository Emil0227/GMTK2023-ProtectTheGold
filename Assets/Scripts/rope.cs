using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class rope : MonoBehaviour
{
    //记录绳子的旋转、伸缩速度
    public float rotateSpeed;
    public float stretchSpeed;
    public float shortenSpeed;
    //在编辑器里赋值
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
        //初始化摇摆计时
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
        if (timer.Finished)//摇摆状态时等待一定的时间，结束计时后，自动拉伸
        {
            state = RopeState.Stretch;
            return;
        }
        transHooker.GetComponent<Collider2D>().enabled = false;
        if (transform.localRotation.z <= -0.5f)//-60°
        {
            dir = Vector3.forward;
        }
        else if (transform.localRotation.z >= 0.5f)//60°
        {
            dir = Vector3.back;
        }
        transform.Rotate(dir * rotateSpeed * Time.deltaTime);
    }
    private void stretch()
    {  
        if (length >= 10)//伸长到最底端，自动缩短
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
        if (length <= 1)//抵达最上方，自动摇摆
        {
            length = 1;
            transHooker.GetComponent<hooker>().SetSprite(true);//改变钩子图像为张开
            if (transHooker.childCount != 0)//抓到了东西
            {
                Destroy(transHooker.GetChild(0).gameObject);//销毁抓到的物品 
                if (ScreenUtils.isPlayerCatched == true)//如果抓到了玩家，则重置玩家状态，并生成新的玩家
                {
                    ScreenUtils.isPlayerCatched = false;
                    rebirth = GameObject.Instantiate(rebirthEffect);
                    rebirth.transform.position = new Vector3(0.2f, -3.3f,0);
                    Destroy(rebirth, 0.3f);
                    GameObject tempPlayer = GameObject.Instantiate(player);
                    return;
                }
                addGold(transHooker.GetChild(0).tag);//增加相应金币            
            }
            //重置摇摆计时
            stretchDelay = Random.Range(0.2f, 4);
            timer.Duration = stretchDelay;
            timer.Run();
            state = RopeState.Swing;
            return;
        }
        transHooker.GetComponent<Collider2D>().enabled = false;//关闭碰撞检测
        length -= Time.deltaTime * shortenSpeed;
        transform.localScale = new Vector3(transform.localScale.x, length, transform.localScale.z);
        transHooker.localScale = new Vector3(transHooker.localScale.x, 1 / length, transHooker.localScale.z);
    }
    private void addGold(string tag)
    {
        GetComponent<AudioSource>().Play();//播放音效
        switch (tag)
        {
            case "goldL":
                ScreenUtils.CumulativeGoldCount += ScreenUtils.GoldLscore;
                cam.GetComponent<gameManager>().textGold.text = "GOLD: " + ScreenUtils.CumulativeGoldCount.ToString();
                //设置特效
                fx = GameObject.Instantiate(addGoldEffect);
                fx.transform.GetChild(0).GetComponent<Text>().text = "+"+ ScreenUtils.GoldLscore.ToString();
                Destroy(fx, 0.8f);
                break;
            case "goldM":
                ScreenUtils.CumulativeGoldCount += ScreenUtils.GoldMscore;
                cam.GetComponent<gameManager>().textGold.text = "GOLD: " + ScreenUtils.CumulativeGoldCount.ToString();
                //设置特效
                fx = GameObject.Instantiate(addGoldEffect);
                fx.transform.GetChild(0).GetComponent<Text>().text = "+" + ScreenUtils.GoldMscore.ToString();
                Destroy(fx, 0.8f);
                break;
            case "goldS":
                ScreenUtils.CumulativeGoldCount += ScreenUtils.GoldSscore;
                cam.GetComponent<gameManager>().textGold.text = "GOLD: " + ScreenUtils.CumulativeGoldCount.ToString();
                //设置特效
                fx = GameObject.Instantiate(addGoldEffect);
                fx.transform.GetChild(0).GetComponent<Text>().text = "+" + ScreenUtils.GoldSscore.ToString();
                Destroy(fx, 0.8f);
                break;
            case "stone":
                ScreenUtils.CumulativeGoldCount += ScreenUtils.StoneScore;
                cam.GetComponent<gameManager>().textGold.text = "GOLD: " + ScreenUtils.CumulativeGoldCount.ToString();
                //设置特效
                fx = GameObject.Instantiate(addGoldEffect);
                fx.transform.GetChild(0).GetComponent<Text>().text = "+" + ScreenUtils.StoneScore.ToString();
                Destroy(fx, 0.8f);
                break;
        }
    }

}
