using System;
using UnityEngine;
using GoogleMobileAds.Api;

public class InterstitialAdController : MonoBehaviour
{
    // This ad unit is configured to always serve test banner ads for android.
    private string androidAppId = "ca-app-pub-3212738706492790/6113697308";
    private InterstitialAd interstitialAd;

    private void Start()
    {
        // Preload the ad
        RequestInterstitial();
    }

    private void RequestInterstitial()
    {
        // Clean up previous ad before creating a new one to avoid memory leak
        if (interstitialAd != null)
            interstitialAd.Destroy();

        // Initialize interstitial ad
        interstitialAd = new InterstitialAd(androidAppId);

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
            Debug.Log("Please wait a few seconds for the Ad to Load.");
    }

    #region Interstitial callback handlers
    public void HandleAdLoaded(object sender, EventArgs args)
    {
        Debug.Log("Interstitial Ad Loaded");
    }

    public void HandleAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        Debug.Log("Interstitial Ad Load Failed:" + args.LoadAdError.GetMessage());
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
