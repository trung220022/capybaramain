using UnityEngine;
using UnityEngine.Advertisements;

public class BannerAdExample : MonoBehaviour, IUnityAdsInitializationListener
{
    [SerializeField] private string _bannerAdUnitId = "Banner_Android"; // Thay đổi ID quảng cáo banner

    private void Awake()
    {
        // Khởi tạo Unity Ads với một listener
        Advertisement.Initialize("5720091", true, this); // Thay "YOUR_GAME_ID" bằng ID của bạn
    }

    private void Start()
    {
        Debug.Log("Starting BannerAdExample");
        Debug.Log("Banner Ad Unit ID: " + _bannerAdUnitId); // Kiểm tra giá trị của _bannerAdUnitId
        LoadBannerAd();
    }

    public void LoadBannerAd()
    {
        if (string.IsNullOrEmpty(_bannerAdUnitId))
        {
            Debug.LogError("Banner Ad Unit ID is null or empty!"); // Kiểm tra xem ID có hợp lệ không
            return;
        }

        BannerLoadOptions loadOptions = new BannerLoadOptions
        {
            loadCallback = () =>
            {
                Debug.Log("Banner loaded successfully.");
                ShowBannerAd(); // Hiện quảng cáo banner sau khi tải thành công
            },
            errorCallback = (message) => Debug.Log("Banner failed to load: " + message)
        };

        // Tải quảng cáo banner
        Advertisement.Banner.Load(_bannerAdUnitId, loadOptions); // Đảm bảo _bannerAdUnitId đã được gán đúng
    }

    private void ShowBannerAd()
    {
        // Đặt vị trí cho quảng cáo banner
        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER); // Đặt quảng cáo ở dưới giữa màn hình

        // Hiện quảng cáo banner
        Advertisement.Banner.Show(_bannerAdUnitId);
    }

    private void OnDestroy()
    {
        // Ẩn quảng cáo banner khi tắt game
        if (Advertisement.Banner.isLoaded)
        {
            Advertisement.Banner.Hide(); // Chỉ ẩn nếu quảng cáo đã được tải
        }
    }

    // Các phương thức từ IUnityAdsInitializationListener
    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads Initialization Complete.");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.LogError($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }
}
