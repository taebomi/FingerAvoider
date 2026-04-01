using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateMap : MonoBehaviour {
    Quaternion a;
    Transform finishTr;
    Vector3 pos;
    private void Awake()
    {
        finishTr = GameObject.FindGameObjectWithTag("Finish").transform;
        pos = finishTr.transform.position;
        a = transform.rotation;
    }
    // Use this for initialization
    void OnEnable ()
    {
        transform.rotation = a;
        finishTr.SetParent(null);
        finishTr.transform.position = pos;
        finishTr.SetParent(gameObject.transform);
    }

    public float speed;
    void FixedUpdate()
    {
        transform.Rotate(0, 0, speed);
    }
}
