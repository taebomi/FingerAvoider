using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDodge : MonoBehaviour {

    Rigidbody2D rb;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = transform.up * 15f;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject.FindGameObjectWithTag("GameManager").SendMessage("GameOver");
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        Vector3 pos;
        Quaternion rot;
        int n = Random.Range(0, 4);
        if (n == 0)
            pos = new Vector2(Random.Range(40f, 50f), Random.Range(-50f, 50f));
        else if (n == 1)
            pos = new Vector2(Random.Range(-50f, -40f), Random.Range(-50f, 50f));
        else if (n == 2)
            pos = new Vector2(Random.Range(-50f, 50f), Random.Range(40f, 50f));
        else
            pos = new Vector2(Random.Range(-50f, 50f), Random.Range(-50f, -40f));
        rot = Quaternion.Euler(0, 0, Random.Range(0, 360f));
        transform.rotation = rot;
        transform.position = pos;
        rb.linearVelocity = transform.up * 15f;
    }
}
