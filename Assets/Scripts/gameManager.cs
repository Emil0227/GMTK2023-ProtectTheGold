using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class gameManager : MonoBehaviour
{
    //��¼����ʱ��Ŀ����
    public float timeCount;
    public float targetGold;
    //�ڱ༭���︳ֵ
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
        GameObject loadScene = GameObject.Instantiate(loadSceneAnim);//�ؿ�������Ч
        textGold.text = "GOLD: "+ ScreenUtils.CumulativeGoldCount.ToString();
    }
    void Update()
    {
        //����ʱ
        timeCount -= Time.deltaTime;
        textTime.text = "TIME: " + ((int)timeCount).ToString();
        //�ж���Ϸ�Ƿ����
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
        if (ScreenUtils.CumulativeGoldCount >= targetGold)//��ҳ���Ŀ�꣬��Ϸʧ��
        {
            gameObject.GetComponent<AudioSource>().Stop();//ֹͣ���ű�������
            ScreenUtils.isLevelFinished = true;
            rope.GetComponent<rope>().GetState = RopeState.Paused;
            maskUI.SetActive(true);
            looseUI.SetActive(true);
        }
        else if (timeCount <= 0)
        {
            gameObject.GetComponent<AudioSource>().Stop();//ֹͣ���ű�������
            ScreenUtils.isLevelFinished = true;
            rope.GetComponent<rope>().GetState = RopeState.Paused;
            maskUI.SetActive(true);
            winUI.SetActive(true);
        }
    }
}
