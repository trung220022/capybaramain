using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameObject panel; // Tham chiếu tới Panel (bảng UI)
    public static bool isPanelOpen = false; // Biến tĩnh để kiểm tra trạng thái tab đang mở

    // Hàm để mở bảng
    public void ShowPanel()
    {
        // Kiểm tra nếu có bảng khác đang mở
        if (isPanelOpen)
        {
            return; // Không mở bảng mới nếu có bảng khác đang mở
        }

        // Mở bảng và gán nó là bảng hiện tại
        panel.SetActive(true);
        isPanelOpen = true; // Đánh dấu là bảng đang mở
    }

    // Hàm để đóng bảng
    public void ClosePanel()
    {
        // Nếu bảng hiện tại đang mở thì đóng nó
        if (panel.activeSelf)
        {
            panel.SetActive(false); // Ẩn bảng
            isPanelOpen = false; // Đánh dấu là không có bảng nào mở
        }
    }
}
