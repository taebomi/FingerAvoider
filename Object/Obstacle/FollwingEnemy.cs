using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollwingEnemy : MonoBehaviour {
    GamePlay gp;
    Transform playerTf;
    Rigidbody2D rb;
    public float speed;
    Vector3 dir;
    Vector3 firstPos;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        gp = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GamePlay>();
        playerTf = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        firstPos = transform.position;
    }
    private void OnEnable()
    {
        transform.position = firstPos;
        StartCoroutine(CheckStart());
    }
    IEnumerator CheckStart()
    {
        while (!gp.playing)
        {
            yield return new WaitForFixedUpdate();
        }
        StartCoroutine(Chase());
        StartCoroutine(ChaseDir());
    }
    IEnumerator Chase()
    {
        while (gp.playing)
        {
            rb.MovePosition(transform.position + dir * speed);
            yield return new WaitForFixedUpdate();
        }
    }
    IEnumerator ChaseDir()
    {
        while (gp.playing)
        {
            dir = (playerTf.position - transform.position).normalized;
            yield return new WaitForSeconds(0.25f);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GamePlay>().GameOver();
        }
    }
}
