using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    public GameObject loadingScreen;  // Màn hình loading
    public Slider progressBar;        // Thanh tiến trình
    public Image bgloadingscene;
    
    public float loadDuration = 5f;   // Thời gian tải giả lập (5 giây)

    void Start()
    {
        // Bắt đầu tiến trình tải khi vừa nhấn Play
        StartCoroutine(LoadSceneAsync());
    }

    IEnumerator LoadSceneAsync()
    {
        // Hiển thị màn hình loading
        loadingScreen.SetActive(true);

        // Tải cảnh chính (đổi "MainScene" thành tên cảnh của bạn)
        AsyncOperation operation = SceneManager.LoadSceneAsync("MainScene");
        operation.allowSceneActivation = false; // Tắt tự động chuyển cảnh khi hoàn tất

        // Biến để theo dõi thời gian tải
        float elapsedTime = 0f;

        // Chạy tiến trình giả lập trong khoảng thời gian 5 giây
        while (elapsedTime < loadDuration)
        {
            // Tính tỷ lệ hoàn thành dựa trên thời gian đã trôi qua
            float progress = Mathf.Clamp01(elapsedTime / loadDuration);
            progressBar.value = progress; // Cập nhật thanh tiến trình

            elapsedTime += Time.deltaTime; // Cập nhật thời gian đã trôi qua
            yield return null;
        }

        // Đảm bảo thanh tiến trình đạt 100% trước khi chuyển cảnh
        progressBar.value = 1f;

        // Đợi một chút trước khi kích hoạt cảnh tiếp theo
        yield return new WaitForSeconds(0.5f);

        // Kích hoạt chuyển cảnh khi tiến trình đạt 100%
        operation.allowSceneActivation = true;
    }
}
