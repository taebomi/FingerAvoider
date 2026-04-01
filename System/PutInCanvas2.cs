using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutInCanvas2 : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        transform.SetParent(GameObject.FindGameObjectWithTag("StageT").transform);
    }
}
