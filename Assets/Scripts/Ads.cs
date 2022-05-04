using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class Ads : MonoBehaviour
{
    private BannerView _bannerView;
    private InterstitialAd _interstitial;

    void Awake()
    {
        MobileAds.Initialize(initStatus => { });
        RequestBanner();
    }

    private void RequestBanner()
    {
#if UNITY_ANDROID
            string adUnitId = "ca-app-pub-3940256099942544/6300978111";
#elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/2934735716";
#else
            string adUnitId = "unexpected_platform";
#endif

        // Create a 320x50 banner at the top of the screen.
        _bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Top);
    }
    
    private void RequestInterstitial()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-3940256099942544/1033173712";
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
        string adUnitId = "unexpected_platform";
#endif

        // Initialize an InterstitialAd.
        _interstitial = new InterstitialAd(adUnitId);
        
        AdRequest request = new AdRequest.Builder().Build();
        _interstitial.LoadAd(request);
    }
}
