using UnityEngine;
using UnityEngine.SceneManagement;
using GoogleMobileAds.Api;

public class GameManager : MonoBehaviour
{
    void Start()
    {
        // Initialize Google Mobile Ads SDK
        MobileAds.Initialize(initStatus => { });
    }

    public void GoToDemoGame()
    {
        SceneManager.LoadSceneAsync("GameDemo", LoadSceneMode.Single);
    }
}
