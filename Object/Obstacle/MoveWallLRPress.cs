using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class MoveWallLRPress : MonoBehaviour {
    
    public bool isUpDir;
    public float upLength;
    public float downLength;
    public float speed;
    void Start()
    {
        if (isUpDir)
            transform.DOScaleY(upLength, ((transform.localScale.y-upLength) * speed) / (downLength - upLength)).SetEase(Ease.InSine).OnComplete(() => { GoDown(); });
        else
            transform.DOScaleY(downLength, ((downLength-transform.localScale.y)* speed) / (downLength-upLength)).SetEase(Ease.OutQuint).OnComplete(() => { GoUp(); });
    }
    void GoUp()
    {
        transform.DOScaleY(upLength, speed*1.2f).SetEase(Ease.InSine).SetRecyclable(true).OnComplete(() => { GoDown(); } );
    }
    void GoDown()
    {
        transform.DOScaleY(downLength, speed).SetEase(Ease.OutQuint).SetRecyclable(true).OnComplete(() => { GoUp(); });
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GamePlay>().GameOver();
        }
    }
}
