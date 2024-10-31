using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] Button[] adButtons; // Mảng chứa các nút quảng cáo
    [SerializeField] string _androidRewardedAdUnitId = "Rewarded_Android";
    [SerializeField] string _iOSRewardedAdUnitId = "Rewarded_iOS";
    [SerializeField] string _androidInterstitialAdUnitId = "Interstitial_Android";
    [SerializeField] string _iOSInterstitialAdUnitId = "Interstitial_iOS";
    private string _adUnitId;

    void Awake()
    {
        // Gán sự kiện cho các nút
        foreach (Button button in adButtons)
        {
            button.onClick.AddListener(() => ShowAd(button));
        }

        // Lấy ID quảng cáo cho nền tảng hiện tại
#if UNITY_IOS
            _adUnitId = _iOSRewardedAdUnitId; // Hoặc Interstitial tùy thuộc vào nút
#elif UNITY_ANDROID
        _adUnitId = _androidRewardedAdUnitId; // Hoặc Interstitial tùy thuộc vào nút
#endif

        // Bỏ qua việc tải quảng cáo banner tạm thời
        // LoadBannerAd();
    }

    public void ShowAd(Button clickedButton)
    {
        // Xác định loại quảng cáo dựa trên tên của nút
        if (clickedButton.name.Contains("Rewarded"))
        {
            _adUnitId = (Application.platform == RuntimePlatform.IPhonePlayer) ? _iOSRewardedAdUnitId : _androidRewardedAdUnitId;
            Advertisement.Show(_adUnitId, this); // Hiện quảng cáo thưởng
        }
        else if (clickedButton.name.Contains("Interstitial"))
        {
            _adUnitId = (Application.platform == RuntimePlatform.IPhonePlayer) ? _iOSInterstitialAdUnitId : _androidInterstitialAdUnitId;
            Advertisement.Show(_adUnitId, this); // Hiện quảng cáo xen kẽ
        }
    }

    // Tải quảng cáo banner (tạm thời không gọi)
    public void LoadBannerAd()
    {
        // Tạo đối tượng BannerLoadOptions
        BannerLoadOptions loadOptions = new BannerLoadOptions
        {
            loadCallback = () => Debug.Log("Banner loaded successfully."),
            errorCallback = (message) => Debug.Log("Banner failed to load: " + message)
        };

        // Tải quảng cáo banner và hiển thị nó
        Advertisement.Banner.Load("Banner_Ad_Unit_ID", loadOptions); // Thay "Banner_Ad_Unit_ID" bằng ID quảng cáo banner của bạn
        Advertisement.Banner.Show("Banner_Ad_Unit_ID");
    }

    void OnDestroy()
    {
        // Ẩn quảng cáo banner khi tắt game
        Advertisement.Banner.Hide();
    }

    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        Debug.Log("Ad Loaded: " + adUnitId);
    }

    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit {adUnitId}: {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowStart(string adUnitId) { }

    public void OnUnityAdsShowClick(string adUnitId) { }

    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        // Grant rewards if necessary for rewarded ads
        if (showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            Debug.Log("Ad completed, grant reward.");
            // Grant reward code here
        }
    }
}
