using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss01_GATCHA : MonoBehaviour
{
    PolygonCollider2D[] pc;
    // Use this for initialization
    void Start()
    {
        pc = GetComponents<PolygonCollider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GamePlay>().GameOver();

        }
    }
    void Change0()
    {
        pc[0].enabled = true;
        pc[1].enabled = false;
        pc[2].enabled = false;
    }
    void Change1()
    {
        pc[0].enabled = false;
        pc[1].enabled = true;
        pc[2].enabled = false;
    }
    void Change2()
    {
        pc[0].enabled = false;
        pc[1].enabled = false;
        pc[2].enabled = true;
    }
}
