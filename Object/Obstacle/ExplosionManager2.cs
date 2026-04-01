using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionManager2 : MonoBehaviour {

    [System.Serializable]
    public struct ExplosionInfo
    {
        public GameObject exObj;
        public bool[] isBomb;
    }
    public ExplosionInfo[] exInfo;
    ExplosionObstacle[] eo;
    public float finishTime;
    public float waitTime;
    public GameObject openDoor;
    public GameObject closeDoor;
    int i = 0;
    private void Awake()
    {
        eo = new ExplosionObstacle[exInfo.Length];
        for (i = 0; i < exInfo.Length; i++)
        {
            eo[i] = exInfo[i].exObj.GetComponent<ExplosionObstacle>();
        }
    }
    private void OnEnable()
    {
        StartCoroutine(Bomb());
    }
    IEnumerator Bomb()
    {
        float tick = waitTime;
        float tickSum;
        int n;
        int length = exInfo.Length;
        int num = 0;
        while (true)
        {
            yield return new WaitForSeconds(2.5f);
            openDoor.SendMessage("Open");
            yield return new WaitForSeconds(0.5f);
            tickSum = Time.time + tick;
            while (num * waitTime != finishTime)
            {
                for (n = 0; n < length; n++)
                {
                    if (exInfo[n].isBomb[num])
                        eo[n].Bomb();
                }
                yield return new WaitForSeconds(tick);
                tickSum += waitTime;
                tick = tickSum - Time.time;
                num++;
            }
            num = 0;
            yield return new WaitForSeconds(1.0f);
            closeDoor.SendMessage("Open");
            yield return new WaitForSeconds(2.5f);
            closeDoor.SendMessage("Open");
            yield return new WaitForSeconds(0.3f);
            openDoor.SendMessage("Open");

        }
    }
}
