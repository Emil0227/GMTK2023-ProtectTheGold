using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hooker : MonoBehaviour
{
    SpriteRenderer sr;
    public Sprite SpriteOpen;
    public Sprite SpriteClosed;
    public void SetSprite(bool isOpen)
    {
        if (isOpen) 
        {
            sr.sprite = SpriteOpen; 
        }
        else
        {
            sr.sprite = SpriteClosed;
        }
    }
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        SetSprite(true);
    }
    public void OnTriggerEnter2D(Collider2D other)//抓到了东西
    {
        SetSprite(false);//改图像为闭合
        GetComponent<Collider2D>().enabled = false;//关闭碰撞检测
        transform.parent.GetComponent<rope>().GetState = RopeState.Shorten;//改变绳子状态
        other.transform.parent = transform;//让物体虽钩子移动
        other.transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);//调整物体位置
        if (other.tag == "Player")
        {
            ScreenUtils.isPlayerCatched = true;//改变玩家状态
            other.GetComponent<AudioSource>().Play(); //播放音效
            return;
        }
        GetComponent<AudioSource>().Play();//播放音效
    }
}
