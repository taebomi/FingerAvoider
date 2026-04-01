using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombFly : MonoBehaviour
{
    Rigidbody2D rb;
    Transform child;
    Vector2 vec;
    public float speed;
    public float rotateSpeed = 0;
    Transform playerTr;
    public GameObject explosion;
    public GameObject boss;
    bool isPreviousDirLeft;
    public AudioClip se;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        child = transform.Find("Dir").transform;
        playerTr = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    // Use this for initialization
    public void OnEnable()
    {
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
                if (!isPreviousDirLeft)
                    rotateSpeed+=0.04f;
                else
                    rotateSpeed = 0;
                child.Rotate(0, 0, -rotateSpeed);
                isPreviousDirLeft = false;
            }
            else if (targetDir.x < 0)
            {
                if (isPreviousDirLeft)
                    rotateSpeed+=0.04f;
                else
                    rotateSpeed = 0;
                child.Rotate(0, 0, rotateSpeed);
                isPreviousDirLeft = true;
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
            explosion.transform.position = transform.position;
            explosion.SetActive(true);
            explosion.SendMessage("Bomb");
            GameSystem.instance.PlaySE(se);
            gameObject.SetActive(false);
        }
        else if (collision.gameObject.CompareTag("Boss"))
        {
            boss.GetComponent<Boss02>().Damaged();
            explosion.transform.position = transform.position;
            explosion.SetActive(true);
            explosion.SendMessage("Bomb");
            GameSystem.instance.PlaySE(se);
            gameObject.SetActive(false);
        }
    }
}
