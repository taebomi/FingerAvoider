using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Door : MonoBehaviour
{
    public float firstScale;
    BoxCollider2D bc;
    public bool isOpen = false;
    bool firstIsOpen;
    public AudioClip[] se;
    Vector3 firstScaleVector3;
    public int idNum;
    private void Awake()
    {
        bc = GetComponent<BoxCollider2D>();
        firstScaleVector3 = transform.localScale;
        firstIsOpen = isOpen;
        if (!isOpen)
            firstScale = transform.localScale.y;
    }
    private void OnEnable()
    {
        DOTween.Kill(idNum);
        isOpen = firstIsOpen;
        transform.localScale = firstScaleVector3;
    }
    public void Open()
    {
        if (!isOpen)
        {
            isOpen = true;
            transform.DOScaleY(0, 0.1f).SetId(idNum);
            if(se.Length!=0)
                GameSystem.instance.PlaySE(se[0]);
        }
        else
        {
            transform.DOScaleY(firstScale, 0.1f).SetId(idNum);
            if (se.Length != 0)
                GameSystem.instance.PlaySE(se[1]);
            isOpen = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GamePlay>().GameOver();
        }
    }
}
