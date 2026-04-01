using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class StageSystem : MonoBehaviour
{
    int stageNum;
    int maxStage = 2;
    public GameObject nextBtn;
    public GameObject prevBtn;
    public AudioClip se;
    public AudioClip[] bgm;
    int medalNum;
    int clearNum;
    bool isAllCleared;
    void Start()
    {
        stageNum = PlayerPrefs.GetInt("LastStage");
        if (stageNum < 1)
        {

        }
        else if (stageNum < 2)
        {
            Camera.main.transform.position = new Vector3(71.1f, 0, -10f);
        }

        if (GameSystem.instance.GetBGMName() != bgm[stageNum].name || !GameSystem.instance.CheckBGMPlaying())
            GameSystem.instance.PlayBGM(bgm[stageNum]);

        if (stageNum == 0)
            prevBtn.SetActive(false);
        if (stageNum == maxStage - 1)
            nextBtn.SetActive(false);
        CheckStageLeaderboard();
    }
    public void GoNextStage()
    {
        prevBtn.SetActive(false);
        nextBtn.SetActive(false);
        stageNum++;
        GameSystem.instance.PlaySE(se);
        Camera.main.transform.DOMoveX(Camera.main.transform.position.x + 71.1f, 0.6f).SetEase(Ease.InOutSine).OnComplete(() => { Complete(); });
    }
    public void GoPrevStage()
    {
        prevBtn.SetActive(false);
        nextBtn.SetActive(false);
        GameSystem.instance.PlaySE(se);
        stageNum--;
        Camera.main.transform.DOMoveX(Camera.main.transform.position.x - 71.1f, 0.6f).SetEase(Ease.InOutSine).OnComplete(() => { Complete(); });
    }
    public void Complete()
    {
        if (maxStage - 1 != stageNum)
            nextBtn.SetActive(true);
        if (stageNum > 0)
            prevBtn.SetActive(true);
        GameSystem.instance.PlayBGM(bgm[stageNum]);
        PlayerPrefs.SetInt("LastStage", stageNum);
        CheckStageLeaderboard();
    }
    void CheckStageLeaderboard()
    {
        if (PlayerPrefs.GetInt("ClearNumber") > 49)
            GPGSManager.instance.UnlockAchevement("CgkIj-CF7q8UEAIQGA");
        else if(PlayerPrefs.GetInt("ClearNumber")>24)
             GPGSManager.instance.UnlockAchevement("CgkIj-CF7q8UEAIQBw");
        
        clearNum = 0;
        medalNum = 4;
        isAllCleared = true;
        int stagenum = stageNum + 1;
        for (int i = 1; i < 26; i++)
        {
            if (PlayerPrefs.GetInt("ClearNumber"+stagenum +"-" + i) == 0)
            {
                isAllCleared = false;
                break;
            }
            else
            {
                if (PlayerPrefs.GetInt("ClearNumber" + stagenum + "-" + i) > 4)
                    clearNum++;
                if (PlayerPrefs.GetInt("ClearMedal" + stagenum + "-" + i) == 4)
                {
                }
                else if (PlayerPrefs.GetInt("ClearMedal" + stagenum + "-" + i) == 3)
                {
                    if (medalNum > 3)
                        medalNum = 3;
                }
                else if (PlayerPrefs.GetInt("ClearMedal" + stagenum + "-" + i) == 2)
                {
                    if (medalNum > 2)
                        medalNum = 2;
                }
                else if (PlayerPrefs.GetInt("ClearMedal" + stagenum + "-" + i) == 1)
                {
                    if (medalNum > 1)
                        medalNum = 1;
                }
                else if (PlayerPrefs.GetInt("ClearMedal" + stagenum + "-" + i) == 0)
                {
                    if (medalNum > 0)
                        medalNum = 0;
                }
            }
        }
        if (isAllCleared)
        {
            if (stagenum == 1)
            {
                if (clearNum == 25)
                    GPGSManager.instance.UnlockAchevement("CgkIj-CF7q8UEAIQCA");
                if (medalNum == 1)
                    GPGSManager.instance.UnlockAchevement("CgkIj-CF7q8UEAIQFA");
                else if(medalNum==2)
                    GPGSManager.instance.UnlockAchevement("CgkIj-CF7q8UEAIQFQ");
                else if(medalNum==3)
                    GPGSManager.instance.UnlockAchevement("CgkIj-CF7q8UEAIQFg");
                else if(medalNum==4)
                    GPGSManager.instance.UnlockAchevement("CgkIj-CF7q8UEAIQFw");

            }
            else if (stagenum == 2)
            {
                if (clearNum == 25)
                    GPGSManager.instance.UnlockAchevement("CgkIj-CF7q8UEAIQGQ");
                if (medalNum == 1)
                    GPGSManager.instance.UnlockAchevement("CgkIj-CF7q8UEAIQGg");
                else if (medalNum == 2)
                    GPGSManager.instance.UnlockAchevement("CgkIj-CF7q8UEAIQGw");
                else if (medalNum == 3)
                    GPGSManager.instance.UnlockAchevement("CgkIj-CF7q8UEAIQHA");
                else if (medalNum == 4)
                    GPGSManager.instance.UnlockAchevement("CgkIj-CF7q8UEAIQHQ");
            }
        }
    }
    public void StageSelect(int n1, int n2)
    {
        GameSystem.instance.stageNum = n1;
        GameSystem.instance.levelNum = n2;
        GameSystem.instance.LoadScene("StageScene");
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            GameSystem.instance.sceneDeapth = 2;
            GameSystem.instance.Quit();
        }
    }
    public void Back()
    {
        GameSystem.instance.sceneDeapth = 1;
        GameSystem.instance.LoadScene("MainScene");
    }
}
