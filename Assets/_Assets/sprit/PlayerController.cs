using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce;
    private Rigidbody2D rb;
    private bool gameStarted = false; // Biến kiểm tra xem trò chơi đã bắt đầu hay chưa
    public GameStartController gameStartController; // Tham chiếu đến GameStartController
    public AudioManager audioManager; // Tham chiếu đến AudioManager để phát âm thanh

    public GameObject specialGameObject; // GameObject bạn muốn hiển thị

    void Start()
    {
        Application.targetFrameRate = 60;  // Đặt tốc độ khung hình mục tiêu
        rb = GetComponent<Rigidbody2D>();
        specialGameObject.SetActive(false); // Đảm bảo GameObject này ẩn ban đầu
    }

    void Update()
    {
        // Chỉ cho phép nhân vật nhảy nếu trò chơi đã bắt đầu
        if (!gameStarted)
        {
            return; // Nếu trò chơi chưa bắt đầu, không cho phép nhảy
        }

        // Khi nhấn chuột, nhân vật sẽ nhảy nếu đang ở trên mặt đất
        if (Input.GetMouseButtonDown(0))
        {
            // Kiểm tra nếu nhân vật đang ở trên mặt đất (không rơi hoặc bay)
            if (Mathf.Abs(rb.velocity.y) < 0.001f)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce); // Nhân vật nhảy lên
            }
        }
    }

    // Hàm này sẽ được gọi khi người chơi nhấn nút bắt đầu game
    public void StartGame()
    {
        gameStarted = true; // Kích hoạt trạng thái trò chơi đã bắt đầu
    }

    // Khi nhân vật va chạm với chướng ngại vật
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Kiểm tra nếu đối tượng va chạm có tag "Obstacle"
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            // Không cộng coin khi trò chơi đã thua
            ObstacleSpawner obstacleSpawner = FindObjectOfType<ObstacleSpawner>();
            if (obstacleSpawner != null && obstacleSpawner.GameLost)
            {
                return; // Không cộng coin khi trò chơi đã thua
            }

            // Cộng điểm khi nhân vật tiếp đất trên chướng ngại vật
            if (rb.velocity.y <= 0) // Kiểm tra nếu nhân vật đang rơi xuống
            {
                ScoreCoin.instance.AddCoins(1); // Cộng 1 coin
                ScoreManager.instance.AddScore(1); // Cộng 1 điểm

                // Kiểm tra layer trùng để hiển thị GameObject đặc biệt
                if (collision.gameObject.layer == gameObject.layer)
                {
                    specialGameObject.SetActive(true); // Hiển thị GameObject đặc biệt
                }

                // Phát âm thanh khi nhảy lên chướng ngại vật
                if (audioManager != null)
                {
                    audioManager.PlayHitObstacleSound(); // Giả sử đây là âm thanh cho va chạm với chướng ngại vật
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Ẩn GameObject đặc biệt khi không còn va chạm với chướng ngại vật trùng layer
        if (collision.gameObject.CompareTag("Obstacle") && collision.gameObject.layer == gameObject.layer)
        {
            // Không ẩn nếu còn va chạm với vachamdie
            if (specialGameObject.activeSelf)
            {
                // Để lại GameObject hiển thị nếu còn đang va chạm với vachamdie
                return;
            }

            specialGameObject.SetActive(false); // Ẩn GameObject
        }
    }
}
