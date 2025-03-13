using UnityEngine;

public class ChangeToy : MonoBehaviour
{
    public GameObject[] toys; // Danh sách đồ chơi
    private GameObject currentToy; // Đồ chơi đang cầm
    private const string TOY_KEY = "SelectedToy"; // Khóa lưu dữ liệu

    private void Start()
    {
        LoadToy(); // Load đồ chơi khi game khởi động
    }

    public void WearToy(int toyIndex)
    {
        // Ẩn đồ chơi cũ
        if (currentToy != null)
        {
            currentToy.SetActive(false);
        }

        // Hiện đồ chơi mới
        if (toyIndex >= 0 && toyIndex < toys.Length)
        {
            currentToy = toys[toyIndex];
            currentToy.SetActive(true);

            // Lưu đồ chơi đã chọn vào PlayerPrefs
            PlayerPrefs.SetInt(TOY_KEY, toyIndex);
            PlayerPrefs.Save();
        }
    }

    public void RemoveToy()
    {
        if (currentToy != null)
        {
            currentToy.SetActive(false);
            currentToy = null;
            PlayerPrefs.DeleteKey(TOY_KEY); // Xóa dữ liệu đã lưu
        }
    }

    private void LoadToy()
    {
        int savedToyIndex = PlayerPrefs.GetInt(TOY_KEY, -1);
        if (savedToyIndex >= 0 && savedToyIndex < toys.Length)
        {
            WearToy(savedToyIndex);
        }
    }
}
