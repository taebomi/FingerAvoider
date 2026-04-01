using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingRefresh : MonoBehaviour {
    Text t;
    private void Start()
    {
        t = GetComponent<Text>();
        StartCoroutine(Refresh());
    }
    IEnumerator Refresh()
    {
        while (true)
        {
            t.text = t.text + ".";
            yield return new WaitForSeconds(0.1f);
        }
    }
}
