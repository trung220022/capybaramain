using UnityEngine;

public class vachamdie : MonoBehaviour
{
    public float forceMagnitude = 10f; // Lực đẩy khi va chạm để người chơi văng xa hơn
    public float slowFallDrag = 10f; // Tăng drag để làm chậm rơi
    public float slowFallGravityScale = 0.1f; // Giảm trọng lực để làm rơi chậm hơn

    private Rigidbody2D playerRb; // Rigidbody của nhân vật
    private bool hasCollided = false; // Biến kiểm soát việc va chạm nhiều lần
    public GameStartController gameStartController; // Tham chiếu đến GameStartController
    public AudioManager audioManager; // Tham chiếu đến AudioManager để phát âm thanh

    void Start()
    {
        // Lấy Rigidbody của nhân vật từ đối tượng cha
        playerRb = GetComponentInParent<Rigidbody2D>();

        if (playerRb != null)
        {
            playerRb.isKinematic = false; // Đảm bảo Rigidbody của người chơi không kinematic
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Kiểm tra xem đã va chạm và đối tượng là chướng ngại vật
        if (!hasCollided && other.CompareTag("Obstacle"))
        {
            if (playerRb != null)
            {
                // Tính toán hướng va chạm theo hướng từ chướng ngại vật đến người chơi
                Vector2 forceDirection = (playerRb.transform.position - other.transform.position).normalized;

                // Đẩy người chơi ra xa theo hướng đã tính toán
                playerRb.AddForce(forceDirection * forceMagnitude, ForceMode2D.Impulse);

                // Tăng drag và giảm gravity scale để làm cho người chơi rơi chậm lại sau khi va chạm
                playerRb.drag = slowFallDrag;
                playerRb.gravityScale = slowFallGravityScale;

                // Phát âm thanh khi va cham die với chướng ngại vật
                if (audioManager != null)
                {
                    audioManager.PlayRoiDieSound(); // Giả sử đây là âm thanh cho va chạm với chướng ngại vật
                }

                if (audioManager != null)
                {
                    audioManager.PlayVaChamDieSound(); // Giả sử đây là âm thanh cho va chạm với chướng ngại vật
                }

                gameStartController.OnPlayerLose(); // Nếu cần gọi sự kiện thua trò chơi

                // Ẩn specialGameObject
                PlayerController playerController = GetComponentInParent<PlayerController>();
                if (playerController != null)
                {
                    playerController.specialGameObject.SetActive(false); // Ẩn GameObject đặc biệt
                }

                // Gọi sự kiện thua trò chơi
                gameStartController.OnPlayerLose();

                ObstacleSpawner obstacleSpawner = FindObjectOfType<ObstacleSpawner>();
                if (obstacleSpawner != null)
                {
                    obstacleSpawner.SetGameLost(true); // Thiết lập trạng thái thua
                }
            }

            // Dừng vật thể hiện tại nếu có Rigidbody
            if (GetComponent<Rigidbody2D>() != null)
            {
                Rigidbody2D rb = GetComponent<Rigidbody2D>();
                rb.velocity = Vector2.zero;
                rb.isKinematic = false; // Cho phép vật thể tiếp tục rơi
            }

            // Đánh dấu đã va chạm để không xử lý nhiều lần
            hasCollided = true;
        }
    }
}