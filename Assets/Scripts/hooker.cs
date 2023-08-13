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
    public void OnTriggerEnter2D(Collider2D other)//ץ���˶���
    {
        SetSprite(false);//��ͼ��Ϊ�պ�
        GetComponent<Collider2D>().enabled = false;//�ر���ײ���
        transform.parent.GetComponent<rope>().GetState = RopeState.Shorten;//�ı�����״̬
        other.transform.parent = transform;//�������乳���ƶ�
        other.transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);//��������λ��
        if (other.tag == "Player")
        {
            ScreenUtils.isPlayerCatched = true;//�ı����״̬
            other.GetComponent<AudioSource>().Play(); //������Ч
            return;
        }
        GetComponent<AudioSource>().Play();//������Ч
    }
}
