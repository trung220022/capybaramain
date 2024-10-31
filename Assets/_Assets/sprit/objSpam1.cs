using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objSpam1 : MonoBehaviour
{
    public GameObject obstaclePrefab; // Prefab của chướng ngại vật
    public Vector3 spawnOffset; // Khoảng cách để sinh ra chướng ngại vật mới
    public float spawnCooldown = 1f; // Thời gian giữa các lần sinh
    public Vector3 obstacleScale = new Vector3(0.5f, 0.5f, 1); // Kích thước của chướng ngại vật
    private bool canSpawn = true; // Kiểm tra có thể sinh thêm chướng ngại vật không

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && canSpawn)
        {
            // Sinh chướng ngại vật mới ở vị trí mới
            Vector3 spawnPosition = transform.position + spawnOffset;
            GameObject newObstacle = Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);

            // Điều chỉnh kích thước của chướng ngại vật mới
            newObstacle.transform.localScale = obstacleScale;

            // Thiết lập tốc độ ngẫu nhiên cho chướng ngại vật mới
            tofuRight obstacleScript = newObstacle.GetComponent<tofuRight>();
            if (obstacleScript != null)
            {
                obstacleScript.minSpeed = 3.3f; // Tốc độ tối thiểu
                obstacleScript.maxSpeed = 5f; // Tốc độ tối đa
            }



            // Thiết lập thời gian cooldown để không sinh quá nhiều lần
            StartCoroutine(SpawnCooldown());
        }
    }

    // Chờ một thời gian trước khi có thể sinh chướng ngại vật mới
    IEnumerator SpawnCooldown()
    {
        canSpawn = false;
        yield return new WaitForSeconds(spawnCooldown);
        canSpawn = true;
    }
}
