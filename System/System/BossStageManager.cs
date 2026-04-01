using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStageManager : MonoBehaviour {

    void Awake()
    {
        if (PlayerPrefs.GetFloat("Story")==1)
        {
            GameSystem.instance.isGame = true;
            GameSystem.instance.LoadScene("Story");
        }
    }
	void Start () {
		
	}
}
