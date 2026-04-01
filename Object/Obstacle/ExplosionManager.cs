using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ExplosionManager : MonoBehaviour
{
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
        float tickSum = Time.time + tick;
        int n;
        int length = exInfo.Length;
        int num = 0;
        while (true)
        {
            for (n = 0; n < length; n++)
            {
                if(exInfo[n].isBomb[num])
                    eo[n].Bomb();
            }
            yield return new WaitForSeconds(tick);
            tickSum += waitTime;
            tick = tickSum - Time.time;
            num++;
            if (num * waitTime == finishTime) {
                num = 0;
            }
        }
    }
}
