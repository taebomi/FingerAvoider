using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class LoadingTextEffect : MonoBehaviour {
    Text txt;

    void Start () {
        txt = GetComponent<Text>();
        StartCoroutine(ChangeText());
    }
    IEnumerator ChangeText()
    {
        while (true)
        {
            txt.text = txt.text + ".";
            if(txt.text.Length == 11)
                txt.text = "LOADING";
            yield return new WaitForSeconds(0.05f);
        }
    }
}
