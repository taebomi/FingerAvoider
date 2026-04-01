using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionObstacle : MonoBehaviour {
    BoxCollider2D bc;
    Animator ani;
    private void Awake()
    {
        bc = GetComponent<BoxCollider2D>();
        ani = GetComponent<Animator>();
    }
    public void Bomb()
    {
        StartCoroutine(Bomb2());
    }
    IEnumerator Bomb2()
    {
        ani.Play("ExplosionAni", -1, 0f);
        bc.enabled = true;
        yield return null;
        bc.enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GamePlay>().GameOver();
        }
    }
}
