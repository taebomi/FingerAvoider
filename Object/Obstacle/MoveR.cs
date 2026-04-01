using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class MoveR : MonoBehaviour
{
    public bool isUp;
    public float upLength;
    public float speed;
    Vector3 pos;
    public int idNum;
    GamePlay gp;
    private void Awake()
    {
        pos = transform.position;
        gp = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GamePlay>();
    }
    private void OnEnable()
    {
        DOTween.Kill(idNum);
        transform.position = pos;
        StartCoroutine(CheckStart());
    }
    IEnumerator CheckStart()
    {
        while (!gp.playing)
        {
            yield return null;
        }
        if (isUp)
            GoUp();
        else
            GoRight();
    }
    void GoUp()
    {
        transform.DOMoveY(upLength, speed).SetEase(Ease.Linear).SetId(idNum);
    }
    void GoRight()
    {
        transform.DOMoveX(upLength, speed).SetEase(Ease.Linear).SetId(idNum);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GamePlay>().GameOver();
        }
    }
}
