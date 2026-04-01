using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Note : MonoBehaviour {
    float speed = 14;
    public AudioClip se;
    public int lineNum;
    RhythmStage rs;
    int id;
    private void Awake()
    {
        rs = GameObject.FindGameObjectWithTag("RhythmManager").GetComponent<RhythmStage>();
    }
    void OnEnable()
    {
        id = rs.GetID();
        transform.DOMoveX(16.8f, 3.9f).OnComplete(() => { Complete(); }).SetEase(Ease.Linear).SetId(id);
    }
    private void OnDisable()
    {
        DOTween.Kill(id);
    }
    void Complete()
    {
        GameSystem.instance.PlaySE(se);
        rs.Bomb(lineNum);
        gameObject.SetActive(false);
    }
}
