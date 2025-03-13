using UnityEngine;

public class ChangeHat : MonoBehaviour
{
    public GameObject[] hats; // Danh sách mũ
    private GameObject currentHat; // Mũ đang đội
    private const string HAT_KEY = "SelectedHat"; // Khóa lưu dữ liệu

    private void Start()
    {
        LoadHat(); // Load mũ khi game khởi động
    }

    public void WearHat(int hatIndex)
    {
        // Ẩn mũ cũ
        if (currentHat != null)
        {
            currentHat.SetActive(false);
        }

        // Hiện mũ mới
        if (hatIndex >= 0 && hatIndex < hats.Length)
        {
            currentHat = hats[hatIndex];
            currentHat.SetActive(true);

            // Lưu mũ đã chọn vào PlayerPrefs
            PlayerPrefs.SetInt(HAT_KEY, hatIndex);
            PlayerPrefs.Save();
        }
    }

    public void RemoveHat()
    {
        if (currentHat != null)
        {
            currentHat.SetActive(false);
            currentHat = null;
            PlayerPrefs.DeleteKey(HAT_KEY); // Xóa dữ liệu đã lưu
        }
    }

    private void LoadHat()
    {
        int savedHatIndex = PlayerPrefs.GetInt(HAT_KEY, -1);
        if (savedHatIndex >= 0 && savedHatIndex < hats.Length)
        {
            WearHat(savedHatIndex);
        }
    }
}
