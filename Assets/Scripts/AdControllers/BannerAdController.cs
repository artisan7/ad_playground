using System;
using UnityEngine;
using GoogleMobileAds.Api;

public class BannerAdController : MonoBehaviour
{
    public BannerView bannerView { get; private set; }

    private void RequestBanner(AdPosition position)
    {
        // These units are configured to always serve test banner ads for android and ios respectively.
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-3940256099942544/6300978111";
#elif UNITY_IOS
        string adUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
        string adUnitId = "unexpected_platform"
#endif


        // Clean up previous banner before creating a new one to avoid memory leak
        if (bannerView != null)
            bannerView.Destroy();

        // create an adaptive size based on screen size
        AdSize bannerSize = AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(320);

        // calculate bannerWidth if width parameter is specified
        //AdSize bannerSize = AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(width);
        bannerView = new BannerView(adUnitId, bannerSize, position);

        // Register ad events
        bannerView.OnAdLoaded += HandleAdLoaded;
        bannerView.OnAdFailedToLoad += HandleAdFailedToLoad;
        bannerView.OnAdOpening += HandleAdOpening;
        bannerView.OnAdClosed += HandleAdClosed;

        // create empty add request
        AdRequest request = new AdRequest.Builder().Build();

        // load banner using request
        bannerView.LoadAd(request);
    }

    public void ShowBannerAd(bool show, int position = 0)
    {
        // AdPosition values
        // Top = 0,
        // Bottom = 1,
        // TopLeft = 2,
        // TopRight = 3,
        // BottomLeft = 4,
        // BottomRight = 5,
        // Center = 6
        if (!show || bannerView != null)
        {
            bannerView.Destroy();
            bannerView = null;
        }

        if (show)
            RequestBanner((AdPosition)position);
    }
    
    // This function is attached and called when certain buttons are pressed
    public void HandleToggleBanner(int position)
    {
        // AdPosition values
        // Top = 0,
        // Bottom = 1,
        // TopLeft = 2,
        // TopRight = 3,
        // BottomLeft = 4,
        // BottomRight = 5,
        // Center = 6
        if (bannerView != null)
        {
            bannerView.Destroy();
            bannerView = null;
        }
        else
        {
            RequestBanner((AdPosition)position);
        }
    }

#region Banner callback handlers
    public void HandleAdLoaded(object sender, EventArgs args)
    {
        Debug.Log("Banner Ad Loaded");
    }

    public void HandleAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        DebugTextArea.Instance.Log("Banner Ad Load Failed:" + args.LoadAdError.GetMessage());
    }

    public void HandleAdOpening(object sender, EventArgs args)
    {
        Debug.Log("Banner Ad Clicked");
    }

    public void HandleAdClosed(object sender, EventArgs args)
    {
        Debug.Log("Banner Ad Closed");
    }

#endregion
}
