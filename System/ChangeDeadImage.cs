using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeDeadImage : MonoBehaviour {
	void Start () {
        GetComponent<Image>().sprite = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterScript>().sp[PlayerPrefs.GetInt("Character") * 2 + 1];
	}
}
