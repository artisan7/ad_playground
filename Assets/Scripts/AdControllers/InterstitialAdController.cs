using System;
using UnityEngine;
using GoogleMobileAds.Api;

public class InterstitialAdController : MonoBehaviour
{
    public InterstitialAd interstitialAd { get; private set; }

    private void Start()
    {
        // Preload the ad
        RequestInterstitial();
    }

    private void RequestInterstitial()
    {
        // These units are configured to always serve test interstitial ads for android and ios respectively.
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-3940256099942544/1033173712";
#elif UNITY_IOS
        string adUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
        string adUnitId = "unexpected_platform"
#endif

        // Clean up previous ad before creating a new one to avoid memory leak
        if (interstitialAd != null)
            interstitialAd.Destroy();

        // Initialize interstitial ad
        interstitialAd = new InterstitialAd(adUnitId);

        // Register ad events
        interstitialAd.OnAdLoaded += HandleAdLoaded;
        interstitialAd.OnAdFailedToLoad += HandleAdFailedToLoad;
        interstitialAd.OnAdOpening += HandleAdOpening;
        interstitialAd.OnAdClosed += HandleAdClosed;

        // create empty add request
        AdRequest request = new AdRequest.Builder().Build();

        // load interstitial ad using request
        interstitialAd.LoadAd(request);
    }

    // This function is attached and called when certain buttons are pressed
    public void HandleShowInterstitial()
    {
        if (interstitialAd.IsLoaded())
            interstitialAd.Show();
        else
            DebugTextArea.Instance.Log("Please wait a few seconds for the Ad to Load.");
    }

    #region Interstitial callback handlers
    public void HandleAdLoaded(object sender, EventArgs args)
    {
        Debug.Log("Interstitial Ad Loaded");
    }

    public void HandleAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        DebugTextArea.Instance.Log("Interstitial Ad Load Failed:" + args.LoadAdError.GetMessage());
    }

    public void HandleAdOpening(object sender, EventArgs args)
    {
        Debug.Log("Interstitial Ad Shown");
    }

    public void HandleAdClosed(object sender, EventArgs args)
    {
        Debug.Log("Interstitial Ad Closed");

        // Preload another ad
        RequestInterstitial();
    }

    #endregion
}
