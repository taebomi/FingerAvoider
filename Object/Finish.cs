using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Finish : MonoBehaviour {
    public Sprite[] sp;
    GameObject player;
    public AudioClip[] se;
    bool first;
    private void Start()
    {
        first = true;
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")&& first&& GameObject.FindGameObjectWithTag("GameManager").GetComponent<GamePlay>().playing)
        {
            StartCoroutine(GameClear());
            first = false;
        }
    }
    IEnumerator GameClear()
    {
        GameSystem.instance.PlaySE(se[0]);
        GetComponent<SpriteRenderer>().sprite = sp[1];
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<GamePlay>().GameClear();
        yield return new WaitForSeconds(0.5f);
        player.transform.DOMove(new Vector3(transform.position.x - 0.1f, transform.position.y + 0.5f, player.transform.position.z), 0.75f);
        player.transform.DOScale(new Vector3(0.7f, 0.7f, 1f), 0.75f);
        yield return new WaitForSeconds(0.9f);
        GameSystem.instance.PlaySE(se[1]);
        GetComponent<SpriteRenderer>().sprite = sp[0];
        player.gameObject.SetActive(false);
        first = true;
    }
}
