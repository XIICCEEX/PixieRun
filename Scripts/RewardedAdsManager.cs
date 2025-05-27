using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class RewardedAdsManager : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] private string androidAdUnitId = "Rewarded_Android";
    [SerializeField] private Button watchAdButton;
    [SerializeField] private GameObject rewardPopup;

    private string adUnitId;

    void Start()
    {

        adUnitId = androidAdUnitId;

        // ปิดปุ่มก่อนจนกว่าจะโหลดเสร็จ
        if (watchAdButton != null)
            watchAdButton.interactable = false;

        // โหลดโฆษณา
        Advertisement.Load(adUnitId, this);
        Debug.Log("Button: " + watchAdButton);  // ดูว่า null มั้ย
        watchAdButton.onClick.AddListener(() => Debug.Log("Button Clicked!"));
    }

    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        if (adUnitId == this.adUnitId)
        {
            Debug.Log("Ad Loaded!");
            watchAdButton.interactable = true;

            if (watchAdButton != null)
                watchAdButton.onClick.AddListener(() => Advertisement.Show(adUnitId, this));
        }
    }

    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        if (showCompletionState == UnityAdsShowCompletionState.COMPLETED && adUnitId == this.adUnitId)
        {
            GiveReward();
        }
    }

    private void GiveReward()
    {
        rewardPopup.SetActive(true);
        // เพิ่มโค้ดให้รางวัลได้ตรงนี้
    }

    // Error handlers
    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.LogError($"Failed to load ad {adUnitId}: {message}");
    }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.LogError($"Failed to show ad {adUnitId}: {message}");
    }

    public void OnUnityAdsShowStart(string adUnitId) { }
    public void OnUnityAdsShowClick(string adUnitId) { }
}
