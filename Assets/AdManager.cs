using System;
using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds.Api;

public class AdManager : MonoBehaviour
{
    private BannerView bannerView;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize Google Mobile Ads SDK
        MobileAds.Initialize(initStatus => { });

        this.RequestBanner(AdPosition.Top, 100);
    }

    private void RequestBanner(AdPosition position, int width = 0)
    {
        // These ad units are configured to always serve test ads.
        #if UNITY_EDITOR
            string adUnitId = "unused";
        #elif UNITY_ANDROID
            string adUnitId = "ca-app-pub-3212738706492790/6113697308";
        #elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3212738706492790/5381898163";
        #else
            string adUnitId = "unexpected_platform";
        #endif

        // Clean up previous banner before creating a new one to avoid memory leak
        if (bannerView != null)
            bannerView.Destroy();

        // create an adaptive size based on screen size
        //AdSize fullWidthSize = AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(AdSize.FullWidth);
        
        // calculate bannerWidth if width parameter is specified
        AdSize bannerSize = AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(width);
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

    public void HandleAdLoaded(object sender, EventArgs args)
    {
        Debug.Log("Ad Loaded");
    }

    public void HandleAdFailedToLoad(object sender, EventArgs args)
    {
        Debug.Log("Ad Load Failed");
    }

    public void HandleAdOpening(object sender, EventArgs args)
    {
        Debug.Log("Ad Clicked");
    }

    public void HandleAdClosed(object sender, EventArgs args)
    {
        Debug.Log("Ad Closed");
    }
}
