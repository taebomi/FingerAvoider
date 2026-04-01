using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGround : MonoBehaviour {
    CharacterScript cs;
	// Use this for initialization
	void Start () {
        cs = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterScript>();
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        cs.CheckGround();

    }
}
