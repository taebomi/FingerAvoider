using UnityEngine;
using System.Collections;

public class RotateDoor : MonoBehaviour {
    public float speed;
	void FixedUpdate () {
        transform.Rotate(0, 0, speed);
    }
}
