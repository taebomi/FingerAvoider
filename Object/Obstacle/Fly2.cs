using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly2 : MonoBehaviour {
    Rigidbody2D rb;
    Transform child;
    Vector2 vec;
    public float speed;
    public float rotateSpeed = 8;
    Transform playerTr;
    Vector3 firstPos;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        child = transform.Find("Dir").transform;
        playerTr = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        firstPos = transform.position;
    }
    // Use this for initialization
    public void OnEnable()
    {
        transform.position = firstPos;
        child.transform.up = (Vector2)(playerTr.position - transform.position);
        StartCoroutine(Chase());
    }
    IEnumerator Chase()
    {
        Vector2 targetDir;
        while (true)
        {
            targetDir = child.InverseTransformPoint(playerTr.transform.position);
            if (targetDir.x > 0)
            {
                child.Rotate(0, 0, -rotateSpeed);
            }
            else if (targetDir.x < 0)
            {
                child.Rotate(0, 0, rotateSpeed);
            }
            yield return new WaitForFixedUpdate();
            rb.linearVelocity = child.up * speed;
            yield return new WaitForFixedUpdate();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject.FindGameObjectWithTag("GameManager").SendMessage("GameOver");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject.FindGameObjectWithTag("GameManager").SendMessage("GameOver");
        }
    }
}
