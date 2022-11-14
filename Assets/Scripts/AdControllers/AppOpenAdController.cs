using System;
using UnityEngine;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;

public class AppOpenAdController : MonoBehaviour
{
    private AppOpenAd appOpenAd;

    private void Start()
    {
        // Preload the ad
        RequestAppOpen();

        // Listen to application foreground and background events
        AppStateEventNotifier.AppStateChanged += HandleShowAppOpen;
    }

    private void RequestAppOpen()
    {
        // These units are configured to always serve test app open ads for android and ios respectively.
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-3940256099942544/3419835294";
#elif UNITY_IOS
        string adUnitId = "ca-app-pub-3940256099942544/5662855259";
#else
        string adUnitId = "unexpected_platform"
#endif

        // Clean up previous ad before creating a new one to avoid memory leak
        if (appOpenAd != null)
            appOpenAd.Destroy();

        // create empty add request
        AdRequest request = new AdRequest.Builder().Build();

        // load interstitial ad using request
        AppOpenAd.LoadAd(adUnitId, ScreenOrientation.PortraitUpsideDown, request, HandleAdLoad);
    }

    // This function is fired everytime the app state changes
    public void HandleShowAppOpen(AppState state)
    {
        // Display ad when app is foregrounded
        if (state == AppState.Foreground && appOpenAd != null)
            appOpenAd.Show();
        else
            DebugTextArea.Instance.Log("Please wait a few seconds for the Ad to Load.");
    }

    // This function is attached to a button (used only to test on the editor)
    public void HandleShowAppOpen()
    {
        // Display ad
        if (appOpenAd != null)
            appOpenAd.Show();
        else
            DebugTextArea.Instance.Log("Please wait a few seconds for the Ad to Load.");
    }

    #region Interstitial callback handlers
    public void HandleAdLoad(AppOpenAd ad, AdFailedToLoadEventArgs error)
    {
        if (error != null)
        {
            DebugTextArea.Instance.Log("App Open Ad Failed to Load: " + error.LoadAdError.GetMessage());
            return;
        }

        Debug.Log("App Open Ad Loaded");

        // assign appOpenAd and register ad events
        appOpenAd = ad;
        appOpenAd.OnAdFailedToPresentFullScreenContent += HandleAdFailedToOpen;
        appOpenAd.OnAdDidPresentFullScreenContent += HandleAdOpening;
        appOpenAd.OnAdDidDismissFullScreenContent += HandleAdClosed;
    }

    public void HandleAdFailedToOpen(object sender, AdErrorEventArgs args)
    {
        DebugTextArea.Instance.Log("App Open Ad Load Failed:" + args.AdError.GetMessage());
    }

    public void HandleAdOpening(object sender, EventArgs args)
    {
        Debug.Log("App Open Ad Shown");
    }

    public void HandleAdClosed(object sender, EventArgs args)
    {
        Debug.Log("App Open Ad Closed");

        // Preload another ad
        RequestAppOpen();
    }

    #endregion
}
