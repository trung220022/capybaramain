using UnityEngine;
using UnityEngine.UI;

public class NameManager : MonoBehaviour
{
    public InputField nameInputField; // Trường nhập tên
    public Text displayNameText; // Text để hiển thị tên đã lưu
    private const string PlayerNameKey = "PlayerName"; // Khóa để lưu tên

    void Start()
    {
        // Tải tên đã lưu khi bắt đầu game
        LoadAndDisplaySavedName();
    }

    private void LoadAndDisplaySavedName()
    {
        // Kiểm tra xem có tên đã được lưu trong PlayerPrefs chưa
        if (PlayerPrefs.HasKey(PlayerNameKey))
        {
            // Lấy tên đã lưu và hiển thị trong cả inputField và displayNameText
            string savedName = PlayerPrefs.GetString(PlayerNameKey);
            nameInputField.text = savedName;  // Hiển thị tên trong inputField
            displayNameText.text = savedName; // Hiển thị tên ở bảng hiển thị tên
        }
        else
        {
            // Nếu chưa có tên, hiển thị một thông báo mặc định
            displayNameText.text = "Tên chưa được lưu";
        }
    }

    public void SaveName()
    {
        // Lấy tên từ InputField và lưu vào PlayerPrefs
        string nameToSave = nameInputField.text;

        // Nếu tên không trống, lưu và hiển thị
        if (!string.IsNullOrEmpty(nameToSave))
        {
            PlayerPrefs.SetString(PlayerNameKey, nameToSave);
            PlayerPrefs.Save(); // Lưu thay đổi

            // Cập nhật tên hiển thị ở bảng hiển thị tên
            displayNameText.text = nameToSave;
        }
    }

    // Hàm gọi khi mở bảng hiện tên (nếu bạn cần cập nhật bảng tên khi mở nó)
    public void OnOpenDisplayNamePanel()
    {
        // Khi mở bảng hiển thị tên, tải và hiển thị tên đã lưu
        LoadAndDisplaySavedName();
    }
}
