using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeTextLanguage : MonoBehaviour {
    string[] str;
	void Start () {
        str = GetComponent<Text>().text.Split('\n');
        if (GameSystem.instance.isKorean)
            GetComponent<Text>().text = str[0];
        else
        {
            GetComponent<Text>().text = str[1];
            GetComponent<Text>().font = GameSystem.instance.font;
        }
    }
}
