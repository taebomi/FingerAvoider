using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPGSManager : Singleton<GPGSManager>
{
    bool isLogined = false;
    string[,] leaderboardName = 
    {
         {"CgkIj-CF7q8UEAIQAA","CgkIj-CF7q8UEAIQAg","CgkIj-CF7q8UEAIQAw","CgkIj-CF7q8UEAIQBA","CgkIj-CF7q8UEAIQBQ"}
        , {"CgkIj-CF7q8UEAIQDw","CgkIj-CF7q8UEAIQEA","CgkIj-CF7q8UEAIQEQ","CgkIj-CF7q8UEAIQEg","CgkIj-CF7q8UEAIQEw" }
    };
    public void Start()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.DebugLogEnabled = false;
        PlayGamesPlatform.Activate();
        Social.localUser.Authenticate((bool success) =>
        {
            if (success)
                isLogined = true;
#if UNITY_EDITOR
            isLogined = false;
            //GameSystem.instance.LoadScene("mainScene");
#elif UNITY_ANDROID
            GameSystem.instance.LoadScene("mainScene");
#endif
        });
    }
    public bool CheckLogin()
    {
        return isLogined;
    }
    public bool CheckNowLogin()
    {
        return Social.localUser.authenticated;
    }
    public void SignInOut()
    {
        if (Social.localUser.authenticated)
        {
            PlayGamesPlatform.Instance.SignOut();
            isLogined = false;
        }
        else
        {
            Social.localUser.Authenticate((bool success) =>
            {
                if (success)
                {
                    isLogined = true;
                }
            });
        }
    }
    public void UnlockAchevement(string s)
    {
        if(isLogined)
        Social.ReportProgress(s, 100.0f, (bool success) => {
        });
    }
    public void PostScore(float time)
    {
        if (isLogined)
            Social.ReportScore((long)time, leaderboardName[GameSystem.instance.stageNum-1,(GameSystem.instance.levelNum-1)/5], (bool success) => { });
    }
    public void PostScore(float time,string s)
    {
        if (isLogined)
            Social.ReportScore((long)time, s, (bool success) => { });
    }
    public void ShowLeaderBoard()
    {
        if(isLogined)
            Social.ShowLeaderboardUI();
    }
    public void ShowLeaderBoard(string s)
    {
        if (isLogined)
            PlayGamesPlatform.Instance.ShowLeaderboardUI(s);
    }
    public void ShowAchevement()
    {
        if (isLogined)
            Social.ShowAchievementsUI();
    }
}
