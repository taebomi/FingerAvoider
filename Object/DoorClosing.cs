using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class DoorClosing : MonoBehaviour {

    float firstScale;
    BoxCollider2D bc;
    bool isOpen = false;
    public AudioClip[] se;
    public float speed;
    AudioSource ase;
    Coroutine soundC;
    public int idNum;
    Vector3 firstScaleVector3;
    void Awake()
    {
        ase = GetComponent<AudioSource>();
        bc = GetComponent<BoxCollider2D>();
        firstScale = transform.localScale.y;
        firstScaleVector3 = transform.localScale;
    }
    private void OnEnable()
    {
        DOTween.Kill(idNum);
        DOTween.Kill(idNum + 1);
        transform.localScale = firstScaleVector3;
        isOpen = false;
    }
    public void Open()
    {
        if (!isOpen)
        {
            isOpen = true;
            DOTween.Kill(idNum + 1);
            transform.DOScaleY(0, 0.1f).OnComplete(() => { GoDown(); }).SetId(idNum);
            if (se != null)
                GameSystem.instance.PlaySE(se[0]);
        }
        else
        {
            ase.Stop();
            StopCoroutine(soundC);
            DOTween.Kill(idNum);
            DOTween.Kill(idNum + 1);
            transform.DOScaleY(firstScale , 2f).SetId(idNum+1);
            if (se != null)
                GameSystem.instance.PlaySE(se[1]);
            isOpen = false;
        }
    }
    void GoDown()
    {
        transform.DOScaleY(firstScale, speed).OnComplete(()=>{ DownComplete(); }).SetId(idNum).SetEase(Ease.Linear);
        soundC = StartCoroutine(GoDown2());
    }
    void DownComplete()
    {
        isOpen = false;
        ase.Stop();
        ase.PlayOneShot(se[3]);
    }
    IEnumerator GoDown2()
    {
        float time = 1.514f;
        ase.clip = se[2];
        while (isOpen)
        {
            if (transform.localScale.y > firstScale * 0.8)
            {
                ase.clip = se[5];
                time = 0.378f;
            }
            else if (transform.localScale.y > firstScale * 0.4)
            {
                ase.clip = se[4];
                time = 0.757f;
            }
            ase.Play();
            yield return new WaitForSeconds(time);
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
