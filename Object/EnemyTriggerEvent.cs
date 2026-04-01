using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTriggerEvent : MonoBehaviour {
    object a;
    private void Start()
    {
        a = gameObject;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Monster"))
        {
            collision.gameObject.SendMessage("Event",a);
        }
    }
}
