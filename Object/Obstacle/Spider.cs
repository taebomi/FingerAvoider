using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Spider : MonoBehaviour
{
    Transform rope;
    Transform spider;
    Transform playerTr;
    Animator ani;
    float firstYPos;
    public float yPos;
    public float distance;
    public int idNum;
    private void Awake()
    {
        firstYPos = transform.position.y;
        spider = transform.Find("Spider");
        rope = transform.Find("Rope");
        playerTr = GameObject.FindGameObjectWithTag("Player").transform;
        ani = spider.gameObject.GetComponent<Animator>();
    }
    private void OnEnable()
    {
        ani.speed = 0;
        DOTween.Kill(idNum);
        DOTween.Kill(idNum + 1);
        DOTween.Kill(idNum + 2);
        DOTween.Kill(idNum + 3);
        rope.localScale = new Vector3(0.1f, 2f, 1);
        spider.localPosition = new Vector3(0, -0.5f, 0);
        StartCoroutine(CheckPlayer());
    }
    void OnDisable()
    {
        CancelInvoke();
    }
    IEnumerator CheckPlayer()
    {
        bool isWait = true;
        while (isWait)
        {
            if (Mathf.Abs(playerTr.position.x - transform.position.x) < distance)
            {
                isWait = false;
                Move();
            }
            yield return new WaitForSeconds(0.05f);
        }
    }
    void Move()
    {
        spider.transform.DOMoveY(yPos, (firstYPos - yPos) / 17f).SetRecyclable(true).SetEase(Ease.OutBounce).SetId(idNum);
        rope.transform.DOScaleY(firstYPos - yPos, (firstYPos - yPos) / 17f).SetRecyclable(true).SetEase(Ease.OutBounce).OnComplete(() => GoUp()).SetId(idNum + 1);
    }
    void GoUp()
    {
        Invoke("GoUp2", 0.7f);
        ani.speed = 1;
    }
    void GoUp2()
    {
        ani.speed = 0;
        spider.transform.DOMoveY(firstYPos, 2f).SetEase(Ease.InOutSine).SetRecyclable(true).SetId(idNum + 2);
        rope.transform.DOScaleY(2, 2f).SetEase(Ease.InOutSine).SetRecyclable(true).SetId(idNum + 3);
        Invoke("Final", 2f);
    }
    void Final()
    {
        StartCoroutine(CheckPlayer());
    }
}
