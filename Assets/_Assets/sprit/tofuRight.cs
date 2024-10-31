using UnityEngine;

public class tofuRight : MonoBehaviour
{
    private Rigidbody2D rb;

    public float moveSpeed;
    public float leftBound;
    public float rightBound;
    public float minSpeed = 3.3f;  // Tốc độ tối thiểu
    public float maxSpeed = 5f;     // Tốc độ tối đa

    private bool isMoving = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true; // Ngăn vật thể di chuyển tự do
        moveSpeed = Random.Range(minSpeed, maxSpeed);
    }

    void Update()
    {
        if (isMoving)
        {
            Vector2 movement = new Vector2(-moveSpeed * Time.deltaTime, 0);
            Vector3 newPosition = transform.position + (Vector3)movement;

            newPosition.x = Mathf.Clamp(newPosition.x, leftBound, rightBound);
            transform.position = newPosition;
        }
    }

    public void StopMovement()
    {
        isMoving = false;
    }
}
