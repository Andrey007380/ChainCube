using System;
using GoogleMobileAds.Api;
using UnityEngine;

public class Ads : MonoBehaviour
{
    private BannerView _bannerView;
    private InterstitialAd _interstitial;
    private int _reloadCounter = 0;

    void Start()
    {

        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(initStatus => { });
        RequestBanner();
    }

    private void RequestInterstitial(Vector3 obj)
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

        // Called when an ad request has successfully loaded.
        this._interstitial.OnAdLoaded += HandleOnAdLoading;
        // Called when an ad request failed to load.
        this._interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        // Called when an ad is shown.
        this._interstitial.OnAdOpening += HandleOnAdOpening;
        // Called when the ad is closed.
        this._interstitial.OnAdClosed += HandleOnAdClosed;
            
        AdRequest request = new AdRequest.Builder().Build();
        _interstitial.LoadAd(request);
    }
    public void HandleOnAdLoading(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLoaded event received");
        _reloadCounter++;
        if (_reloadCounter % 10 == 0)
        {
            if (_interstitial.IsLoaded())
            {
                _interstitial.Show();
            }
        }
    }
    
    private void RequestBanner()
    {
        #if UNITY_ANDROID
                string adUnitId = "ca-app-pub-3940256099942544/1033173711";
        #elif UNITY_IPHONE
                    string adUnitId = "ca-app-pub-3940256099942544/2934735716";
        #else
                    string adUnitId = "unexpected_platform";
        #endif
        // Create a 320x50 banner at the top of the screen.
        _bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);
        
        // Called when an ad request has successfully loaded.
        _bannerView.OnAdLoaded += HandleOnAdLoaded;
        // Called when an ad request failed to load.
        _bannerView.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        // Called when an ad is clicked.
        _bannerView.OnAdOpening += HandleOnAdOpening;
        // Called when the user returned from the app after an ad click.
        _bannerView.OnAdClosed += HandleOnAdClosed;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        _bannerView.LoadAd(request);
    }
    
    
    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        // Load the banner with the request.
        MonoBehaviour.print("HandleAdLoaded event received");
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print("HandleFailedToReceiveAd eventx received with message: "
                            + args.LoadAdError);
    }

    public void HandleOnAdOpening(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpened event received");
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdClosed event received");
        _bannerView.Destroy();
        _interstitial.Destroy();
    }

    public void HandleOnAdLeavingApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLeavingApplication event received");
    }

    private void OnEnable()
    {
        Spawner.OnLose += RequestBanner;
        TileCubeMover.OnShoot += RequestInterstitial;
    }

    private void OnDisable()
    {
        Spawner.OnLose += RequestBanner;
        TileCubeMover.OnShoot -= RequestInterstitial;
    }
}
