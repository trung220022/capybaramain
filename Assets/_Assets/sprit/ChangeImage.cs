using UnityEngine;

public class ChangeImage : MonoBehaviour
{
    public SpriteRenderer playerSpriteRenderer; // SpriteRenderer của đối tượng người chơi
    public Sprite[] sprites; // Mảng chứa các sprite

    private const string outfitKey = "SelectedOutfit"; // Khóa lưu trang phục

    void Start()
    {
        // Tải lại trang phục đã lưu khi bắt đầu game
        int outfitIndex = PlayerPrefs.GetInt(outfitKey, 0); // Mặc định là trang phục đầu tiên
        ChangePlayerImage(outfitIndex); // Thay đổi sprite khi vào game
    }

    public void ChangePlayerImage(int index)
    {
        if (index >= 0 && index < sprites.Length)
        {
            playerSpriteRenderer.sprite = sprites[index]; // Thay đổi sprite
            PlayerPrefs.SetInt(outfitKey, index); // Lưu chỉ số trang phục
            PlayerPrefs.Save(); // Lưu lại các thay đổi
        }
        else
        { 
            Debug.LogError("Skin chưa mở khóa!");
        }
    }
}
