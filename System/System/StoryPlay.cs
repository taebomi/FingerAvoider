using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class StoryPlay : MonoBehaviour {
    public GameObject[] cut;
    public int[] num;
    Animator[] ani;
    bool canGoNext;
    Text t;
    Button b;
    int n = 1;
    private void Awake()
    {
        b = GameObject.FindGameObjectWithTag("Button").GetComponent<Button>();
        t= GameObject.FindGameObjectWithTag("Text").GetComponent<Text>();
        n = 1;
        canGoNext = true;
        Camera.main.transform.position = cut[0].transform.position;
        ani = new Animator[cut.Length];
        for(int i = 0; i < cut.Length-1; i++)
        {
            ani[i] = cut[i+1].GetComponent<Animator>();
            ani[i].speed = 0;
        }
        StartCoroutine(NextText());
    }
    public void GoNext()
    {
        if (n == cut.Length)
        {

            GameSystem.instance.isGame = false;
            GameSystem.instance.LoadPreviousScene(1f);

        }
        else
        {
            MoveCamera(cut[n], num[n]);
            canGoNext = false;
            b.gameObject.SetActive(false);
            t.gameObject.SetActive(false);
        }
    }
    void MoveCamera(GameObject g,int n)
    {
        Vector3 pos = new Vector3(g.transform.position.x, g.transform.position.y, -10);
        if (n == 0)
        {
            Camera.main.transform.position = pos;
            Invoke("CoolTime", 3f);
        }
        else if (n == 1)
        {
            Camera.main.transform.position = pos;
            PlayAni();
        }
        else if (n == 2)
        {
            Camera.main.transform.DOMove(pos, 1).SetEase(Ease.InOutSine).OnComplete(() => { PlayAni(); });
        }
    }
    void PlayAni()
    {
        ani[n - 1].speed = 1;
        n++;
        Invoke("CoolTime", 3f);
    }
    void CoolTime()
    {
        canGoNext = true;
        StartCoroutine(NextText());
    }
    IEnumerator NextText()
    {
        int n = 0;
        bool isbig = true;
        b.gameObject.SetActive(true);
        t.gameObject.SetActive(true);
        while (canGoNext)
        {
            t.color = new Color(0, 0, 0, n * 0.01f);
            if (n == 100)
                isbig = false;
            else if (n == 0)
                isbig = true;
            if (isbig)
                n++;
            else
                n--;
            yield return null;
        }
    }
}
