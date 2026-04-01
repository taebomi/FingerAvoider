using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly2Boss : MonoBehaviour
{
    SpriteRenderer sr;
    Rigidbody2D rb;
    Transform child;
    Vector2 vec;
    public float speed;
    public float rotateSpeed = 8;
    Transform playerTr;
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        child = transform.Find("Dir").transform;
        playerTr = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    private void OnEnable()
    {
        sr.color = new Color(1, 1, 1, 1);
        child.transform.up = (Vector2)(playerTr.position - transform.position);
        StartCoroutine(Chase());
        Remove();
    }
    public void Remove()
    {
        StartCoroutine(RemoveStart());
    }
    IEnumerator RemoveStart()
    {
        float n = 1;
        yield return new WaitForSeconds(6.5f);
        while (sr.color.a > 0)
        {
            n -= 0.05f;
            sr.color = new Color(1, 1, 1, n);
            yield return new WaitForSeconds(0.08f);
        }
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
