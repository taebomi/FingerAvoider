using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Fly1Boss : MonoBehaviour {
    Rigidbody2D rb;
    Vector2 vec;
    public float speed;
    public float[] xPos;
    public float[] yPos;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        Vector2 randomPos = new Vector2(0, -9f) + Random.insideUnitCircle * 3f;
        rb.linearVelocity = Vector2.zero;
        rb.DOMove(randomPos, Random.Range(0.5f, 1f)).SetRecyclable(true);
    }
    public void Move()
    {
        Vector2 dir = transform.position - new Vector3(0, -9, 0);
        rb.linearVelocity = dir.normalized*2.4f;
        transform.Translate(0, 0, -0.1f);
        StartCoroutine(Remove());
    }
    IEnumerator Remove()
    {
        yield return new WaitForSeconds(22f);
        rb.linearVelocity = Vector2.zero;
        gameObject.SetActive(false);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject.FindGameObjectWithTag("GameManager").SendMessage("GameOver");
        }
    }
}
