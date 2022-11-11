using UnityEngine;
using GoogleMobileAds.Api;

public class AdManager : MonoBehaviour
{
    string androidAppID = "ca-app-pub-3940256099942544/6300978111";

    private BannerView bannerView;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize Google Mobile Ads SDK
        MobileAds.Initialize(initStatus => { });

        this.RequestBanner();
    }

    private void RequestBanner()
    {
        this.bannerView = new BannerView(androidAppID, AdSize.SmartBanner, AdPosition.Top);

        // create empty add request
        AdRequest request = new AdRequest.Builder().Build();

        // load banner using request
        this.bannerView.LoadAd(request);
    }
}
