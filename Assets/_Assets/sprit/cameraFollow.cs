using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;  // Nhân vật chính
    public Vector3 offset;    // Khoảng cách giữa camera và nhân vật
    public float smoothSpeed = 0.125f;  // Độ mượt khi di chuyển

    void LateUpdate()
    {
        // Lấy vị trí hiện tại của camera
        Vector3 currentPosition = transform.position;

        // Chỉ thay đổi giá trị trục X và Y dựa trên vị trí của nhân vật và offset, giữ nguyên trục Z (vì là game 2D)
        Vector3 desiredPosition = new Vector3(currentPosition.x, target.position.y + offset.y, currentPosition.z);

        // Di chuyển camera mượt mà tới vị trí mới
        Vector3 smoothedPosition = Vector3.Lerp(currentPosition, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
