using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class RewardedAdsManager : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] private string androidAdUnitId = "Rewarded_Android";
    [SerializeField] private Button watchAdButton;
    [SerializeField] private GameObject rewardPopup;

    private string adUnitId;
    private bool adLoaded = false;

    void Start()
    {
        adUnitId = androidAdUnitId;

        // ปิดปุ่มไว้ก่อน
        watchAdButton.interactable = false;

        // กำหนดการกดปุ่ม
        watchAdButton.onClick.RemoveAllListeners();
        watchAdButton.onClick.AddListener(ShowAd);

        // โหลดโฆษณา
        Advertisement.Load(adUnitId, this);
    }

    private void ShowAd()
    {
        if (adLoaded)
        {
            Advertisement.Show(adUnitId, this);
        }
        else
        {
            Debug.Log("Ad not loaded yet.");
        }
    }

    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        if (adUnitId == this.adUnitId)
        {
            adLoaded = true;
            watchAdButton.interactable = true;
            Debug.Log("Ad Loaded and button enabled.");
        }
    }

    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        if (adUnitId == this.adUnitId && showCompletionState == UnityAdsShowCompletionState.COMPLETED)
        {
            Debug.Log("Ad Completed. Reward the player!");
            GiveReward();
        }
    }

    private void GiveReward()
    {
        rewardPopup.SetActive(true);
        
        
    }

    
    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.LogError($"Ad failed to load: {adUnitId} - {message}");
    }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.LogError($"Ad failed to show: {adUnitId} - {message}");
    }

    public void OnUnityAdsShowStart(string adUnitId) { }
    public void OnUnityAdsShowClick(string adUnitId) { }
}