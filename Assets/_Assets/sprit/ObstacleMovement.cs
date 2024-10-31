using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    private float speed; // Tốc độ di chuyển của chướng ngại vật

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed; // Thiết lập tốc độ di chuyển
    }

    void Update()
    {
        // Di chuyển chướng ngại vật xuống dưới theo tốc độ đã chỉ định
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }
}
