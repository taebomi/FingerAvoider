using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonOpenClose : MonoBehaviour {
    public GameObject[] doorObject;
    public Sprite[] sp;
    public AudioClip se;
    bool canChange = true;
    SpriteRenderer sr;
    GamePlay gp;
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        gp = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GamePlay>();
    }
    private void OnEnable()
    {
        canChange = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")&&canChange&&gp.playing)
        {
            canChange = false;
            GameSystem.instance.PlaySE(se);
            sr.sprite = sp[1];
            for (int i = 0; i < doorObject.Length; i++)
            {
                doorObject[i].SendMessage("Open");
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !canChange)
        {
            canChange = true;
            sr.sprite = sp[0];
        }
    }
}
