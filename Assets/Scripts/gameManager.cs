using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class gameManager : MonoBehaviour
{
    //记录倒计时、目标金币
    public float timeCount;
    public float targetGold;
    //在编辑器里赋值
    public GameObject maskUI;
    public GameObject looseUI;
    public GameObject winUI;
    public GameObject loadSceneAnim;
    public GameObject rope;
    public Text textTime;
    public Text textGold;
    private void Awake()
    {
        ScreenUtils.Initialize();
        ScreenUtils.isPlayerCatched = false;
        ScreenUtils.isLevelFinished = false;
    }
    void Start()
    {
        GameObject loadScene = GameObject.Instantiate(loadSceneAnim);//关卡加载特效
        textGold.text = "GOLD: "+ ScreenUtils.CumulativeGoldCount.ToString();
    }
    void Update()
    {
        //倒计时
        timeCount -= Time.deltaTime;
        textTime.text = "TIME: " + ((int)timeCount).ToString();
        //判断游戏是否结束
        if (ScreenUtils.isLevelFinished == false)
        {
            checkWinOrLoose();
        }
        else
        {
            timeCount = 0;
        }
    }
    private void checkWinOrLoose()
    {
        if (ScreenUtils.CumulativeGoldCount >= targetGold)//金币超过目标，游戏失败
        {
            gameObject.GetComponent<AudioSource>().Stop();//停止播放背景音乐
            ScreenUtils.isLevelFinished = true;
            rope.GetComponent<rope>().GetState = RopeState.Paused;
            maskUI.SetActive(true);
            looseUI.SetActive(true);
        }
        else if (timeCount <= 0)
        {
            gameObject.GetComponent<AudioSource>().Stop();//停止播放背景音乐
            ScreenUtils.isLevelFinished = true;
            rope.GetComponent<rope>().GetState = RopeState.Paused;
            maskUI.SetActive(true);
            winUI.SetActive(true);
        }
    }
}
