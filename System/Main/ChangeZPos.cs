using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeZPos : MonoBehaviour {
    public float upDownY;
    Transform playerTr;
	// Use this for initialization
	void Start () {
        playerTr = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
	}
    private void FixedUpdate()
    {
        if (playerTr.position.y > upDownY)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -2);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 1);
        }
    }
}
