using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Achevement_2_7 : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GPGSManager.instance.UnlockAchevement("CgkIj-CF7q8UEAIQDg");
        }
    }
}
