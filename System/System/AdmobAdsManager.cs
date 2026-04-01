using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
public class AdmobAdsManager : Singleton<AdmobAdsManager> {
    
    InterstitialAd interstitial;
    private void Start()
    {
        string adUnitId = "ca-app-pub-2915301137740963/4263817533";
        interstitial = new InterstitialAd(adUnitId);
        AdRequest request = new AdRequest.Builder().Build();
        interstitial.LoadAd(request);
    }
    public void ReadyAd()
    {
        AdRequest request = new AdRequest.Builder().Build();
        interstitial.LoadAd(request);
    }
    public void LoadAd()
    {
        if (interstitial.IsLoaded())
        {
            interstitial.Show();
        }
    }
}
