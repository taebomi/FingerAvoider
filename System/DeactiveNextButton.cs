using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactiveNextButton : MonoBehaviour {
	void Start () {
        if (GameSystem.instance.levelNum == 25)
        {
            gameObject.SetActive(false);
        }
	}
}
