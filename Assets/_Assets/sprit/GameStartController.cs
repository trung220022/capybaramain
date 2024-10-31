using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameStartController : MonoBehaviour
{
    public GameObject startButton; // Button bắt đầu
    public GameObject[] otherButtons; // Mảng chứa các Button khác
    public GameObject losePanel; // Bảng thông báo thua
    public Button replayButton; // Button chơi lại trên bảng thua
    public PlayerController playerController; // Tham chiếu đến script điều khiển nhân vật
    public tofuMove[] obstacles; // Mảng các chướng ngại vật
    public ObstacleSpawner obstacleSpawner; // Tham chiếu đến script tạo chướng ngại vật
    public GameObject parachute; // Tham chiếu đến cái dù
    public Text scoreText; // Text để hiển thị điểm hiện tại

    void Start()
    {
        startButton.SetActive(true);

        foreach (var button in otherButtons)
        {
            button.SetActive(true);
        }

        losePanel.SetActive(false);
        replayButton.gameObject.SetActive(false); // Đảm bảo replayButton được ẩn

        if (parachute != null)
        {
            parachute.SetActive(false); // Tắt cái dù
        }

        if (scoreText != null)
        {
            scoreText.gameObject.SetActive(false);
        }

        if (playerController != null)
        {
            playerController.enabled = false; // Vô hiệu hóa điều khiển nhân vật trước khi bắt đầu trò chơi
        }

        foreach (var obstacle in obstacles)
        {
            obstacle.StopMovement(); // Đảm bảo chướng ngại vật không di chuyển trước khi bắt đầu
        }

        if (obstacleSpawner != null)
        {
            obstacleSpawner.enabled = false; // Vô hiệu hóa quá trình sinh chướng ngại vật
        }

        replayButton.onClick.AddListener(OnReplayButtonPressed);
    }

    public void OnStartButtonPressed()
    {
        // Kiểm tra nếu có bảng UI nào đang mở
        if (UIController.isPanelOpen)
        {
            return; // Dừng việc bắt đầu trò chơi
        }

        // Nếu không có tab nào đang mở, tiếp tục bắt đầu trò chơi
        startButton.SetActive(false);

        foreach (var button in otherButtons)
        {
            button.SetActive(false); // Ẩn tất cả các button khác khi trò chơi bắt đầu
        }

        if (scoreText != null)
        {
            scoreText.gameObject.SetActive(true);
        }

        if (playerController != null)
        {
            playerController.enabled = true; // Kích hoạt điều khiển nhân vật
            playerController.StartGame(); // Bắt đầu trò chơi và cho phép nhân vật di chuyển
        }

        foreach (var obstacle in obstacles)
        {
            obstacle.StartMovement(); // Bắt đầu di chuyển chướng ngại vật
        }

        if (obstacleSpawner != null)
        {
            obstacleSpawner.enabled = true; // Kích hoạt quá trình sinh chướng ngại vật
        }
    }

    public void OnPlayerLose()
    {
        losePanel.SetActive(true);
        replayButton.gameObject.SetActive(true); // Hiện nút chơi lại

        if (parachute != null)
        {
            parachute.SetActive(true); // Bật cái dù
        }

        foreach (var button in otherButtons)
        {
            button.SetActive(false);
        }

        playerController.enabled = false; // Vô hiệu hóa điều khiển nhân vật
        foreach (var obstacle in obstacles)
        {
            obstacle.StopMovement(); // Dừng di chuyển chướng ngại vật
        }
        obstacleSpawner.enabled = false; // Vô hiệu hóa sinh chướng ngại vật

        Invoke("RestartGameAfterDelay", 5f);
    }

    void RestartGameAfterDelay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnReplayButtonPressed()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
