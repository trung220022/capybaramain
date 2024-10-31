using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance; // Singleton để dễ dàng truy cập
    public Text scoreText; // Text để hiển thị điểm hiện tại trên UI
    public Text highScoreText; // Text để hiển thị điểm cao nhất trên UI
    public Text highScoreText1; // Text để hiển thị điểm cao nhất trên UI
    private int score = 0; // Điểm hiện tại
    private int highScore = 0; // Điểm cao nhất

    void Awake()
    {
        // Kiểm tra nếu chưa có instance nào
        if (instance == null)
        {
            instance = this;
            LoadHighScore(); // Tải điểm cao nhất khi khởi tạo
        }
        else
        {
            Destroy(gameObject); // Ngăn tạo thêm instance
        }
    }

    void Start()
    {
        UpdateScoreText(); // Cập nhật UI điểm hiện tại
        UpdateHighScoreText(); // Cập nhật UI điểm cao nhất
    }

    // Hàm để tăng điểm
    public void AddScore(int amount)
    {
        score += amount; // Tăng điểm
        UpdateScoreText(); // Cập nhật UI điểm hiện tại
        CheckAndSetHighScore(); // Kiểm tra và cập nhật điểm cao nhất
    }

    // Hàm để cập nhật UI cho điểm hiện tại
    private void UpdateScoreText()
    {
        scoreText.text = "" + score.ToString(); // Cập nhật UI để hiển thị điểm hiện tại
    }

    // Hàm để cập nhật UI cho điểm cao nhất
    private void UpdateHighScoreText()
    {
        highScoreText.text = "" + highScore.ToString(); // Cập nhật UI để hiển thị điểm cao nhất
        highScoreText1.text = "" + highScore.ToString(); // Cập nhật UI để hiển thị điểm cao nhất
    }

    // Lưu điểm cao nhất vào PlayerPrefs
    private void SaveHighScore()
    {
        PlayerPrefs.SetInt("HighScore", highScore);
        PlayerPrefs.Save(); // Lưu dữ liệu
    }

    // Tải điểm cao nhất từ PlayerPrefs
    private void LoadHighScore()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0); // Mặc định là 0 nếu chưa có dữ liệu
    }

    // Hàm để kiểm tra và cập nhật điểm cao nhất
    private void CheckAndSetHighScore()
    {
        if (score > highScore)
        {
            highScore = score; // Cập nhật điểm cao nhất
            SaveHighScore(); // Lưu điểm cao nhất
            UpdateHighScoreText(); // Cập nhật UI
        }
    }

    // Hàm để reset điểm nếu cần
    public void ResetScore()
    {
        score = 0; // Reset điểm hiện tại
        UpdateScoreText(); // Cập nhật UI
    }

    // Hàm để lấy điểm hiện tại
    public int GetScore()
    {
        return score;
    }
}
