using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class MoveWallLR : MonoBehaviour {
    public bool isUp;
    public float upLength;
    public float downLength;
    public float speed;
    void Start()
    {
        if (isUp)
        {
            transform.DOMoveY(upLength, ((upLength - transform.position.y) *speed) / (upLength - downLength)).OnComplete(() => { GoDown(); }).SetEase(Ease.Linear);
        }
        else
        {
            transform.DOMoveX(upLength, ((upLength - transform.position.x) * speed) / (upLength - downLength)).OnComplete(() => { GoLeft(); }).SetEase(Ease.Linear);
        }
    }
    void GoUp()
    {
        transform.DOMoveY(upLength, speed).SetRecyclable(true).OnComplete(()=> { GoDown(); }).SetEase(Ease.Linear);
    }
    void GoDown()
    {
        transform.DOMoveY(downLength, speed).SetRecyclable(true).OnComplete(() => { GoUp(); }).SetEase(Ease.Linear);
    }
    void GoRight()
    {
        transform.DOMoveX(upLength, speed).SetRecyclable(true).OnComplete(() => { GoLeft(); }).SetEase(Ease.Linear);
    }
    void GoLeft()
    {
        transform.DOMoveX(downLength, speed).SetRecyclable(true).OnComplete(() => { GoRight(); }).SetEase(Ease.Linear);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GamePlay>().GameOver();
        }
    }
}
