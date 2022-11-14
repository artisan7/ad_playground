using System;
using UnityEngine;
using GoogleMobileAds.Api;

public class RewardedAdController : MonoBehaviour
{
    public RewardedAd rewardedAd { get; private set; }

    private void Start()
    {
        // Preload the ad
        RequestRewarded();
    }

    private void RequestRewarded()
    {
        // These units are configured to always serve test rewarded ads for android and ios respectively.
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-3940256099942544/5224354917";
#elif UNITY_IOS
        string adUnitId = "ca-app-pub-3940256099942544/1712485313";
#else
        string adUnitId = "unexpected_platform"
#endif

        // Clean up previous ad before creating a new one to avoid memory leak
        if (rewardedAd != null)
            rewardedAd.Destroy();

        // Initialize rewarded ad
        rewardedAd = new RewardedAd(adUnitId);

        // Register ad events
        rewardedAd.OnAdLoaded += HandleAdLoaded;
        rewardedAd.OnAdFailedToLoad += HandleAdFailedToLoad;
        rewardedAd.OnAdOpening += HandleAdOpening;
        rewardedAd.OnAdFailedToShow += HandleAdFailedToShow;
        rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        rewardedAd.OnAdClosed += HandleAdClosed;

        // create empty add request
        AdRequest request = new AdRequest.Builder().Build();

        // load rewarded ad using request
        rewardedAd.LoadAd(request);
    }

    // This function is attached and called when certain buttons are pressed
    public void HandleShowRewarded()
    {
        if (rewardedAd.IsLoaded())
            rewardedAd.Show();
    }

    #region Interstitial callback handlers
    public void HandleAdLoaded(object sender, EventArgs args)
    {
        Debug.Log("Rewarded Ad Loaded");
    }

    public void HandleAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        DebugTextArea.Instance.Log("Rewarded Ad Load Failed:" + args.LoadAdError.GetMessage());
    }

    public void HandleAdOpening(object sender, EventArgs args)
    {
        Debug.Log("Rewarded Ad Shown");
    }

    public void HandleAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        DebugTextArea.Instance.Log("Rewarded Ad Failed To Show: " + args.AdError.GetMessage());
    }

    public void HandleAdClosed(object sender, EventArgs args)
    {
        Debug.Log("Rewarded Ad Closed");

        // Preload new ad after closing current ad
        RequestRewarded();
    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        string type = args.Type;
        double amount = args.Amount;
        Debug.Log("User earned reward: " + amount.ToString() + " " + type);
    }

    #endregion
}
