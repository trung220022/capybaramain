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

    public Button[] unlockableButtons; // Danh sách button có thể unlock

    void Start()
    {
        LoadUnlockedButtons(); // Tải trạng thái button từ PlayerPrefs

        startButton.SetActive(true);

        foreach (var button in otherButtons)
        {
            button.SetActive(true);
        }

        losePanel.SetActive(false);
        replayButton.gameObject.SetActive(false);

        if (parachute != null)
        {
            parachute.SetActive(false);
        }

        if (scoreText != null)
        {
            scoreText.gameObject.SetActive(false);
        }

        if (playerController != null)
        {
            playerController.enabled = false;
        }

        foreach (var obstacle in obstacles)
        {
            obstacle.StopMovement();
        }

        if (obstacleSpawner != null)
        {
            obstacleSpawner.enabled = false;
        }

        replayButton.onClick.AddListener(OnReplayButtonPressed);
    }

    public void OnStartButtonPressed()
    {
        if (UIController.isPanelOpen)
        {
            return;
        }

        startButton.SetActive(false);

        foreach (var button in otherButtons)
        {
            button.SetActive(false);
        }

        if (scoreText != null)
        {
            scoreText.gameObject.SetActive(true);
        }

        if (playerController != null)
        {
            playerController.enabled = true;
            playerController.StartGame();
        }

        foreach (var obstacle in obstacles)
        {
            obstacle.StartMovement();
        }

        if (obstacleSpawner != null)
        {
            obstacleSpawner.enabled = true;
        }
    }

    public void OnPlayerLose()
    {
        losePanel.SetActive(true);
        replayButton.gameObject.SetActive(true);

        if (parachute != null)
        {
            parachute.SetActive(true);
        }

        foreach (var button in otherButtons)
        {
            button.SetActive(false);
        }

        playerController.enabled = false;
        foreach (var obstacle in obstacles)
        {
            obstacle.StopMovement();
        }
        obstacleSpawner.enabled = false;

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

    // ====== XỬ LÝ GIỮ TRẠNG THÁI BUTTON SAU KHI VÀO SHOP ======

    void LoadUnlockedButtons()
    {
        for (int i = 0; i < unlockableButtons.Length; i++)
        {
            if (PlayerPrefs.GetInt("Button_" + i, 0) == 1)
            {
                unlockableButtons[i].gameObject.SetActive(true);
            }
            else
            {
                unlockableButtons[i].gameObject.SetActive(false);
            }
        }
    }
}
