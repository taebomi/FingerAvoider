using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2_5 : MonoBehaviour {
    public GameObject flyObj;
    public GameObject flyObj2;
    GamePlay gp;
	void Start () {
        gp = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GamePlay>();
        StartCoroutine(MakeFly());
        StartCoroutine(CheckFinish());
	}
    IEnumerator CheckFinish()
    {
        while (gp.GetTime()<45)
        {
            yield return new WaitForSeconds(0.1f);
        }
        gp.SetFinishPos(0, 14);
    }
    IEnumerator MakeFly()
    {
        int n;
        Vector3 vec;
        for (int i = 0; i < 100; i++)
        {
            vec = new Vector3(Random.Range(-39f, 39f), Random.Range(-28f, 28f));
            n = Random.Range(0, 4);
            if (n == 0)
                vec = new Vector3(39f, vec.y);
            else if (n == 1)
                vec = new Vector3(-39f, -vec.y);
            else if (n == 2)
                vec = new Vector3(vec.x, 28f);
            else
                vec = new Vector3(vec.x, -28f);
            Instantiate(flyObj, vec, Quaternion.identity, transform.parent);
            yield return new WaitForSeconds(0.1f);
        }
        vec = new Vector3(Random.Range(-39f, 39f), Random.Range(-28f, 28f));
        n = Random.Range(0, 4);
        if (n == 0)
            vec = new Vector3(39f, vec.y);
        else if (n == 1)
            vec = new Vector3(-39f, -vec.y);
        else if (n == 2)
            vec = new Vector3(vec.x, 28f);
        else
            vec = new Vector3(vec.x, -28f);
        Instantiate(flyObj2, vec, Quaternion.identity, transform.parent);
    }
}
