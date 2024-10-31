using UnityEngine;

public class movestick : MonoBehaviour
{
    public float rightSpeed; // Tốc độ di chuyển sang phải
    public float leftSpeed; // Tốc độ rút về bên trái
    public float leftLimit; // Giới hạn bên trái
    public float rightLimit; // Giới hạn bên phải

    private bool movingRight = true; // Biến để kiểm tra hướng di chuyển
    private bool finishedMoving = false; // Biến kiểm tra xem đã hoàn thành di chuyển hay chưa

    void Update()
    {
        if (finishedMoving) return; // Nếu đã di chuyển xong thì dừng lại

        if (movingRight)
        {
            // Di chuyển sang phải với tốc độ `rightSpeed`
            transform.Translate(Vector3.right * rightSpeed * Time.deltaTime);

            // Kiểm tra nếu chạm giới hạn bên phải
            if (transform.position.x >= rightLimit)
            {
                movingRight = false; // Đổi hướng sang trái
            }
        }
        else
        {
            // Di chuyển sang trái với tốc độ `leftSpeed`
            transform.Translate(Vector3.left * leftSpeed * Time.deltaTime);

            // Kiểm tra nếu chạm giới hạn bên trái
            if (transform.position.x <= leftLimit)
            {
                finishedMoving = true; // Đã di chuyển xong, ngừng việc cập nhật
            }
        }
    }
}
