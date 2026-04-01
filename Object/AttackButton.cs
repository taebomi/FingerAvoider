using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackButton : MonoBehaviour
{
    public GameObject tower;
    public float reloadTime;
    bool canFire;
    public Sprite[] sp;
    public AudioClip se;
    SpriteRenderer sr;
    GamePlay gp;
    public GameObject bulletPrefab;
    GameObject[] bullet;
    public float speed;
    int n = 0;
    private void Awake()
    {
        canFire = true;
        sr = GetComponent<SpriteRenderer>();
        bullet = new GameObject[2];
        gp = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GamePlay>();
        bullet[0] = Instantiate(bulletPrefab, new Vector3(transform.position.x, transform.position.y + 3, 3f), Quaternion.identity) as GameObject;
        bullet[1] = Instantiate(bulletPrefab, new Vector3(transform.position.x, transform.position.y + 3, 3f), Quaternion.identity) as GameObject;
        bullet[0].GetComponent<BulletScript>().speed = speed;
        bullet[0].SetActive(false);
        bullet[1].GetComponent<BulletScript>().speed = speed;
        bullet[1].SetActive(false);
    }
    void Fire()
    {
        if (n == 2)
            n = 0;
        bullet[n].SetActive(true);
        n++;
        Invoke("ChangeFireState", reloadTime);
    }
    void ChangeFireState()
    {
        canFire = true;
        sr.sprite = sp[0];
    }
    private void OnTriggerStay2D(Collider2D coll)
    {
        if (gp.playing)
        {
            if (canFire)
            {
                if (coll.gameObject.CompareTag("Player"))
                {
                    sr.sprite = sp[1];
                    canFire = false;
                    GameSystem.instance.PlaySE(se);
                    Fire();
                }
            }
        }
    }
}
