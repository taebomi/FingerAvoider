using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetSliderValue : MonoBehaviour {
    public string name;
	// Use this for initialization
	void Start () {
        GetComponent<Slider>().value = PlayerPrefs.GetFloat(name);
	}
}
