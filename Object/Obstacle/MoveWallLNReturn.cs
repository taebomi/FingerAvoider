using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveWallLNReturn : MonoBehaviour {
    public bool isUp;
    public float upLength;
    public float speed;
    public Vector3 firstPos;
    void Start()
    {
        if (isUp)
        {
            transform.DOMoveY(upLength, Mathf.Abs(((upLength - transform.position.y) * speed) / (upLength - firstPos.y))).OnComplete(() => { Return(); }).SetEase(Ease.Linear);
        }
        else
        {
            transform.DOMoveX(upLength, Mathf.Abs(((upLength - transform.position.x) * speed) / (upLength - firstPos.x))).OnComplete(() => { Return(); }).SetEase(Ease.Linear);
        }
    }
    void GoUp()
    {
        transform.DOMoveY(upLength, speed).SetRecyclable(true).OnComplete(() => { Return(); }).SetEase(Ease.Linear);
    }
    void GoRight()
    {
        transform.DOMoveX(upLength, speed).SetRecyclable(true).OnComplete(() => {Return(); }).SetEase(Ease.Linear);
    }
    void Return()
    {
        transform.position = firstPos;
        if (isUp)
            GoUp();
        else
            GoRight();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GamePlay>().GameOver(); 
        }
    }
}
