using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    Rigidbody2D rBody;
    Animator anim;
    float speed;
    float colliderHalfWidth;
    float colliderHalfHeight;
    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        speed = 2.0f;
        colliderHalfWidth = 0.42f; 
        colliderHalfHeight = 0.27f;
        //Time.timeScale = 1;
    }
    void FixedUpdate()
    {
        if(ScreenUtils.isLevelFinished == false)//�ؿ��ڽ�����
        {
            if (ScreenUtils.isPlayerCatched == false)//���û�б�ץס
            {
                float moveX = Input.GetAxisRaw("Horizontal");
                float moveY = Input.GetAxisRaw("Vertical");
                Vector2 position = transform.position;
                position.x += moveX * speed * Time.fixedDeltaTime;
                position.y += moveY * speed * Time.fixedDeltaTime;
                //transform.Translate(Vector3.right * moveX * speed * Time.deltaTime);
                //transform.Translate(Vector3.up * moveY * speed * Time.deltaTime);
                rBody.MovePosition(position);
                if (moveX < 0)
                {
                    anim.SetInteger("state", 2);
                }
                else if (moveX > 0)
                {
                    anim.SetInteger("state", 1);
                }
                else 
                {
                    anim.SetInteger("state", 0);
                }
            }
            else//��ұ�ץס
            {
                anim.SetInteger("state", 3);
                gameObject.layer = LayerMask.NameToLayer("Gold");
            }
        }
        else//�ؿ�����
        {
            anim.SetInteger("state", 0);
        }
    }
    void Update()
    {
        if (ScreenUtils.isPlayerCatched == false)//û�б�ץס
        {
            ClampInScreen();//���ƽ�ɫ����Ļ��
        }
    }
    void ClampInScreen()
    {
        Vector3 position = transform.position;
        if (position.x - colliderHalfWidth < ScreenUtils.ScreenLeft)
        {
            position.x = ScreenUtils.ScreenLeft + colliderHalfWidth;
        }
        else if (position.x + colliderHalfWidth > ScreenUtils.ScreenRight)
        {
            position.x = ScreenUtils.ScreenRight - colliderHalfWidth;
        }
        if (position.y - colliderHalfHeight < ScreenUtils.ScreenBottom)
        {
            position.y = ScreenUtils.ScreenBottom + colliderHalfHeight;
        }
        else if (position.y > 0)//��ɫ�ִ���Ļ���ϲ�
        {
            position.y = 0;
        }
        transform.position = position;
    }
}
