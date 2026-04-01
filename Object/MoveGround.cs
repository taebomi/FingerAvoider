using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class MoveGround : MonoBehaviour {

    [System.Serializable]
    public struct MoveInfo
    {
        public Vector3 destination;
        public float speed;
        public Ease ease;
        public float delayTime;
    }
    public MoveInfo[] moveInfo;
    int num;
    float time;
    // Use this for initialization
    void Start () {
        num = 0;
        StartCoroutine(Move());
	}
    IEnumerator Move()
    {
        while (true)
        {
            time = (transform.position - moveInfo[num].destination).magnitude / moveInfo[num].speed;
            if(moveInfo[num].delayTime==0)
                yield return transform.DOMove(moveInfo[num].destination, time).SetEase(moveInfo[num].ease).WaitForCompletion();
            else
                yield return transform.DOMove(moveInfo[num].destination, time).SetEase(moveInfo[num].ease).SetDelay(moveInfo[num].delayTime).WaitForCompletion();
            num++;
            if (num > moveInfo.Length-1)
                num = 0;
        }
    }
}
