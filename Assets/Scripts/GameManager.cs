using UnityEngine;
using GoogleMobileAds.Api;

public class GameManager : MonoBehaviour
{
    void Start()
    {
        // Initialize Google Mobile Ads SDK
        MobileAds.Initialize(initStatus => { });
    }
}
