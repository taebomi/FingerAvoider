using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class NPC1 : MonoBehaviour
{
    public GameObject invisibleScreen;
    public GameObject shouldOffScreen;
    public GameObject tutorialText;
    public Text textNPC;
    public GameObject NPCDialoge;
    public Canvas canvas;
    Image backImage;
    Transform playerTr;
    Animator ani;
    Rigidbody2D rb;
    bool isAct;
    bool isSpeak;
    int previousActNum;
    int previousActNum2;
    string[,] speech = { { "안녕!" , "Hi!" }, { "친구들을 구해줘!", "please save friends!" }, { "너도 같이 놀래? 재밌겠다~", "You wanna play too?\nIt'll be fun!" }
        ,{ "클리어 보너스 골드 획득은 레벨당 5번까지 가능해!","you can get Clear Bonus Gold up to 5 times per level!"} };
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        transform.parent.position = new Vector3(Random.Range(-28f, 28f), Random.Range(10f, -10f), 3f);
        ani = GetComponent<Animator>();
        backImage = NPCDialoge.transform.Find("Image").GetComponent<Image>();
        playerTr = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        isAct = false;
        isSpeak = false;
        previousActNum = 0;
        if (!GameSystem.instance.isKorean)
        {
            textNPC.font = GameSystem.instance.font;
        }
            if (PlayerPrefs.GetFloat("Story") == 0)
        {
            transform.parent.gameObject.transform.position = new Vector3(42f, 16.5f, 3);
            playerTr.gameObject.GetComponent<CharacterScript>().StopSpeed();
            Story0();
        }
        else
        {
            StartCoroutine(CheckState());
        }
    }
    IEnumerator CheckState()
    {
        float distance;
        while (true)
        {
            if (!isSpeak)
            {
                distance = (playerTr.position - transform.position).sqrMagnitude;
                if (distance < 35)
                {
                    isSpeak = true;
                    StartCoroutine(React());
                }
                else
                {
                    if (!isAct)
                    {
                        isAct = true;
                        StartCoroutine(RandomAct());
                    }
                }
            }
            yield return new WaitForSeconds(0.2f);
        }
    }
    IEnumerator RandomAct()
    {
        int n = Random.Range(0, 4);
        while (n == previousActNum)
        {
            n = Random.Range(0, 4);
        }
        previousActNum = n;
        float t;
        if (n == 0)     // Stop
        {
            t = Random.Range(0.5f, 2f);
            ani.SetInteger("State", 0);
            yield return new WaitForSeconds(t);
        }
        else if (n == 1)    // 제자리 뛰기
        {
            t = Random.Range(1f, 3f);
            ani.SetInteger("State", 1);
            yield return new WaitForSeconds(t);
        }
        else if (n == 2)        // 뒹굴뒹굴
        {
            t = Random.Range(2f, 4f);
            ani.SetInteger("State", 2);
            yield return new WaitForSeconds(t);
        }
        else                // 이동
        {
            t = Random.Range(3f, 8f);
            ani.SetInteger("State", 3);
            float n1 = Random.Range(-t, t);
            float n2 = Mathf.Sqrt(t * t - n1 * n1);
            if (n == 0)
                n2 = n2 * -1;
            if (Mathf.Abs(transform.position.x + n1) > 30f)
                n1 = n1 * -1;
            if (Mathf.Abs(transform.position.y + n2) > 19.5f)
                n2 = n2 * -1;
            rb.DOMove(new Vector2(transform.position.x + n1, transform.position.y + n2), t - 0.6f).SetEase(Ease.Linear).SetDelay(0.6f);
            yield return new WaitForSeconds(t);
        }
        ani.SetInteger("State", 0);
        yield return new WaitForSeconds(Random.Range(1, 3f));
        isAct = false;
    }
    IEnumerator React()
    {
        isSpeak = true;
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + 3, 0);
        if (pos.x > 30)
            pos.x = 27;
        else if (pos.x < -30)
            pos.x = -27;
        if (pos.y > 18)
            pos.y = 18;
        else if (pos.y < -18)
            pos.y = -18;
        NPCDialoge.transform.position = pos;
        NPCDialoge.SetActive(true);
        int n = Random.Range(0, 4);
        while (n == previousActNum2)
        {
            n = Random.Range(0, 4);
        }
        previousActNum2 = n;
        if (GameSystem.instance.isKorean)
            textNPC.text = speech[n, 0];
        else
            textNPC.text = speech[n, 1];
        if (n == 0)
        {
            backImage.rectTransform.sizeDelta = new Vector2(100f, 80f);
            textNPC.rectTransform.sizeDelta = new Vector2(80f, 65f);
        }
        else if (n == 1)
        {
            backImage.rectTransform.sizeDelta = new Vector2(300f, 70f);
            textNPC.rectTransform.sizeDelta = new Vector2(280f, 55f);
        }
        else if (n == 2)
        {
            backImage.rectTransform.sizeDelta = new Vector2(450f, 70f);
            textNPC.rectTransform.sizeDelta = new Vector2(430f, 55f);
        }
        else if (n == 3)
        {
            backImage.rectTransform.sizeDelta = new Vector2(600f, 90f);
            textNPC.rectTransform.sizeDelta = new Vector2(580f, 75f);
        }
        yield return new WaitForSeconds(5.0f);
        NPCDialoge.SetActive(false);
        isSpeak = false;
    }
    void Story0()
    {
        invisibleScreen.SetActive(true);
        ani.SetInteger("State", 3);
        transform.parent.transform.DOMove(new Vector3(27.3f, 6.6f, 3f),3f);
        Invoke("SpeakTutorial", 3f);
    }
    void SpeakTutorial()
    {
        tutorialText.SetActive(true);
        ani.SetInteger("State", 0);
        shouldOffScreen.SetActive(false);
        PlayerPrefs.SetFloat("Story", 1);
    }
}
