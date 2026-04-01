using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss01 : MonoBehaviour
{
    Animator ani;
    Animator aniGatcha;
    Rigidbody2D rb;
    Transform playerTr;
    GameObject gatchaRope;
    GameObject gatcha;
    public GameObject ball;
    public GameObject ball2;
    public GameObject[] ballPos;
    GameObject[] balls;
    GameObject[] balls2;
    public GameObject explosion;
    int speed;
    public int firstHp;
    int hp;
    public AudioClip se;
    enum State
    {
        Stop, Move, Attack
    }
    State state;
    bool isAct = false;
    int previousStateNum;
    void Start()
    {
        hp = firstHp;
        speed = 10;
        isAct = false;
        previousStateNum = 0;
        state = State.Stop;
        rb = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        playerTr = GameObject.FindGameObjectWithTag("Player").transform;
        gatcha = transform.Find("P").transform.Find("GATCHA").gameObject;
        gatchaRope = gatcha.transform.Find("Rope").gameObject;
        aniGatcha = gatcha.GetComponent<Animator>();
        balls = new GameObject[15];
        balls2 = new GameObject[30];
        for (int i = 0; i < 12; i++)
        {
            balls[i] = Instantiate(ball, transform.position, Quaternion.identity) as GameObject;
            balls[i].GetComponent<SpriteRenderer>().color = new Color(1, 0.9f, 0.45f);
            balls[i].SetActive(false);
        }
        for (int i = 0; i < 30; i++)
        {
            balls2[i] = Instantiate(ball2, transform.position, Quaternion.identity) as GameObject;
            balls2[i].GetComponent<SpriteRenderer>().color = new Color(1, 0.45f, 0.45f);
            balls2[i].SetActive(false);
        }
        StartCoroutine(CheckStart());
    }
    IEnumerator CheckStart()
    {
        GamePlay gp = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GamePlay>();
        while (!gp.playing)
        {
            yield return null;
        }
        StartCoroutine(MoveCheck());
        StartCoroutine(CheckState());
    }
    void Damaged()
    {
        hp--;
        if (hp == 0)
        {
            StopAllCoroutines();
            StartCoroutine(Clear());
        }
    }
    void StopAct()
    {
        isAct = false;
    }
    IEnumerator Clear()
    {
        isAct = true;
        rb.linearVelocity = Vector2.zero;
        Invoke("StopAct", 3f);
        while (isAct)
        {
            transform.localPosition = transform.position + Random.insideUnitSphere * 1f;
            transform.position = new Vector3(transform.position.x, transform.position.y, -1f);
            yield return new WaitForFixedUpdate();
        }
        explosion.transform.position = gameObject.transform.position;
        explosion.SetActive(true);
        GameSystem.instance.PlaySE(se);
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<GamePlay>().GameClear();
        gameObject.SetActive(false);
    }
    IEnumerator Attack3()
    {
        rb.linearVelocity = Vector2.zero;
        ani.SetInteger("State", 1);
        int n;
        int k = 1;
        if (hp < firstHp * 0.4f)
            n = Random.Range(10, 20);
        else
            n = Random.Range(20, 30);
        for (int i = 0; i < n; i++)
        {
            if (k % 4 == 0)
                k = 1;
            balls2[i].transform.position = ballPos[k - 1].transform.position;
            balls2[i].transform.rotation = Quaternion.FromToRotation(Vector3.down, ballPos[k - 1].transform.position - playerTr.position);
            balls2[i].SetActive(true);
            balls2[i].GetComponent<CircleCollider2D>().isTrigger = true;
            balls2[i].SendMessage("Remove", 5);
            k++;
            yield return new WaitForSeconds(0.2f);
        }
        Invoke("ChangeState", Random.Range(4f, 8f));
    }

    IEnumerator Attack2()
    {
        if (firstHp * 0.4f > hp)
        {
            speed = Random.Range(15, 26);
        }
        else
        {
            speed = Random.Range(5, 16);
        }
        if (Random.Range(0, 2) == 0)
        {
            speed = speed * -1;
            if (transform.position.x > 29 && speed > 0)
                speed = speed * -1;
            else if (transform.position.x < -29 && speed < 0)
                speed = speed * -1;
        }
        rb.linearVelocity = Vector3.right * speed;

        ani.SetInteger("State", 1);
        int n;
        int k = 1;
        if (hp < firstHp * 0.4f)
            n = Random.Range(6, 12);
        else
            n = Random.Range(4, 8);
        for (int i = 0; i < n; i++)
        {
            if (k % 4 == 0)
                k = 1;
            balls[i].transform.position = ballPos[k - 1].transform.position;
            balls[i].transform.rotation = Quaternion.Euler(0, 0, Random.Range(120, 240));
            balls[i].SetActive(true);
            balls[i].SendMessage("Remove", 15);
            k++;
            yield return new WaitForSeconds(0.2f);
        }
        Invoke("ChangeState", Random.Range(8f, 18f));
    }
    IEnumerator Attack1()
    {
        rb.linearVelocity = Vector2.zero;
        ani.SetInteger("State", 2);
        yield return new WaitForSeconds(1.0f);
        aniGatcha.SetInteger("State", 1);
        yield return new WaitForSeconds(2f);
        Coroutine look = StartCoroutine(LookPlayerGatcha());
        yield return new WaitForSeconds(Random.Range(0.5f, 1.5f));
        StopCoroutine(look);
        yield return StartCoroutine(GoToPlayerGatcha());
        ani.SetInteger("State", 0);
        aniGatcha.SetInteger("State", 0);
        gatcha.transform.parent.transform.localPosition = Vector3.zero;
        gatcha.transform.rotation = Quaternion.Euler(0, 0, 0);
        gatchaRope.transform.localScale = new Vector3(0.34f, 1, 1f);
        gatchaRope.transform.localPosition = new Vector3(0.285f, 0, 0);
        Invoke("ChangeState", Random.Range(0.5f, 2f));
    }
    IEnumerator GoToPlayerGatcha()
    {
        float length = (playerTr.position - gatcha.transform.position).magnitude;
        float movelength = 0;
        while (true)
        {
            movelength += 0.3f;
            gatcha.transform.parent.transform.Translate(gatcha.transform.up * -0.3f);
            gatchaRope.transform.localScale += Vector3.up * 0.2f;
            if (length - 2.5f < movelength)
                break;
            yield return new WaitForFixedUpdate();
        }
        aniGatcha.SetInteger("State", 2);
        yield return new WaitForSeconds(1.5f);
        while (true)
        {
            movelength -= 0.3f;
            gatcha.transform.parent.transform.Translate(gatcha.transform.up * 0.3f);
            gatchaRope.transform.localScale -= Vector3.up * 0.2f;
            if (-3 > movelength)
                break;
            yield return new WaitForFixedUpdate();
        }
    }
    IEnumerator LookPlayerGatcha()
    {
        Vector3 direction;
        while (true)
        {
            direction = playerTr.position - gatcha.transform.position;
            gatcha.transform.rotation = Quaternion.Slerp(gatcha.transform.rotation, Quaternion.FromToRotation(Vector2.down, direction), Time.deltaTime * 5);
            yield return new WaitForFixedUpdate();
        }
    }
    IEnumerator CheckState()
    {
        while (true)
        {
            if (!isAct)
            {
                switch (state)
                {
                    case State.Stop:
                        Stop();
                        ani.SetInteger("State", 0);
                        break;
                    case State.Move:
                        ani.SetInteger("State", 1);
                        Move();
                        break;
                    case State.Attack:
                        Attack();
                        break;
                }
            }
            yield return new WaitForSeconds(0.2f);
        }
    }
    void ChangeState()
    {
        int n = Random.Range(0, 3);
        while (n == previousStateNum)
            n = Random.Range(0, 3);
        switch (n)
        {
            case 0:
                state = State.Stop;
                break;
            case 1:
                state = State.Move;
                break;
            case 2:
                state = State.Attack;
                break;
        }
        isAct = false;
    }
    void Attack()
    {
        isAct = true;
        int n = Random.Range(0, 100);
        if (n < 20)
        {
            StartCoroutine(Attack1());
        }
        else if (n < 60)
        {
            StartCoroutine(Attack2());
        }
        else
        {
            StartCoroutine(Attack3());
        }

    }
    void Stop()
    {
        isAct = true;
        rb.linearVelocity = Vector2.zero;
        Invoke("ChangeState", Random.Range(0.1f, 2f));
    }
    private void Move()
    {
        isAct = true;
        if (firstHp * 0.4f > hp)
        {
            speed = Random.Range(15, 26);
        }
        else
        {
            speed = Random.Range(5, 16);
        }
        if (Random.Range(0, 2) == 0)
        {
            speed = speed * -1;
            if (transform.position.x > 29 && speed > 0)
                speed = speed * -1;
            else if (transform.position.x < -29 && speed < 0)
                speed = speed * -1;
        }
        rb.linearVelocity = Vector3.right * speed;
        Invoke("ChangeState", Random.Range(1f, 4f));
    }
    IEnumerator MoveCheck()
    {
        while (true)
        {
            if ((transform.position.x > 29f&&speed>0) ||( transform.position.x < -29f&&speed<-1))
            {
                speed *= -1;
                rb.linearVelocity = Vector2.right * speed;
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
}
