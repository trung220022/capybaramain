using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UnlockButton : MonoBehaviour
{
    public Button unlockButton; // Nút mở khóa
    public Button[] buttonsToUnlock; // Danh sách button có thể unlock
    private List<int> availableIndices = new List<int>(); // Danh sách button chưa mở

    void Start()
    {
        LoadUnlockedButtons(); // Tải trạng thái button từ PlayerPrefs

        // Tạo danh sách các button chưa mở khóa
        for (int i = 0; i < buttonsToUnlock.Length; i++)
        {
            if (!buttonsToUnlock[i].gameObject.activeSelf)
            {
                availableIndices.Add(i);
            }
        }

        unlockButton.onClick.AddListener(UnlockRandomButton);
    }

    void UnlockRandomButton()
    {
        if (availableIndices.Count == 0)
        {
            Debug.Log("✅ Tất cả button đã được mở khóa!");
            return;
        }

        // Kiểm tra nếu đủ vàng để trừ
        if (!ScoreCoin.instance.SpendCoins(50))
        {
            Debug.Log("❌ Không đủ vàng để mở khóa!");
            return;
        }

        // Mở khóa button ngẫu nhiên
        int randomIndex = Random.Range(0, availableIndices.Count);
        int buttonIndex = availableIndices[randomIndex];

        buttonsToUnlock[buttonIndex].gameObject.SetActive(true);
        PlayerPrefs.SetInt("Button_" + buttonIndex, 1);
        PlayerPrefs.Save();

        availableIndices.RemoveAt(randomIndex);
    }

    void LoadUnlockedButtons()
    {
        for (int i = 0; i < buttonsToUnlock.Length; i++)
        {
            if (PlayerPrefs.GetInt("Button_" + i, 0) == 1)
            {
                buttonsToUnlock[i].gameObject.SetActive(true);
            }
            else
            {
                buttonsToUnlock[i].gameObject.SetActive(false);
            }
        }
    }
}
