using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly1 : MonoBehaviour {
    Rigidbody2D rb;
    Vector2 vec;
    public float speed;
    public float[] xPos;
    public float[] yPos;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        ChangeVelocity();
    }
    // Use this for initialization
    void OnEnable () {
        StartCoroutine(CheckPos());
        StartCoroutine(ChangeVec());
    }
    IEnumerator ChangeVec()
    {
        while (true)
        {
                ChangeVelocity();
                yield return new WaitForSeconds(Random.Range(5f, 15f));
        }
    }
    IEnumerator CheckPos()
    {
        while (true)
        {
            if (transform.position.x > xPos[1])
                {
                    rb.linearVelocity = new Vector2(-1,Random.Range(-0.8f,0.8f)).normalized * speed;
                }
                else if (transform.position.x < xPos[0])
                {
                    rb.linearVelocity = new Vector2(1, Random.Range(-0.8f, 0.8f)).normalized * speed;
                }
                else if (transform.position.y > yPos[1])
                {
                    rb.linearVelocity = new Vector2(Random.Range(-0.8f, 0.8f), -1).normalized * speed;
                }
                else if (transform.position.y < yPos[0])
                {
                    rb.linearVelocity = new Vector2(Random.Range(-0.8f, 0.8f),1).normalized * speed;
                }
            yield return new WaitForSeconds(1f);
        }
    }
    public void ChangeVelocity()
    {
        vec = Vector2.zero;
        int r = Random.Range(0, 4);
        while (vec.sqrMagnitude < 0.2)
            vec = new Vector2(Random.Range(0f, 1f), Random.Range(0f, 1f));
        if (vec.sqrMagnitude > 1f)
            vec = vec.normalized;
        if (r == 0)
            vec = new Vector2(-vec.x,-vec.y);
        else if (r == 1)
            vec = new Vector2(-vec.x, vec.y);
        else if (r == 2)
            vec = new Vector2(vec.x,-vec.y);
        rb.linearVelocity = vec * speed;
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
