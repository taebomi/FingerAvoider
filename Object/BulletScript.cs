using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {
    Rigidbody2D rb;
    public float speed = 0;
    Vector3 firstPos;
    public GameObject ting;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        firstPos = transform.position;
        ting = Instantiate(ting, transform.position, Quaternion.identity)as GameObject;
        ting.SetActive(false);
    }
    private void OnEnable()
    {
        rb.linearVelocity = Vector2.up * speed;
        Invoke("Disable", 2f);
    }
    void Disable()
    {
        transform.position = firstPos;
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Boss"))
        {
            collision.SendMessage("Damaged");
            gameObject.SetActive(false);
            ting.transform.position = transform.position;
            ting.SetActive(true);
            ting.GetComponent<Animator>().Play("BulletTing", -1, 0);
            transform.position = firstPos;
        }
    }
}
