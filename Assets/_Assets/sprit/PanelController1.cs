using UnityEngine;

public class PanelController1 : MonoBehaviour
{
    public GameObject[] pages; // Mảng chứa các trang
    private int currentPageIndex = 0; // Chỉ số trang hiện tại

    void Start()
    {
        ShowPage(currentPageIndex); // Hiện trang đầu tiên
    }

    public void ShowNextPage()
    {
        if (currentPageIndex < pages.Length - 1)
        {
            currentPageIndex++; // Tăng chỉ số để chuyển sang trang tiếp theo
            ShowPage(currentPageIndex);
        }
    }

    public void ShowPreviousPage()
    {
        if (currentPageIndex > 0)
        {
            currentPageIndex--; // Giảm chỉ số để quay lại trang trước
            ShowPage(currentPageIndex);
        }
    }

    private void ShowPage(int index)
    {
        // Ẩn tất cả các trang
        foreach (GameObject page in pages)
        {
            page.SetActive(false);
        }
        // Hiện trang hiện tại
        pages[index].SetActive(true);
    }

    public void SwitchToPanel(int newIndex)
    {
        if (newIndex >= 0 && newIndex < pages.Length)
        {
            ShowPage(newIndex); // Hiện panel mới
            currentPageIndex = newIndex; // Cập nhật chỉ số trang hiện tại
        }
    }
}
