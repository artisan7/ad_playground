using System;
using UnityEngine;
using GoogleMobileAds.Api;

public class BannerAdController : MonoBehaviour
{
    // This ad unit is configured to always serve test banner ads for android.
    private string androidAppId = "ca-app-pub-3212738706492790/6113697308";
    private BannerView bannerView;

    private void RequestBanner(AdPosition position, int width = 0)
    {
        // Clean up previous banner before creating a new one to avoid memory leak
        if (bannerView != null)
            bannerView.Destroy();

        // create an adaptive size based on screen size
        //AdSize fullWidthSize = AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(AdSize.FullWidth);

        // calculate bannerWidth if width parameter is specified
        AdSize bannerSize = AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(width);
        bannerView = new BannerView(androidAppId, bannerSize, position);

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
            RequestBanner((AdPosition)position, 100);
        }
    }

    #region Banner callback handlers
    public void HandleAdLoaded(object sender, EventArgs args)
    {
        Debug.Log("Banner Ad Loaded");
    }

    public void HandleAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        Debug.Log("Banner Ad Load Failed:" + args.LoadAdError.GetMessage());
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
