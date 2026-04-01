using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindZone : MonoBehaviour {
    GameObject playerObj;
    public float speed;
    public enum Direction { Left,Up,Down,Right}
    public Direction dir;
	// Use this for initialization
	void Start () {
        playerObj = GameObject.FindGameObjectWithTag("Player");
	}
    public void ChangeSpeed(float n)
    {
        speed = n;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject == playerObj)
        {
                if (dir == Direction.Left)
                    playerObj.transform.Translate(Vector3.left * speed);
                else if (dir == Direction.Right)
                    playerObj.transform.Translate(Vector3.right * speed);
                else if (dir == Direction.Up)
                    playerObj.transform.Translate(Vector3.up * speed);
                else
                    playerObj.transform.Translate(Vector3.down * speed);
        }
    }

}
