using UnityEngine;

public class PanelController : MonoBehaviour
{
    public GameObject[] panels; // Danh sách các panel

    void Start()
    {
        ShowPanel(0); // Hiện panel đầu tiên (shop skin) ngay khi vào game
    }

    public void ShowPanel(int index)
    {
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].SetActive(i == index); // Chỉ hiện panel tại index đã chỉ định
        }
    }
}
