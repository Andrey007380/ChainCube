using System;
using GoogleMobileAds.Api;
using UnityEngine;

public class Ads : MonoBehaviour
{
    private BannerView _bannerView;
    private InterstitialAd _interstitial;
    private int _reloadCounter = 0;

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
        _reloadCounter++;
        if (_reloadCounter % 10 == 0)
        {
            // Initialize an InterstitialAd.
            _interstitial = new InterstitialAd(adUnitId);

            AdRequest request = new AdRequest.Builder().Build();
            _interstitial.LoadAd(request);
        }
    }

    private void OnEnable()
    {
        TileCubeMover.OnShoot += RequestInterstitial;
        Score.OnNewRecordAchiew += RequestInterstitial;
    }

    private void OnDisable()
    {
        TileCubeMover.OnShoot -= RequestInterstitial;
        Score.OnNewRecordAchiew -= RequestInterstitial;
    }
}
