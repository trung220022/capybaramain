using System.Collections;
using System.Collections.Generic; // Thêm không gian tên cho danh sách
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] obstaclePrefab; // Prefab của chướng ngại vật
    public Vector3 spawnOffset; // Khoảng cách để sinh ra chướng ngại vật mới
    public float spawnCooldown = 1f; // Thời gian giữa các lần sinh

    // Tạo các kích thước riêng cho từng chướng ngại vật
    public Vector3[] obstacleScales = new Vector3[4]; // Ví dụ với 4 chướng ngại vật

    // Mảng tỉ lệ xuất hiện của từng vật thể
    public float[] spawnChances; // Tỉ lệ ngẫu nhiên cho 4 vật thể

    private bool canSpawn = true; // Kiểm tra có thể sinh thêm chướng ngại vật không
    private bool gameLost = false; // Kiểm tra xem trò chơi đã thua hay chưa

    // Danh sách chứa các tham chiếu đến chướng ngại vật đã sinh
    private List<GameObject> spawnedObstacles = new List<GameObject>();

    void Update()
    {
        // Kiểm tra trạng thái trò chơi đã thua hay chưa
        if (gameLost)
        {
            canSpawn = false; // Ngừng sinh chướng ngại vật
        }
    }
    
    public void SetGameLost(bool lost)
    {
        gameLost = lost; // Cập nhật trạng thái thua

        // Dừng tất cả các chướng ngại vật hiện tại
        foreach (GameObject obstacle in spawnedObstacles)
        {
            if (obstacle != null)
            {
                tofuMove obstacleScript = obstacle.GetComponent<tofuMove>();
                if (obstacleScript != null)
                {
                    obstacleScript.StopMovement(); // Dừng chuyển động
                }

                Rigidbody2D rb = obstacle.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.velocity = Vector2.zero; // Dừng chướng ngại vật
                    rb.isKinematic = true; // Ngăn không cho chướng ngại vật di chuyển nữa
                }
            }
        }
    }

    public bool GameLost
    {
        get { return gameLost; }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && canSpawn)
        {
            if (gameLost)
            {
                return; // Không sinh chướng ngại vật
            }

            // Chọn ngẫu nhiên vật thể dựa trên tỉ lệ
            int randomIndex = GetRandomObstacleIndex();

            // Sinh chướng ngại vật mới ở vị trí mới
            Vector3 spawnPosition = transform.position + spawnOffset;
            GameObject newObstacle = Instantiate(obstaclePrefab[randomIndex], spawnPosition, Quaternion.identity);

            // Điều chỉnh kích thước của chướng ngại vật mới
            newObstacle.transform.localScale = obstacleScales[randomIndex];

            // Thiết lập tốc độ ngẫu nhiên cho chướng ngại vật mới
            tofuMove obstacleScript = newObstacle.GetComponent<tofuMove>();
            if (obstacleScript != null)
            {
                // Tùy chỉnh tốc độ cho chướng ngại vật
                obstacleScript.minSpeed = 3f; // Tốc độ tối thiểu
                obstacleScript.maxSpeed = 6f; // Tốc độ tối đa

                // Bắt đầu di chuyển chướng ngại vật
                obstacleScript.StartMovement();
            }

            // Thêm chướng ngại vật mới vào danh sách
            spawnedObstacles.Add(newObstacle);

            // Thiết lập thời gian cooldown để không sinh quá nhiều lần
            StartCoroutine(SpawnCooldown());
        }
    }

    // Chờ một thời gian trước khi có thể sinh chướng ngại vật mới
    IEnumerator SpawnCooldown()
    {
        canSpawn = false; // Ngừng sinh trong thời gian cooldown
        yield return new WaitForSeconds(spawnCooldown); // Đợi thời gian cooldown
        canSpawn = true; // Cho phép sinh lại
    }

    // Hàm để chọn ngẫu nhiên một chướng ngại vật dựa trên tỉ lệ
    int GetRandomObstacleIndex()
    {
        float total = 0f;
        foreach (float chance in spawnChances)
        {
            total += chance; // Tính tổng xác suất
        }

        float randomValue = Random.value * total; // Sinh giá trị ngẫu nhiên từ 0 đến tổng xác suất
        float cumulative = 0f;

        for (int i = 0; i < spawnChances.Length; i++)
        {
            cumulative += spawnChances[i]; // Tăng giá trị tích lũy
            if (randomValue <= cumulative)
            {
                return i; // Trả về chỉ số của vật thể phù hợp với giá trị ngẫu nhiên
            }
        }

        return spawnChances.Length - 1; // Nếu không có gì phù hợp, trả về vật thể cuối cùng
    }

    // Phương thức để lấy danh sách chướng ngại vật đã sinh
    public List<GameObject> GetSpawnedObstacles()
    {
        return spawnedObstacles;
    }
}
