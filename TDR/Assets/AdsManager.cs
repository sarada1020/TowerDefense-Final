using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Advertisements;
using static UnityEngine.Advertisements.Advertisement;

public class AdsManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsShowListener
{
    float timer;
    bool bannerVisivel;
    bool visivelGeral;

    private void Start()
    {
        timer = 10f;
        bannerVisivel = true;
        visivelGeral = false;
        Advertisement.Initialize("5729657", true, this);
        Advertisement.Banner.SetPosition(BannerPosition.TOP_CENTER);
        Advertisement.Banner.Show("Banner_Android");
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (bannerVisivel && timer <= 0)
        {
            Advertisement.Banner.Hide();
            bannerVisivel = false;
            timer = 5f;
        }
        else if (!bannerVisivel && timer <= 0)
        {
            Advertisement.Banner.SetPosition(BannerPosition.TOP_CENTER);
            Advertisement.Banner.Show("Banner_Android");
            bannerVisivel = true;
            timer = 10f;
        }
    }
    public void Interstitial()
    {
        Advertisement.Show("Interstitial_Android", this);
    }
    public void Recompensa()
    {
        Advertisement.Show("Anuncio_Reward", this);
    }
    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        if (placementId == "Anuncio_Reward" && showCompletionState == UnityAdsShowCompletionState.COMPLETED)
        {
            Debug.Log("Player earned reward!");
            
        }
        else if (showCompletionState == UnityAdsShowCompletionState.SKIPPED)
        {
            Debug.Log("Player skipped the ad.");
            
        }
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {

    }

    public void OnUnityAdsShowStart(string placementId)
    {

    }

    public void OnUnityAdsShowClick(string placementId)
    {

    }
    public void OnInitializationComplete()
    {

    }
    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {

    }
}
