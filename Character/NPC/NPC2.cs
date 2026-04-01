using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC2 : MonoBehaviour
{
    public Text textNPC;
    public GameObject NPCDialoge;
    Image backImage = null;
    Transform playerTr;
    Animator ani;
    bool isSpeak;
    int previousActNum2;
    string[,] speech = { { "날 구해줘서 고마워!" , "Thank you for saving me!" }, { "친구들을 구해줘!", "please save friends!" }
        ,{ "집을 고쳐줄게! 조금만 기다려줘!","I'll repair house! please wait!"} };
    void Awake()
    {
        if (PlayerPrefs.GetFloat("Story") < 2)
        {
            gameObject.SetActive(false);
        }
        else
        {
            ani = GetComponent<Animator>();
            playerTr = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            backImage = NPCDialoge.transform.Find("Image").GetComponent<Image>();
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
            }
            yield return new WaitForSeconds(0.2f);
        }
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
        int n = Random.Range(0, 3);
        while (n == previousActNum2)
        {
            n = Random.Range(0, 3);
        }
        previousActNum2 = n;
        if (GameSystem.instance.isKorean)
            textNPC.text = speech[n, 0];
        else
            textNPC.text = speech[n, 1];
        if (n == 0)
        {
            backImage.rectTransform.sizeDelta = new Vector2(300f, 70f);
            textNPC.rectTransform.sizeDelta = new Vector2(280f, 65f);
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
        yield return new WaitForSeconds(5.0f);
        NPCDialoge.SetActive(false);
        isSpeak = false;
    }
}
