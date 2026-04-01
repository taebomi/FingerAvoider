using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainStory : MonoBehaviour {
	void Start () {
        if (PlayerPrefs.GetInt("ClearNumber") >= 25&&PlayerPrefs.GetFloat("Story")!=2f)
        {
            PlayerPrefs.SetFloat("Story",1.5f);
        }
        if (PlayerPrefs.GetFloat("Story") == 1.5f&&PlayerPrefs.GetInt("ClearNumber")>=25)
        {
            GameSystem.instance.LoadScene("Story");
        }
	}
}
