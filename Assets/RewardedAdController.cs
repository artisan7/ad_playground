using System;
using UnityEngine;
using GoogleMobileAds.Api;

public class RewardedAdController : MonoBehaviour
{
    // This ad unit is configured to always serve test banner ads for android.
    private string androidAppId = "ca-app-pub-3940256099942544/5224354917";
    private RewardedAd rewardedAd;

    private void Start()
    {
        // Preload the ad
        RequestRewarded();
    }

    private void RequestRewarded()
    {
        // Clean up previous ad before creating a new one to avoid memory leak
        if (rewardedAd != null)
            rewardedAd.Destroy();

        // Initialize rewarded ad
        rewardedAd = new RewardedAd(androidAppId);

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
        Debug.Log("Rewarded Ad Load Failed:" + args.LoadAdError.GetMessage());
    }

    public void HandleAdOpening(object sender, EventArgs args)
    {
        Debug.Log("Rewarded Ad Shown");
    }

    public void HandleAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        Debug.Log("Rewarded Ad Failed To Show: " + args.AdError.GetMessage());
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
