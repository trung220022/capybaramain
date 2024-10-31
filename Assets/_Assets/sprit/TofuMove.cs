using UnityEngine;

public class tofuMove : MonoBehaviour
{
    private Rigidbody2D rb;

    public float moveSpeed;
    public float leftBound;
    public float rightBound;

    public float minSpeed;  // Tốc độ tối thiểu
    public float maxSpeed;  // Tốc độ tối đa

    private bool isMoving = false; // Ban đầu không di chuyển cho đến khi trò chơi bắt đầu

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true; // Ngăn vật thể di chuyển tự do

        // Chọn tốc độ di chuyển ngẫu nhiên trong khoảng tốc độ tối thiểu và tối đa
        moveSpeed = Random.Range(minSpeed, maxSpeed);
    }

    void Update()
    {
        // Chỉ cho phép di chuyển khi isMoving là true
        if (isMoving)
        {
            Vector2 movement = new Vector2(moveSpeed * Time.deltaTime, 0);
            Vector3 newPosition = transform.position + (Vector3)movement;
            
            // Giới hạn vị trí của đối tượng trong khoảng leftBound và rightBound
            newPosition.x = Mathf.Clamp(newPosition.x, leftBound, rightBound);
            transform.position = newPosition;
        }
    }

    // Hàm để bắt đầu di chuyển khi trò chơi bắt đầu
    public void StartMovement()
    {
        isMoving = true; // Bắt đầu cho phép di chuyển
    }

    // Hàm dừng di chuyển
    public void StopMovement()
    {
        isMoving = false; // Dừng di chuyển

        if (rb != null) // Kiểm tra xem rb đã được khởi tạo chưa
        {
            rb.velocity = Vector2.zero; // Đặt vận tốc về 0 để đảm bảo vật thể dừng lại
        }
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Kiểm tra va chạm với người chơi
        {
            StopMovement(); // Dừng chuyển động khi va chạm với người chơi
        }
    }
}
