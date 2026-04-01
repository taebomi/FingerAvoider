
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ExplosionDoor : MonoBehaviour {

    public float firstScale;
    Vector3 firstScaleVector;
    public bool isOpen = false;
    public AudioClip[] se;
    bool firstIsOpen;
    public int idNum;
    private void Awake()
    {
        if (!isOpen)
            firstScale = transform.localScale.y;
        firstScaleVector = transform.localScale;
        firstIsOpen = isOpen;
    }
    void OnEnable()
    {
        DOTween.Kill(idNum);
        if (firstIsOpen)
        {
            isOpen = true;
            transform.localScale = firstScaleVector;
        }
        else
        {
            isOpen = false;
            transform.localScale = firstScaleVector;
        }
    }
    public void Open()
    {
        if (!isOpen)
        {
            isOpen = true;
            transform.DOScaleY(0, 0.1f).SetId(idNum);
            if (se.Length != 0)
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
}
