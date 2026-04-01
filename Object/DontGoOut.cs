using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontGoOut : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameObject.FindGameObjectWithTag("GameManager").SendMessage("GameOver");
        }
    }
}
