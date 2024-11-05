using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using System.Collections;
using System;

public class AdManager : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] Button[] adButtons;
    [SerializeField] Button rewardedAdButton; // Nút để xem quảng cáo thưởng
    [SerializeField] string _androidRewardedAdUnitId = "Rewarded_Android";
    [SerializeField] string _iOSRewardedAdUnitId = "Rewarded_iOS";
    [SerializeField] string _androidInterstitialAdUnitId = "Interstitial_Android";
    [SerializeField] string _iOSInterstitialAdUnitId = "Interstitial_iOS";
    [SerializeField] string _bannerAdUnitId = "Banner_Ad_Unit_ID";
    private string _currentAdUnitId;

    private int rewardAmount = 50; // Số coin thưởng mỗi lần xem quảng cáo
    private TimeSpan cooldownTime = TimeSpan.FromMinutes(30); // Thời gian chờ 30 phút
    private const string lastAdWatchedKey = "LastAdWatchedTime"; // Khóa lưu thời gian xem quảng cáo cuối cùng
    private const string adRewardClaimedKey = "AdRewardClaimed"; // Khóa lưu trạng thái nhận coin từ quảng cáo
    private DateTime nextAvailableRewardTime; // Thời gian có thể xem quảng cáo tiếp theo

    void Awake()
    {
        foreach (Button button in adButtons)
        {
            button.onClick.AddListener(() => ShowAd(button));
        }

        rewardedAdButton.onClick.AddListener(ShowRewardedAd); // Gán sự kiện cho nút quảng cáo thưởng

#if UNITY_IOS
        _currentAdUnitId = _iOSRewardedAdUnitId;
#elif UNITY_ANDROID
        _currentAdUnitId = _androidRewardedAdUnitId;
#endif

        LoadAllAds();
        LoadLastAdWatchedTime(); // Tải thời gian xem quảng cáo cuối cùng
        CheckRewardButtonStatus(); // Kiểm tra trạng thái nút quảng cáo khi khởi tạo
    }

    public void LoadAllAds()
    {
        LoadAd(_androidRewardedAdUnitId);
        LoadAd(_androidInterstitialAdUnitId);
        LoadBannerAd();
    }

    private void LoadAd(string adUnitId)
    {
        Advertisement.Load(adUnitId, this);
    }

    public void ShowAd(Button clickedButton)
    {
        if (clickedButton.name.Contains("Rewarded"))
        {
            _currentAdUnitId = (Application.platform == RuntimePlatform.IPhonePlayer) ? _iOSRewardedAdUnitId : _androidRewardedAdUnitId;
        }
        else if (clickedButton.name.Contains("Interstitial"))
        {
            _currentAdUnitId = (Application.platform == RuntimePlatform.IPhonePlayer) ? _iOSInterstitialAdUnitId : _androidInterstitialAdUnitId;
        }

        Advertisement.Show(_currentAdUnitId, this);
    }

    public void ShowRewardedAd()
    {
        // Kiểm tra xem đã hết thời gian chờ chưa
        if (DateTime.Now >= nextAvailableRewardTime)
        {
            _currentAdUnitId = (Application.platform == RuntimePlatform.IPhonePlayer) ? _iOSRewardedAdUnitId : _androidRewardedAdUnitId;
            Advertisement.Show(_currentAdUnitId, this);
        }
        else
        {
            Debug.Log("Vui lòng chờ để xem quảng cáo thưởng tiếp theo.");
        }
    }

    public void LoadBannerAd()
    {
        BannerLoadOptions loadOptions = new BannerLoadOptions
        {
            loadCallback = () => Debug.Log("Banner loaded successfully."),
            errorCallback = (message) => Debug.Log("Banner failed to load: " + message)
        };

        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
        Advertisement.Banner.Load(_bannerAdUnitId, loadOptions);
        Advertisement.Banner.Show(_bannerAdUnitId);
    }

    void OnDestroy()
    {
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
        if (showCompletionState == UnityAdsShowCompletionState.COMPLETED && adUnitId == _currentAdUnitId)
        {
            Debug.Log("Ad completed, granting reward.");
            GrantReward(); // Gọi hàm thưởng coin cho người chơi
            nextAvailableRewardTime = DateTime.Now.Add(cooldownTime); // Thiết lập thời gian chờ
            PlayerPrefs.SetString(lastAdWatchedKey, DateTime.Now.ToString()); // Lưu thời gian xem quảng cáo
            PlayerPrefs.Save(); // Lưu PlayerPrefs
            StartCoroutine(Countdown()); // Bắt đầu đếm ngược
            CheckRewardButtonStatus(); // Kiểm tra lại trạng thái nút quảng cáo thưởng
        }
        else if (showCompletionState == UnityAdsShowCompletionState.SKIPPED)
        {
            Debug.Log("Ad skipped, no reward granted.");
        }

        LoadAd(adUnitId); // Tải quảng cáo sau khi xem xong
    }

    private void GrantReward()
    {
        // Kiểm tra xem người chơi đã nhận coin từ quảng cáo chưa
        if (ScoreCoin.instance != null && !PlayerPrefs.HasKey(adRewardClaimedKey))
        {
            ScoreCoin.instance.AddCoins(rewardAmount); // Thêm coin cho người chơi
            Debug.Log("Người chơi nhận được " + rewardAmount + " coin.");

            // Đánh dấu rằng người chơi đã nhận coin từ quảng cáo
            PlayerPrefs.SetInt(adRewardClaimedKey, 1);
            PlayerPrefs.Save(); // Lưu PlayerPrefs
        }
    }

    private void LoadLastAdWatchedTime()
    {
        if (PlayerPrefs.HasKey(lastAdWatchedKey))
        {
            string lastAdWatchedTimeString = PlayerPrefs.GetString(lastAdWatchedKey);
            nextAvailableRewardTime = DateTime.Parse(lastAdWatchedTimeString).Add(cooldownTime); // Thiết lập thời gian chờ từ PlayerPrefs

            // Reset trạng thái nhận coin khi thời gian chờ đã hết
            if (DateTime.Now >= nextAvailableRewardTime)
            {
                PlayerPrefs.DeleteKey(adRewardClaimedKey); // Xóa trạng thái đã nhận coin
            }
        }
        else
        {
            nextAvailableRewardTime = DateTime.Now; // Nếu không có thời gian lưu, cho phép xem ngay
        }
    }

    private void CheckRewardButtonStatus()
    {
        if (DateTime.Now < nextAvailableRewardTime)
        {
            rewardedAdButton.interactable = false; // Vô hiệu hóa nút nếu chưa đến thời gian
            TimeSpan remainingTime = nextAvailableRewardTime - DateTime.Now;
            rewardedAdButton.GetComponentInChildren<Text>().text = remainingTime.Minutes.ToString("D2") + ":" + remainingTime.Seconds.ToString("D2"); // Hiển thị thời gian còn lại
        }
        else
        {
            rewardedAdButton.interactable = true; // Kích hoạt nút nếu đã đến thời gian
            rewardedAdButton.GetComponentInChildren<Text>().text = ""; // Không hiển thị chữ
        }
    }


    private IEnumerator Countdown()
    {
        while (DateTime.Now < nextAvailableRewardTime)
        {
            CheckRewardButtonStatus(); // Cập nhật trạng thái nút
            yield return new WaitForSeconds(1); // Đợi 1 giây trước khi lặp lại
        }
    }

    void Update()
    {
        // Cập nhật trạng thái nút quảng cáo thưởng
        CheckRewardButtonStatus();
    }
}
