using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using ItsHarshdeep.LoadingScene.Controller;
using System.Text;

public class GameSystem : Singleton<GameSystem>
{
    /* PlayerPrfs 저장 관련
     * PlayTime     플레이 횟수    Int     0일땐 처음 킴
     * Language     언어           Int     0은 KR, 1은 ENG
     * Character    캐릭터         Int     0은 기본, ~~
     * ClearTime1-1 클리어시간     float   0은 클리어 안함
     * ClearMedal1-1클리어메달     Int     0 노메달, 1 동, 2 은, 3 금, 4 개발자
     * ClearNumber1-1클리어횟수    Int     5회까지 돈 지급
     * ClearNumber  클리어개수     Int     ClearNumber + 1만큼 레벨 언락
     * BGM          BGM 크기       float   0 무음, 1 최대
     * SE           SE  크기       float   위와 동일
     * Sensivity    감도           float   0.5 ~ 1.5까지
     * DeadNumber   죽은 횟수      Int     죽을때마다 +1
     * EarnGold     누적골드       int     0
     * Gold         골드           int     0
     * Story        스토리         float   0
     * LastStage    마지막스테이지 int     0과 1은 1스테이지, 나머지 그대로
     */
    public bool isKorean = true;
    public int storyNum = 0;
    public int stageNum;
    public int levelNum;
    public string stageInfo;
    public bool isGame;
    AudioSource[] ase;
    public Font font;
    float bgmTime;
    public int sceneDeapth = 1;
    GameObject quitScreen;
    void Awake()
    {
        isGame = false;
        //PlayerPrefs.SetInt("Story", 0);
        DontDestroyOnLoad(gameObject);                                       // 파괴 안함
        ase = GetComponents<AudioSource>();                                   // 효과음 여기서 재생
        if (PlayerPrefs.GetInt("PlayTime") == 0)                    // 게임을 최초 실행 시
        {
            if (Application.systemLanguage.ToString() == "Korean")
                PlayerPrefs.SetInt("Language", 0);
            else
                PlayerPrefs.SetInt("Language", 1);
            for (int i = 1; i < 13; i++)
            {
                for(int j=1;j<26;j++)
                PlayerPrefs.SetFloat("ClearTime" + i + "-" + j, 123456789);
            }
            PlayerPrefs.SetFloat("SE", 1);
            PlayerPrefs.SetFloat("BGM", 1);
            PlayerPrefs.SetFloat("Sensivity", 1);
        }
        if (PlayerPrefs.GetInt("Language") == 1) {
            isKorean = false;
        }
        font = Resources.Load("Font/slkscr") as Font;
        ase[0].volume = PlayerPrefs.GetFloat("SE");
        ase[1].volume = PlayerPrefs.GetFloat("BGM");
        stageNum = 1;
        levelNum = 1;
    }
    public void Quit()
    {
        if(sceneDeapth == 1)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterScript>().StopSpeed();
            quitScreen = GameObject.FindGameObjectWithTag("QuitScreen").transform.Find("Screen").gameObject;
            quitScreen.SetActive(true);
        }
        else if(sceneDeapth == 2)
        {
            LoadScene("MainScene");
            sceneDeapth = 1;
        }
    }
    public bool CheckBGMPlaying()
    {
        return ase[1].isPlaying;
    }
    public void UpdateStageInfo()
    {
        stageInfo = stageNum + "-" + levelNum;
    }
    public void LoadScene(string sceneName)                         // 씬 로드
    {
        SceneController.LoadLevel(sceneName);
    }
    public void LoadScene(string sceneName,float waitingTime)                         // 시간 후 씬 로드
    {
        SceneController.LoadLevel(sceneName,waitingTime);
    }
    public void LoadPreviousScene(float waitingTime)
    {
        SceneController.LoadPreviousScene(waitingTime);
    }
    public void PlaySE(AudioClip se)                                // 효과음 재생 함수
    {
        ase[0].PlayOneShot(se);
    }
    public void StopBGM()
    {
        bgmTime = ase[1].time;
        ase[1].Stop();
    }
    public void PlayBGM()
    {
        ChangeBGMVolume2(PlayerPrefs.GetFloat("BGM"));
        ase[1].time = bgmTime;
        ase[1].Play();
    }
    public void PlayBGM(AudioClip bgm)
    {
        ase[1].clip = bgm;
        ase[1].Play();
    }
    public void PlayBGM(AudioClip bgm,float t)
    {
        ase[1].clip = bgm;
        ase[1].time = t;
        ase[1].Play();
    }
    public void ChangeBGMVolume(float n)
    {
        ase[1].volume = n;
        PlayerPrefs.SetFloat("BGM", n);
    }
    public void ChangeBGMVolume2(float n)
    {
        ase[1].volume = n;
    }
    public void ChangeSEVolume(float n)
    {
        ase[0].volume = n;
        PlayerPrefs.SetFloat("SE", n);
    }
    public void ChangeSensivity(float n)
    {
        PlayerPrefs.SetFloat("Sensivity", n);
    }
    public string GetBGMName()
    {
        if (ase[1].clip != null)
            return ase[1].clip.name;
        else
            return null;
    }
}