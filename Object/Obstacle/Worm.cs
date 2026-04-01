using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Worm : MonoBehaviour
{
    public enum Option { returnFirst, shuttle };
    PolygonCollider2D[] pc;
    Animator ani;
    public Vector3 returnPos;
    public float pos;
    public bool isY;
    public bool isFlip;
    public Option option;
    Vector3 dir;
    float speed;
    GameObject g;
    bool isFliping = false;
    // Use this for initialization
    private void Awake()
    {
        pc = GetComponents<PolygonCollider2D>();
        ani = GetComponent<Animator>();
        g = null;
        if (returnPos == null)
            returnPos = transform.position;

    }
    void Start()
    {
        Refrash();
    }
    void Refrash()
    {
        if (isFlip)
            speed = -2.5f;
        else
            speed = 2.5f;
        if (isY)
            dir = new Vector3(0, speed, 0);
        else
            dir = new Vector3(speed, 0, 0);
    }
    public void Check()
    {

        switch (option)
        {
            case Option.returnFirst:
                ReturnFirst();
                break;
        }
    }
    void ReturnFirst()
    {
        if (isY)
        {
            if (isFlip)
            {
                if(pos<transform.position.y)
                    transform.position = returnPos;
            }
            else
            {
                if(pos>transform.position.y)
                    transform.position = returnPos;
            }
        }
        else
        {
            if (isFlip)
            {
                if(pos<transform.position.x)
                    transform.position = returnPos;
            }
            else
            {
               if(pos>transform.position.x)
                    transform.position = returnPos;
            }
        }
        Refrash();
    }
    void Event(GameObject gO)
    {
        if (g != gO)
        {
            g = gO;
            Shuttle();
        }
    }
    void Shuttle()
    {
        ani.speed = 0;
        isFliping = true;
        if (isFlip)
            transform.DOScaleX(1, 1).SetEase(Ease.InOutSine);
        else
            transform.DOScaleX(-1, 1).SetEase(Ease.InOutSine);
        Invoke("Shuttle2", 1f);
    }
    void Shuttle2()
    {
        isFlip = !isFlip;
        isFliping = false;
        ani.speed = 1;
        Refrash();
    }
    public void ChangePC1()
    {
        pc[7].enabled = false;
        pc[0].enabled = true;
    }
    public void ChangePC2()
    {
        pc[0].enabled = false;
        pc[1].enabled = true;
    }
    public void ChangePC3()
    {
        pc[1].enabled = false;
        pc[2].enabled = true;
    }
    public void ChangePC4()
    {
        pc[2].enabled = false;
        pc[3].enabled = true;
    }
    public void ChangePC5()
    {
        pc[3].enabled = false;
        pc[4].enabled = true;
    }
    public void ChangePC6()
    {
        pc[4].enabled = false;
        pc[5].enabled = true;
    }
    public void ChangePC7()
    {
        pc[5].enabled = false;
        pc[6].enabled = true;
    }
    public void ChangePC8()
    {
        pc[6].enabled = false;
        pc[7].enabled = true;
    }
    public void ChangePC9()
    {
        pc[7].enabled = false;
        pc[5].enabled = true;
        transform.DOMove(transform.position - dir, 0.33f).OnComplete(() => { Check(); });
    }
    public void ChangePC10()
    {
        pc[5].enabled = false;
        pc[3].enabled = true;
    }
    public void ChangePC11()
    {
        pc[3].enabled = false;
        pc[1].enabled = true;
    }
    public void ChangePC12()
    {
        pc[1].enabled = false;
        pc[0].enabled = true;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject.FindGameObjectWithTag("GameManager").SendMessage("GameOver");
        }
    }
}
