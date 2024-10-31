using UnityEngine;

public class ShieldController : MonoBehaviour
{
    public GameObject shieldObject; // Tham chiếu đến đối tượng khiên

    private void Start()
    {
        shieldObject.SetActive(false); // Ẩn khiên khi bắt đầu
    }

    public void ShowShield()
    {
        shieldObject.SetActive(true); // Hiển thị khiên
    }

    public void HideShield()
    {
        shieldObject.SetActive(false); // Ẩn khiên
    }
}
