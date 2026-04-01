using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSpriteLanguage : MonoBehaviour {
    public Sprite sprite;
	void Start () {
        if(!GameSystem.instance.isKorean)
        GetComponent<SpriteRenderer>().sprite = sprite;
	}
}
