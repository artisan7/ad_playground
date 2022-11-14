using System;
using UnityEngine;
using GoogleMobileAds.Api;

public class RewardedInterstitialAdController : MonoBehaviour
{
    public RewardedInterstitialAd rewardedInterstitialAd { get; private set; }

    private void Start()
    {
        // Preload the ad
        RequestRewardedInterstitial();
    }

    private void RequestRewardedInterstitial()
    {
        // These units are configured to always serve test rewarded interstitial ads for android and ios respectively.
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-3940256099942544/5354046379";
#elif UNITY_IOS
        string adUnitId = "ca-app-pub-3940256099942544/6978759866";
#else
        string adUnitId = "unexpected_platform"
#endif

        // Clean up previous ad before creating a new one to avoid memory leak
        if (rewardedInterstitialAd != null)
            rewardedInterstitialAd.Destroy();

        // create empty add request
        AdRequest request = new AdRequest.Builder().Build();

        // load rewarded ad using request
        RewardedInterstitialAd.LoadAd(adUnitId, request, HandleAdLoaded);
    }

    // This function is attached and called when certain buttons are pressed
    public void HandleShowRewardedInterstitial()
    {
        if (rewardedInterstitialAd != null)
            rewardedInterstitialAd.Show(HandleUserEarnedReward);
    }

    #region Interstitial callback handlers
    public void HandleAdLoaded(RewardedInterstitialAd ad, AdFailedToLoadEventArgs args)
    {
        if (args == null)
        {
            Debug.Log("Interstitial Rewarded Ad Loaded");
            rewardedInterstitialAd = ad;

            // Register ad events
            rewardedInterstitialAd.OnAdFailedToPresentFullScreenContent += HandleAdFailedToShow;
            rewardedInterstitialAd.OnAdDidPresentFullScreenContent += HandleAdOpening;
            rewardedInterstitialAd.OnAdDidDismissFullScreenContent += HandleAdClosed;
        }
    }

    public void HandleAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        DebugTextArea.Instance.Log("Interstitial Rewarded Ad Load Failed:" + args.LoadAdError.GetMessage());
    }

    public void HandleAdOpening(object sender, EventArgs args)
    {
        Debug.Log("Interstitial Rewarded Ad Shown");
    }

    public void HandleAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        DebugTextArea.Instance.Log("Interstitial Rewarded Ad Failed To Show: " + args.AdError.GetMessage());
    }

    public void HandleAdClosed(object sender, EventArgs args)
    {
        Debug.Log("Interstitial Rewarded Ad Closed");

        // Preload new ad after closing current ad
        RequestRewardedInterstitial();
    }

    public void HandleUserEarnedReward(Reward reward)
    {
        string type = reward.Type;
        double amount = reward.Amount;
        Debug.Log("User earned reward: " + amount.ToString() + " " + type);
    }

    #endregion
}
