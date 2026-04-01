using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class UnityAdsManager : MonoBehaviour {
    public Text text;
    public void ShowRewardedAd()
    {
        if (Advertisement.IsReady("rewardedVideo"))
        {
            var options = new ShowOptions { resultCallback = HandleShowResult };
            Advertisement.Show("rewardedVideo", options);
        }
    }

    private void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                PlayerPrefs.SetInt("Gold", PlayerPrefs.GetInt("Gold") + 3);
                PlayerPrefs.SetInt("EarnGold", PlayerPrefs.GetInt("EarnGold") + 3);
                if (GameSystem.instance.isKorean)
                    text.text = "감사합니다.";
                else
                    text.text = "Thank you!";
                break;
            case ShowResult.Skipped:
                if (GameSystem.instance.isKorean)
                    text.text = "끝까지 다 봐주세요 ㅠㅜ";
                else
                    text.text = "Please watch until the end of the ad. T-T";
                break;
            case ShowResult.Failed:
                if (GameSystem.instance.isKorean)
                    text.text = "오류 발생";
                else
                    text.text = "Error";
                break;
        }
    }
}
