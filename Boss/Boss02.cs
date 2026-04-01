using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss02 : MonoBehaviour
{
    bool isAct;
    public int hp;
    int firstHp;
    Animator ani;
    SpriteRenderer eyeSR;
    public Sprite[] eyeS;
    WindZone wz;
    public GameObject wind;
    public GameObject fly1;
    public GameObject fly2;
    public GameObject bombFly;
    GameObject magic;
    GameObject magicE;
    List<GameObject> fly1s;
    GameObject[] fly2s;
    PolygonCollider2D[] mouthPC;
    PolygonCollider2D[] wingPC;
    PolygonCollider2D[] legPC;
    Transform playerTr;
    Vector3 playerPos;
    public GameObject explosion;
    int previousAttack;
    int attackNum;
    public AudioClip[] se;
    private void Awake()
    {
        legPC = transform.GetChild(1).GetComponents<PolygonCollider2D>();
        wingPC  = transform.GetChild(2).GetComponents<PolygonCollider2D>();
        mouthPC = transform.GetChild(3).GetComponents<PolygonCollider2D>();
        eyeSR = transform.GetChild(0).GetComponent<SpriteRenderer>();
        magic = transform.GetChild(4).gameObject;
        magicE = transform.GetChild(5).gameObject;
        playerTr = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        ani = GetComponent<Animator>();
        wz = wind.GetComponent<WindZone>();
        fly1s = new List<GameObject>();
        fly2s = new GameObject[16];
        bombFly.SetActive(false);
        for(int i = 0; i < 200; i++)
        {
            fly1s.Add(Instantiate(fly1, new Vector3(transform.position.x,transform.position.y,1f), Quaternion.identity) as GameObject);
            fly1s[i].SetActive(false);
        }
        for (int i = 0; i < 16; i++)
        {
            fly2s[i] = Instantiate(fly2, new Vector3(transform.position.x, transform.position.y, -0.1f), Quaternion.identity) as GameObject;
            fly2s[i].SetActive(false);
        }
    }
    private void Start()
    {
        firstHp = hp;
        isAct = false;
        StartCoroutine(CheckStart());
    }
    IEnumerator ActiveBombFly()
    {
        GamePlay gp = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GamePlay>();
        while (gp.playing)
        {
            if (!bombFly.activeSelf)
            {
                bombFly.transform.position = new Vector3(Random.Range(-42f,42f),-30 , 0);
                bombFly.SetActive(true);
            }
            yield return new WaitForSeconds(1f);
        }
    }
    IEnumerator CheckStart()
    {
        GamePlay gp = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GamePlay>();
        while (!gp.playing)
        {
            yield return null;
        }
        StartCoroutine(CheckState());
        StartCoroutine(ActiveBombFly());
    }
    IEnumerator CheckState()
    {
        yield return new WaitForSeconds(Random.Range(0.5f, 3f));
        while (true)
        {
            if (!isAct)
            {
                Attack();
            }
            yield return new WaitForSeconds(0.2f);
        }
    }
    void Attack()
    {
        isAct = true;
        ani.SetInteger("State", 0);
        attackNum = Random.Range(0, 3);
        while (previousAttack == attackNum)
        {
            attackNum = Random.Range(0, 3);
        }
        if (attackNum == 0) {
            StartCoroutine(Attack1());
        }
        else if (attackNum == 1)
        {
            StartCoroutine(Attack2());
        }
        else if (attackNum == 2)
        {
            StartCoroutine(Attack3());
        }
        previousAttack = attackNum;
    }
    IEnumerator Attack1()
    {
        wind.SetActive(true);
        ani.SetInteger("State", 1);
        float power;
        if (hp > firstHp * 0.4f)
        {
            power = Random.Range(0.03f, 0.13f);
            for (float i = 0; i < power; i += 0.004f)
            {
                wz.ChangeSpeed(i);
                yield return new WaitForSeconds(0.05f);
            }
            wz.ChangeSpeed(power);
            yield return new WaitForSeconds(Random.Range(6f, 12f));
        }
        else
        {
            power = Random.Range(0.13f, 0.23f);
            for (float i = 0; i < power; i += 0.004f)
            {
                wz.ChangeSpeed(i);
                yield return new WaitForSeconds(0.05f);
            }
            wz.ChangeSpeed(power);
            yield return new WaitForSeconds(Random.Range(4f, 8f));
        }
        ani.SetInteger("State", 0);
        wind.SetActive(false);
        for (float i = power; i > 0; i -= 0.002f)
        {
            wz.ChangeSpeed(i);
            yield return new WaitForSeconds(0.05f);
        }
        wz.ChangeSpeed(0);
        yield return new WaitForSeconds(Random.Range(2f, 5f));
        isAct = false;
    }
    IEnumerator Attack2()
    {
        ani.SetInteger("State", 2);
        magic.SetActive(true);
        magicE.SetActive(true);
        GameSystem.instance.PlaySE(se[0]);
        yield return new WaitForSeconds(Random.Range(1.5f,2.3f));
        if (hp > firstHp * 0.4f)
        {
            for (int i = 0; i < 8; i++)
            {
                fly2s[i].transform.position = playerTr.position;
            }
            fly2s[0].transform.Translate(0.001f, 10, 1);
            fly2s[1].transform.Translate(6.32f,6.32f, 1);
            fly2s[2].transform.Translate(10, 0, 1);
            fly2s[3].transform.Translate(6.32f,-6.32f, 1);
            fly2s[4].transform.Translate(0, -10, 1);
            fly2s[5].transform.Translate(-6.32f,-6.32f, 1);
            fly2s[6].transform.Translate(-10,0.001f, 1);
            fly2s[7].transform.Translate(-6.32f,6.32f, 1);
            for (int i = 0; i < 8; i++)
            {
                fly2s[i].SetActive(true);
            }
        }
        else
        {
            for (int i = 0; i < 16; i++)
            {
                fly2s[i].transform.position = playerTr.position;
            }
            fly2s[0].transform.Translate(0f, 20, 1);
            fly2s[1].transform.Translate(7.65f, 18.47f, 1);
            fly2s[2].transform.Translate(14.14f, 14.14f, 1);
            fly2s[3].transform.Translate(18.47f, 7.65f, 1);
            fly2s[4].transform.Translate(20, 0, 1);
            fly2s[5].transform.Translate(18.47f, -7.65f, 1);
            fly2s[6].transform.Translate(14.14f, -14.14f, 1);
            fly2s[7].transform.Translate(7.65f, -18.47f, 1);
            fly2s[8].transform.Translate(0f, -20, 1);
            fly2s[9].transform.Translate(-7.65f, -18.47f, 1);
            fly2s[10].transform.Translate(-14.14f, -14.14f, 1);
            fly2s[11].transform.Translate(-18.47f, -7.65f, 1);
            fly2s[12].transform.Translate(-20, 0, 1);
            fly2s[13].transform.Translate(-18.47f, 7.65f, 1);
            fly2s[14].transform.Translate(-14.14f, 14.14f, 1);
            fly2s[15].transform.Translate(-7.65f, 18.47f, 1);
            for (int i = 0; i < 16; i++)
            {
                fly2s[i].SetActive(true);
            }
        }
        yield return new WaitForSeconds(8f);

        for (int i = 0; i < 16; i++)
        {
            fly2s[i].SetActive(false);
        }
        ani.SetInteger("State", 0);
        isAct = false;
    }
    IEnumerator Attack3()
    {
        ani.SetInteger("State", 3);
        yield return new WaitForSeconds(0.75f);
        if (hp > firstHp * 0.4f)
        {
            for (int i = 0; i < 50; i++)
            {
                fly1s[i].transform.position = new Vector3(transform.position.x,transform.position.y,1);
                fly1s[i].SetActive(true);
                GameSystem.instance.PlaySE(se[1]);
                yield return new WaitForSeconds(0.12f);
            }
            ani.SetInteger("State", 0);
            fly1s.Sort((g1, g2) => ((Vector2)g1.transform.position - new Vector2(0, -9)).sqrMagnitude.CompareTo(((Vector2)g2.transform.position - new Vector2(0, -9)).sqrMagnitude));
            yield return new WaitForSeconds(2f);
            for (int i = 49; i > -1; i--)
            {
                fly1s[i].GetComponent<Fly1Boss>().Move();
                yield return new WaitForSeconds(0.08f);
            }
        }
        else
        {
            for (int i = 0; i < 100; i++)
            {
                fly1s[i].transform.position = new Vector3(transform.position.x, transform.position.y, 1);
                fly1s[i].SetActive(true);
                yield return new WaitForSeconds(0.08f);
            }
            ani.SetInteger("State", 0);
            fly1s.Sort((g1, g2) => ((Vector2)g1.transform.position - new Vector2(0, -9)).sqrMagnitude.CompareTo(((Vector2)g2.transform.position - new Vector2(0, -9)).sqrMagnitude));
            yield return new WaitForSeconds(2f);
            if (Random.Range(0, 2) == 0)
            {
                for (int i = 99; i > -1; i--)
                {
                    fly1s[i].GetComponent<Fly1Boss>().Move();
                    yield return new WaitForSeconds(0.04f);
                }
            }
            else
            {
                for (int i = 99; i >79; i--)
                {
                    fly1s[i].GetComponent<Fly1Boss>().Move();
                    yield return new WaitForSeconds(0.04f);
                }
                yield return new WaitForSeconds(2f);
                for (int i = 79; i > 59; i--)
                {
                    fly1s[i].GetComponent<Fly1Boss>().Move();
                    yield return new WaitForSeconds(0.04f);
                }
                yield return new WaitForSeconds(2f);
                for (int i = 59; i > 39; i--)
                {
                    fly1s[i].GetComponent<Fly1Boss>().Move();
                    yield return new WaitForSeconds(0.04f);
                }
                yield return new WaitForSeconds(2f);
                for (int i = 39; i > 19; i--)
                {
                    fly1s[i].GetComponent<Fly1Boss>().Move();
                    yield return new WaitForSeconds(0.04f);
                }
                yield return new WaitForSeconds(2f);
                for (int i = 19; i > -1; i--)
                {
                    fly1s[i].GetComponent<Fly1Boss>().Move();
                    yield return new WaitForSeconds(0.04f);
                }
                yield return new WaitForSeconds(2f);
            }
        }
        ani.SetInteger("State", 0);
        yield return new WaitForSeconds(Random.Range(10f, 15f));
        isAct = false;
    }
    IEnumerator ChangeEyeColor()
    {
        eyeSR.sprite = eyeS[1];
        yield return new WaitForSeconds(0.25f);
        eyeSR.sprite = eyeS[0];
    }
    public void Damaged()
    {
        hp--;
        StartCoroutine(ChangeEyeColor());
        if (hp == 0)
        {
            StopAllCoroutines();
            StartCoroutine(Clear());
        }
    }
    IEnumerator Clear()
    {
        isAct = true;
        Invoke("StopAct", 3f);
        while (isAct)
        {
            transform.localPosition = transform.position + Random.insideUnitSphere * 1f;
            transform.position = new Vector3(transform.position.x, transform.position.y, -1f);
            yield return new WaitForFixedUpdate();
        }
        explosion.transform.position = gameObject.transform.position;
        explosion.SetActive(true);
        GameSystem.instance.PlaySE(se[2]);
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<GamePlay>().GameClear();
        gameObject.SetActive(false);
    }
    void StopAct()
    {
        isAct = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")){
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GamePlay>().GameOver();
        }
    }
    void Mouth1()
    {
        mouthPC[0].enabled = false;
        mouthPC[1].enabled = true;
    }
    void Mouth2()
    {
        mouthPC[1].enabled = false;
        mouthPC[2].enabled = true;
    }
    void Mouth3()
    {
        mouthPC[2].enabled = false;
        mouthPC[3].enabled = true;
    }
    void Mouth4()
    {
        mouthPC[3].enabled = false;
        mouthPC[4].enabled = true;
    }
    void Mouth5()
    {
        mouthPC[4].enabled = false;
        mouthPC[2].enabled = true;
    }
    void Mouth6()
    {
        mouthPC[2].enabled = false;
        mouthPC[0].enabled = true;
    }
    void Wing1()
    {
        wingPC[0].enabled = true;
    }
    void Wing2()
    {
        wingPC[0].enabled = false;
        wingPC[1].enabled = true;
    }
    void Wing3()
    {
        wingPC[1].enabled = false;
        wingPC[2].enabled = true;
    }
    void Wing4()
    {
        wingPC[2].enabled = false;
        wingPC[3].enabled = true;
    }
    void Wing5()
    {
        wingPC[3].enabled = false;
        wingPC[1].enabled = true;
    }
    void Wing6()
    {
        wingPC[1].enabled = false;
    }
    void Leg1()
    {
        legPC[0].enabled = false;
        legPC[1].enabled = true;
    }
    void Leg2()
    {
        legPC[1].enabled = false;
        legPC[2].enabled = true;
    }
    void Leg3()
    {
        legPC[2].enabled = false;
        legPC[3].enabled = true;
    }
    void Leg4()
    {
        legPC[3].enabled = false;
        legPC[4].enabled = true;
    }
    void Leg5()
    {
        legPC[4].enabled = false;
        legPC[5].enabled = true;
    }
    void Leg6()
    {
        legPC[5].enabled = false;
        legPC[6].enabled = true;
    }
    void Leg7()
    {
        legPC[6].enabled = false;
        legPC[7].enabled = true;
    }
    void Leg8()
    {
        legPC[7].enabled = false;
        legPC[2].enabled = true;
    }
    void Leg9()
    {
        legPC[2].enabled = false;
        legPC[8].enabled = true;
    }
    void Leg10()
    {
        legPC[8].enabled = false;
        legPC[3].enabled = true;
    }
    void Leg11()
    {
        legPC[3].enabled = false;
        legPC[2].enabled = true;
    }
    void Leg12()
    {
        legPC[2].enabled = false;
        legPC[1].enabled = true;
    }
    void Leg13()
    {
        legPC[1].enabled = false;
        legPC[0].enabled = true;
    }
    void Leg14(){
        magic.SetActive(false);
        magicE.SetActive(false);
        ani.SetInteger("State", 0);
    }
}
