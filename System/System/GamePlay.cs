using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GamePlay : MonoBehaviour
{
    Text timer;
    float time;
    GameObject medalObject;
    public Sprite[] medals;
    int medalNumber;
    int level;
    public bool playing;
    CharacterScript cs;
    public GameObject bg;
    public Sprite[] bgs;
    public GameObject pauseScreen;
    public GameObject GameOverScreen;
    public GameObject GameClearScreen;
    public Text clearTimeText;
    public Text topRecordText;
    public Text goldText;
    public GameObject medalObjectScreen;
    GameObject stage;
    public GameObject finish;
    public AudioClip[] bgm;
    public GameObject loadingImage;
    float[,] startFinishPos = {
        { -26.1f, 0.4f, 24.3f, -0.7f } , {-30.6f,-4.9f,29.7f,-5.6f }, {-26.2f,9.2f ,24.2f,-12f}, { -28.1f,8.7f,12.8f,-0.4f} , {0f,-10f,-50f,50f },
        { -26.1f, 0.4f, 24.3f, -0.2f } ,{ -26.7f, 1.6f, 26.6f, -0.2f },{ 27f, -10.9f,-18.3f, 11.8f } , {23.64f,1.2f,-23.48f,0f }, { 22.5f,-8.6f,-23f,-10f} ,
        {23.8f,11.6f,-24.5f,-10.4f }, {24.2f,-9.6f,-24.2f,-9.8f }, {-24.2f,-9.6f,24.2f,-9.8f } , {30f,-7.8f,30f,1.4f } , {22.6f,1.4f,-50f,0f },
        {14,2.4f,-12.6f,-4f }, {-25.7f,1.4f,25.6f,0.3f }, {-20.6f,1,21.4f,0.9f }, {-25.4f,-5.6f,-24.6f,9.2f } , {-29.8f,0.2f,30.2f,0.1f },
        { -26.1f, 0.4f, 24.3f, -0.7f }, {-23.6f,-5.3f,27.7f,4.8f }, {-25f,10.9f,25.3f,-3f }, {-27.3f,-4.6f,28.9f,-5.7f } ,{0f,-13f,-60f,0f },
        { -26.8f,-8.9f,22.6f,6.5f}, {-16.5f,0.9f,16.1f,-0.2f }, {0,0f,28,0f } , {8.8f,14f,-14.8f,14.2f }, {0,0,-50,-50 },
        {28.3f,3.5f,-28.34f,2.1f }, {26.67f,4.27f,0,-13.8f } , {0.7f,-1f,16.3f,-1.7f }, {-17.7f,1.2f,17.24f,0 }, {-28.1f,11.9f,29.1f,-13.3f },
        {-11.1f,-13.4f,8.2f,16.4f }, {-30f,-3.7f,29.7f,-4.5f } , {-28.5f,6.2f,28.1f,-9.9f } , {-19.7f,-9.2f,19.4f,-10.1f }, {22.6f,1.4f,-50f,0f },
        {-29.2f,-9.6f,27.3f,11f }, {14.4f,-14.4f,1.7f,1.1f } , {-33.5f,0f,33.3f,0f } , {25.5f,1.3f,-25.7f,0 }, { -26.6f,-1.54f,-26.7f,-14.54f},
        {30f,-0.2f,-30f,0f}, {31,0.5f,-31,0 }, {31f,14.2f,-31f,14f }, {-26,-6,24,-6.5f }, {0,-10,0,-100 }
    };
    float[,] finishTime =
    {
        // 1 스테이지
        {0.09f,0.15f,0.2f, 0.4f }, {0.52f,0.8f,1.2f,1.6f }, {0.45f,0.85f,1.35f,2f }, {0.87f,1.2f,1.8f,2.5f }, {30.5f,31f,33f,35f },
        {0.37f,0.8f,1.3f,1.8f }, { 0.98f,1.4f,2.2f,3f }, {2.89f,3.5f,4.5f,5.5f }, {1.31f,1.8f,2.4f,3f }, {1.01f,1.4f,2.1f,2.8f },
        {1.81f,2.5f,3,4}, {2.5f,3,4,5 }, {2.83f,3.5f,4,5 }, {6.87f,8f,11,15 }, {53,54,55,56 },
        {3.69f,5.5f,7,9.5f }, {2.1f,3,4,5 }, {2.73f,4.5f,6.5f,9 }, {20.8f,22f,23.5f,25f }, {1.25f,2.5f,3.5f,5 },
        {4.36f,5.5f,7f,9f }, {16f,20,25,29 }, {1.86f,2.5f,3.5f,4.5f }, {1.15f,2f,3,5 }, {58.74f,65,75,90 },
        // 2 스테이지
        {0.78f,1f,1.6f,2.3f }, {6.54f,7f,9,11 }, {9.5f,12,15,20}, {5.85f,6.5f,8,10 }, {45.5f,46,48,50 },
        {1.1f,2.3f,3.5f,5f }, {2.38f,3f,4f,5f }, {4.44f,5.5f,6.5f,7.5f }, {1.27f,1.6f,2.3f,3 }, {3.98f,5.5f,8f,12 },
        {1.6f,2.3f,3,4 }, {3.23f,4.5f,6,8 }, {2.88f,3.5f,4.5f,6 }, {6.14f,7.5f,9f,13f }, {106,107,108,109 },
        {9.61f,108f,12.5f,15f }, {6.8f,8f,10,12 }, {0.18f,0.4f,1,3 }, {9.93f,10.5f,11.5f,13 }, {6.25f,7f,8f,10 },
        {4.59f,5.5f,7f,10f }, {21.82f,22.5f,25f, 29f}, {11.66f,13,15.5f,19f }, {13.41f,16f,17,18 }, {136.48f,140,150, 170}
    };
    Color[] medalColor = { Color.yellow, Color.gray, new Color(1, 0.35f, 0) };
    Coroutine timerCoroutine;
    Coroutine medalCoroutine;
    public void Awake()
    {
        playing = false;
    }
    public void Start()
    {
        cs = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterScript>();
        timer = GameObject.FindGameObjectWithTag("Timer").GetComponent<Text>();
        medalObject = timer.transform.GetChild(0).gameObject;
        time = 0;
        medalNumber = 1;
        GameSystem.instance.UpdateStageInfo();
        level = 25 * (GameSystem.instance.stageNum - 1) + GameSystem.instance.levelNum - 1;
        cs.SetPosition(startFinishPos[level, 0], startFinishPos[level, 1]);
        cs.TrailClear();
        cs.bc.enabled = false;
        finish.transform.position = new Vector2(startFinishPos[level, 2], startFinishPos[level, 3]);
        StartCoroutine(CheckStart());
        if (GameSystem.instance.stageNum == 1)
        {
            bg.GetComponent<SpriteRenderer>().sprite = bgs[0];
            if (GameSystem.instance.levelNum == 15)
            {
                GameSystem.instance.StopBGM();
            }
            else if (GameSystem.instance.levelNum == 25)
            {
                if (GameSystem.instance.GetBGMName() != bgm[1].name || !GameSystem.instance.CheckBGMPlaying())
                    GameSystem.instance.PlayBGM(bgm[1]);
            }
            else
            {
                if (GameSystem.instance.GetBGMName() != bgm[0].name || !GameSystem.instance.CheckBGMPlaying())
                    GameSystem.instance.PlayBGM(bgm[0]);
            }
        }
        else if (GameSystem.instance.stageNum == 2)
        {
            bg.GetComponent<SpriteRenderer>().sprite = bgs[1];
            if (GameSystem.instance.levelNum == 15)
            {
                GameSystem.instance.StopBGM();
            }
            else if (GameSystem.instance.levelNum == 25)
            {
                GameSystem.instance.PlayBGM(bgm[3]);
            }
            else
            {
                if (GameSystem.instance.GetBGMName() != bgm[2].name || !GameSystem.instance.CheckBGMPlaying())
                    GameSystem.instance.PlayBGM(bgm[2]);
            }
        }
        stage = Instantiate(Resources.Load("Stage/" + GameSystem.instance.stageNum + "-" + GameSystem.instance.levelNum) as GameObject);

        AdmobAdsManager.instance.ReadyAd();
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Pause();
        }
    }
    public float GetTime()
    {
        return time;
    }
    public void SetFinishPos(float n1, float n2)
    {
        finish.transform.position = new Vector2(n1, n2);
    }
    public void Pause()
    {
        if (playing)
        {
            if (GameSystem.instance.levelNum == 15)
            {
                GameSystem.instance.StopBGM();
            }
            Time.timeScale = 0;
            pauseScreen.SetActive(true);
        }
    }
    public void Resume()
    {
        pauseScreen.SetActive(false);
        if (GameSystem.instance.levelNum == 15)
            GameSystem.instance.PlayBGM();
        Time.timeScale = 1;
    }
    public void Restart()
    {
        if (GameSystem.instance.levelNum == 15 || GameSystem.instance.levelNum == 25 || GameSystem.instance.levelNum == 5)
            GameSystem.instance.LoadScene(SceneManager.GetActiveScene().name);
        playing = false;
        StartCoroutine(Restart2());
    }
    IEnumerator Restart2()
    {
        loadingImage.SetActive(true);
        pauseScreen.SetActive(false);
        GameOverScreen.SetActive(false);
        GameClearScreen.SetActive(false);
        stage.SetActive(false);
        cs.Restart();
        StopCoroutine(timerCoroutine);
        StopCoroutine(medalCoroutine);
        time = 0;
        timer.text = time.ToString("N2");
        medalNumber = 1;
        cs.SetPosition(startFinishPos[level, 0], startFinishPos[level, 1]);
        cs.TrailClear();
        cs.bc.enabled = false;
        finish.transform.position = new Vector2(startFinishPos[level, 2], startFinishPos[level, 3]);
        medalObject.GetComponent<Image>().sprite = medals[1];
        timer.GetComponent<Outline>().effectColor = medalColor[0];
        if (GameSystem.instance.stageNum == 1)
        {
            if (GameSystem.instance.levelNum == 15)
            {
                GameSystem.instance.StopBGM();
            }
            else if (GameSystem.instance.levelNum == 25)
            {
                if (GameSystem.instance.GetBGMName() != bgm[1].name || !GameSystem.instance.CheckBGMPlaying())
                    GameSystem.instance.PlayBGM(bgm[1]);
            }
            else
            {
                if (GameSystem.instance.GetBGMName() != bgm[0].name || !GameSystem.instance.CheckBGMPlaying())
                    GameSystem.instance.PlayBGM(bgm[0]);
            }
        }
        else if (GameSystem.instance.stageNum == 2)
        {
            if (GameSystem.instance.levelNum == 15)
            {
                GameSystem.instance.StopBGM();
            }
            else if (GameSystem.instance.levelNum == 25)
            {
                GameSystem.instance.PlayBGM(bgm[3]);
            }
            else
            {
                if (GameSystem.instance.GetBGMName() != bgm[2].name || !GameSystem.instance.CheckBGMPlaying())
                    GameSystem.instance.PlayBGM(bgm[2]);
            }
        }
        yield return new WaitForSecondsRealtime(0.01f);
        Time.timeScale = 1;
        stage.SetActive(true);
        loadingImage.SetActive(false);
        cs.transform.gameObject.SetActive(true);
        medalObject.SetActive(true);
        cs.GameStart();
        StartCoroutine(CheckStart());
    }
    public void NextStage()
    {
        if (GameSystem.instance.levelNum != 25)
        {
            GameSystem.instance.levelNum += 1;
            GameSystem.instance.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    public void Back()
    {
        GameSystem.instance.LoadScene("StageSelectScene");
        Time.timeScale = 1;
    }
    public void GameOver()
    {
        if (playing)
        {
            playing = false;
            PlayerPrefs.SetInt("PlayTime", PlayerPrefs.GetInt("PlayTime") + 1);
            PlayerPrefs.SetInt("DeadNumber", PlayerPrefs.GetInt("DeadNumber") + 1);
            cs.GameOver();
            CheckAchevement();
            Invoke("GameOver2", 1f);
        }
    }
    void GameOver2()
    {
        GameOverScreen.SetActive(true);
        if (PlayerPrefs.GetInt("PlayTime") % 4 == 0)
        {
            AdmobAdsManager.instance.LoadAd();
        }
    }
    public void GameClear()
    {
        playing = false;
        cs.GameClear();
        Invoke("GameClear2", 1.5f);
        float highTime = PlayerPrefs.GetFloat("ClearTime" + GameSystem.instance.stageInfo);
        float timeSum = 0;
        clearTimeText.text = "Clear Time: " + timer.text;
        clearTimeText.color = new Color(0.33f, 1f, 0.3f);
        topRecordText.text = "Top Record: " + highTime.ToString("N2");
        if (highTime == 0 || highTime > time)
        {
            // 신기록 달성!
            highTime = time;
            clearTimeText.text = "New Record!!!";
            topRecordText.text = timer.text;
            clearTimeText.color = new Color(0.3f, 0.95f, 1f);
            PlayerPrefs.SetFloat("ClearTime" + GameSystem.instance.stageInfo, time);
        }
        medalObjectScreen.SetActive(true);
        if (highTime > finishTime[level, 3])
        {
            medalObjectScreen.SetActive(false);
            // 메달 획득 실패
        }
        else if (highTime > finishTime[level, 2])
        {
            PlayerPrefs.SetInt("ClearMedal" + GameSystem.instance.stageInfo, 1);
            medalObjectScreen.GetComponent<Image>().sprite = medals[3];
        }
        else if (highTime > finishTime[level, 1])
        {
            PlayerPrefs.SetInt("ClearMedal" + GameSystem.instance.stageInfo, 2);
            medalObjectScreen.GetComponent<Image>().sprite = medals[2];
        }
        else if (highTime > finishTime[level, 0])
        {
            PlayerPrefs.SetInt("ClearMedal" + GameSystem.instance.stageInfo, 3);
            medalObjectScreen.GetComponent<Image>().sprite = medals[1];
        }
        else
        {
            PlayerPrefs.SetInt("ClearMedal" + GameSystem.instance.stageInfo, 4);
            medalObjectScreen.GetComponent<Image>().sprite = medals[0];
        }
        if (GameSystem.instance.levelNum < 6)
        {
            for (int i = 1; i < 6; i++)
            {
                timeSum += PlayerPrefs.GetFloat("ClearTime" + GameSystem.instance.stageNum + "-" + i);
            }
        }
        else if (GameSystem.instance.levelNum < 11)
        {
            for (int i = 6; i < 11; i++)
            {
                timeSum += PlayerPrefs.GetFloat("ClearTime" + GameSystem.instance.stageNum + "-" + i);
            }
        }
        else if (GameSystem.instance.levelNum < 16)
        {
            for (int i = 11; i < 16; i++)
            {
                timeSum += PlayerPrefs.GetFloat("ClearTime" + GameSystem.instance.stageNum + "-" + i);
            }
        }
        else if (GameSystem.instance.levelNum < 21)
        {
            for (int i = 16; i < 21; i++)
            {
                timeSum += PlayerPrefs.GetFloat("ClearTime" + GameSystem.instance.stageNum + "-" + i);
            }
        }
        else
        {
            for (int i = 21; i < 25; i++)
            {
                timeSum += PlayerPrefs.GetFloat("ClearTime" + GameSystem.instance.stageNum + "-" + i);
            }
        }    // timeSum 구하기
        timeSum = timeSum * 1000;
        if (timeSum < 123456789)
            GPGSManager.instance.PostScore(timeSum);
        if (PlayerPrefs.GetInt("ClearNumber") == level)
        {
            PlayerPrefs.SetInt("ClearNumber", PlayerPrefs.GetInt("ClearNumber") + 1);
        }
        if (PlayerPrefs.GetInt("ClearNumber" + GameSystem.instance.stageInfo) < 6)
        {
            PlayerPrefs.SetInt("ClearNumber" + GameSystem.instance.stageInfo, PlayerPrefs.GetInt("ClearNumber" + GameSystem.instance.stageInfo) + 1);
            PlayerPrefs.SetInt("Gold", PlayerPrefs.GetInt("Gold") + (int)Mathf.Pow(1.2f, GameSystem.instance.stageNum));
            PlayerPrefs.SetInt("EarnGold", PlayerPrefs.GetInt("EarnGold") + (int)Mathf.Pow(1.2f, GameSystem.instance.stageNum));
            goldText.text = "Bonus Gold:    " + (int)Mathf.Pow(1.2f, GameSystem.instance.stageNum) + "G";
        }
        else
        {
            goldText.gameObject.SetActive(false);
        }
        PlayerPrefs.SetInt("PlayTime", PlayerPrefs.GetInt("PlayTime") + 1);
        CheckAchevement();
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
    void GameClear2()
    {
        GameClearScreen.SetActive(true);
        if (PlayerPrefs.GetInt("PlayTime") % 4 == 0)
        {
            AdmobAdsManager.instance.LoadAd();
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
                cs.bc.enabled = true;
                timerCoroutine = StartCoroutine(Timer());
                medalCoroutine = StartCoroutine(ChangeMedal());
                break;
            }
#endif
#if UNITY_EDITOR
            if (Input.GetMouseButton(1))
            {
                playing = true;
                cs.bc.enabled = true;
                timerCoroutine = StartCoroutine(Timer());
                medalCoroutine = StartCoroutine(ChangeMedal());
                break;
            }
#endif
            yield return null;
        }
    }
    IEnumerator ChangeMedal()
    {
    restartLabel:
        while (time <= finishTime[level, medalNumber])
        {
            yield return new WaitForSeconds(0.02f);
        }
        medalNumber++;
        if (medalNumber != 4)
        {
            timer.GetComponent<Outline>().effectColor = medalColor[medalNumber - 1];
            medalObject.GetComponent<Image>().sprite = medals[medalNumber];
            goto restartLabel;
        }
            medalObject.SetActive(false);
        timer.GetComponent<Outline>().effectColor = Color.black;
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
}
