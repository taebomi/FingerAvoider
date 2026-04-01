using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DodgeMode : MonoBehaviour {
    enum Difficulty { Normal, Hard,VeryHard}
    Difficulty dif;

    public GameObject ball;
    GameObject[] balls;

    int n = 0;
    public Text timer;
    float time;
    public bool playing;
    CharacterScript cs;
    public GameObject pauseScreen;
    public GameObject GameOverScreen;
    public GameObject newRecordText;
    public Text clearTimeText;
    public AudioClip bgm;
    private void Awake()
    {
        Time.timeScale = 0;
        cs = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterScript>();
        if (GameSystem.instance.levelNum == 1)
        {
            dif = Difficulty.Normal;
            balls = new GameObject[500];
            Vector3 pos;
            Quaternion rot;
            int n;
            for(int i = 0; i < 500; i++)
            {
                n = Random.Range(0, 4);
                if (n == 0)
                    pos = new Vector2(Random.Range(40f, 50f), Random.Range(-50f, 50f));
                else if (n == 1)
                    pos = new Vector2(Random.Range(-50f, -40f), Random.Range(-50f, 50f));
                else if (n == 2)
                    pos = new Vector2(Random.Range(-50f, 50f), Random.Range(40f, 50f));
                else
                    pos = new Vector2(Random.Range(-50f, 50f), Random.Range(-50f, -40f));
                rot = Quaternion.Euler(0, 0, Random.Range(0, 360f));
                balls[i] = Instantiate(ball,pos,rot) as GameObject;
                balls[i].GetComponent<SpriteRenderer>().color = new Color(1f, 0.5f, 0.9f);
                balls[i].SetActive(false);
            }
            GameSystem.instance.PlayBGM(bgm);
        }
        else if(GameSystem.instance.levelNum == 2)
        {
            dif = Difficulty.Hard;
        }
        else
        {
            dif = Difficulty.VeryHard;
        }
    }
    void Start () {
        switch (dif)
        {
            case Difficulty.Normal:
                for (n = 0; n < 100; n++)
                {
                    balls[n].SetActive(true);
                }
                StartCoroutine(NormalMode());
                StartCoroutine(CheckStart());
                break;
        }
    }
    IEnumerator NormalMode()
    {
        float k = 0;
        while (n < 500)
        {
            if (time > k)
            {
                balls[n].SetActive(true);
                n++;
                k += 0.5f;
            }
            yield return new WaitForSeconds(0.2f);
        }
    }
    IEnumerator CheckStart()
    {
        while (!playing)
        {
#if UNITY_ANDROID
            if (Input.touchCount == 1)
            {
                playing = true;
                Time.timeScale = 1;
                StartCoroutine(Timer());
                break;
            }
#endif
#if UNITY_EDITOR
            if (Input.GetMouseButton(1))
            {
                playing = true;
                Time.timeScale = 1;
                StartCoroutine(Timer());
                break;
            }
#endif
            yield return null;
        }
    }
    IEnumerator Timer()
    {
        while (playing)
        {
            time += Time.deltaTime;
            timer.text = time.ToString("N2");
            yield return null;
        }
    }
    void GameOver()
    {
        if (playing)
        {
            playing = false;
            PlayerPrefs.SetInt("PlayTime", PlayerPrefs.GetInt("PlayTime") + 1);
            cs.GameOver();
            CheckAchevement();
            clearTimeText.text = timer.text;
            float highTime = PlayerPrefs.GetFloat("DodgeNormal");
            if (highTime<time)
            {
                highTime = time;
                PlayerPrefs.SetFloat("DodgeNormal", time);
                newRecordText.SetActive(true);
            }
            GPGSManager.instance.PostScore(highTime*1000, "CgkIj-CF7q8UEAIQDQ");
            Invoke("GameOver2", 1f);
        }
    }
    void GameOver2()
    {
        GameOverScreen.SetActive(true);
        AdmobAdsManager.instance.LoadAd();
    }
    void CheckAchevement()
    {
        // 업적 확인
        if (PlayerPrefs.GetInt("PlayTime") > 999)
        {
            GPGSManager.instance.UnlockAchevement("CgkIj-CF7q8UEAIQDA");
        }
        else if (PlayerPrefs.GetInt("PlayTime") > 499)
        {
            GPGSManager.instance.UnlockAchevement("CgkIj-CF7q8UEAIQCw");
        }
        else if (PlayerPrefs.GetInt("PlayTime") > 99)
        {
            GPGSManager.instance.UnlockAchevement("CgkIj-CF7q8UEAIQCg");
        }
        else if (PlayerPrefs.GetInt("PlayTime") > 9)
        {
            GPGSManager.instance.UnlockAchevement("CgkIj-CF7q8UEAIQCQ");
        }
    }
    public void ViewLeaderBoard()
    {
        GPGSManager.instance.ShowLeaderBoard("CgkIj-CF7q8UEAIQDQ");
    }
    public void Pause()
    {
        if (playing)
        {
            Time.timeScale = 0;
            pauseScreen.SetActive(true);
        }
    }
    public void Resume()
    {
        pauseScreen.SetActive(false);
        Time.timeScale = 1;
    }
    public void Restart()
    {
        GameSystem.instance.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }
    public void Back()
    {
        GameSystem.instance.LoadScene("MainScene");
        Time.timeScale = 1;
    }
}
