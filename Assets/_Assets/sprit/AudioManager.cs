using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip backgroundMusic; // Nhạc nền
    public AudioClip startGameMusic;  // Nhạc khi bắt đầu trò chơi
    public AudioClip buttonClickSound; // Âm thanh khi nhấn nút
    public AudioClip buttonGoSound; // Âm thanh khi nhấn nút Go
    public AudioClip hitObstacleSound; // Âm thanh khi va chạm chướng ngại vật 
    public AudioClip roiDieSound; // Âm thanh khi va chạm chướng ngại vật 
    public AudioClip vaChamDieSound; // Âm thanh khi va chạm chướng ngại vật

    public Toggle toggleBackgroundMusic; // Toggle cho nhạc nền
    public Toggle toggleSoundEffects; // Toggle cho âm thanh hiệu ứng

    private const string BackgroundMusicKey = "BackgroundMusic"; // Khóa lưu trạng thái nhạc nền
    private const string SoundEffectsKey = "SoundEffects"; // Khóa lưu trạng thái âm thanh hiệu ứng

    private bool isBackgroundMusicOn = true; // Trạng thái nhạc nền
    private bool isSoundEffectsOn = true; // Trạng thái âm thanh hiệu ứng

    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // Lấy AudioSource

        // Lấy trạng thái từ PlayerPrefs
        isBackgroundMusicOn = PlayerPrefs.GetInt(BackgroundMusicKey, 1) == 1;
        isSoundEffectsOn = PlayerPrefs.GetInt(SoundEffectsKey, 1) == 1;

        // Cập nhật toggle
        toggleBackgroundMusic.isOn = isBackgroundMusicOn;
        toggleSoundEffects.isOn = isSoundEffectsOn;

        // Phát nhạc nền nếu được bật
        if (isBackgroundMusicOn)
        {
            PlayBackgroundMusic();
        }

        // Đăng ký sự kiện cho các Toggle
        toggleBackgroundMusic.onValueChanged.AddListener(delegate { ToggleBackgroundMusic(toggleBackgroundMusic.isOn); });
        toggleSoundEffects.onValueChanged.AddListener(delegate { ToggleSoundEffects(toggleSoundEffects.isOn); });
    }

    public void ToggleBackgroundMusic(bool isOn)
    {
        isBackgroundMusicOn = isOn;
        PlayerPrefs.SetInt(BackgroundMusicKey, isOn ? 1 : 0); // Lưu trạng thái vào PlayerPrefs

        if (isOn)
        {
            PlayBackgroundMusic(); // Bật nhạc nền
        }
        else
        {
            audioSource.Stop(); // Tắt nhạc nền
        }
    }

    public void ToggleSoundEffects(bool isOn)
    {
        isSoundEffectsOn = isOn;
        PlayerPrefs.SetInt(SoundEffectsKey, isOn ? 1 : 0); // Lưu trạng thái vào PlayerPrefs
    }

    public void PlayBackgroundMusic()
    {
        if (backgroundMusic != null && isBackgroundMusicOn) // Kiểm tra nếu nhạc nền đang bật
        {
            audioSource.clip = backgroundMusic;
            audioSource.Play();
        }
    }

    public void PlayStartGameMusic()
    {
        if (isBackgroundMusicOn && startGameMusic != null)
        {
            audioSource.Stop(); // Dừng nhạc nền
            audioSource.clip = startGameMusic; // Chọn nhạc mới
            audioSource.Play(); // Phát nhạc
        }
    }

    public void PlayButtonClickSound()
    {
        if (isSoundEffectsOn && buttonClickSound != null)
        {
            audioSource.PlayOneShot(buttonClickSound, 1.0f); // Phát âm thanh khi nhấn nút
        }
    }

    public void PlayButtonGoSound()
    {
        if (isSoundEffectsOn && buttonGoSound != null)
        {
            audioSource.PlayOneShot(buttonGoSound, 1.0f); // Phát âm thanh khi nhấn nút Go
        }
    }

    public void PlayHitObstacleSound()
    {
        if (isSoundEffectsOn && hitObstacleSound != null)
        {
            audioSource.PlayOneShot(hitObstacleSound, 1.0f); // Phát âm thanh khi va chạm chướng ngại vật
        }
    }

    public void PlayRoiDieSound()
    {
        if (isSoundEffectsOn && roiDieSound != null)
        {
            audioSource.PlayOneShot(roiDieSound, 1.0f); // Phát âm thanh khi va chạm chướng ngại vật
        }
    }

    public void PlayVaChamDieSound()
    {
        if (isSoundEffectsOn && vaChamDieSound != null)
        {
            audioSource.PlayOneShot(vaChamDieSound, 1.0f); // Phát âm thanh khi va chạm chướng ngại vật
        }
    }

    // Đảm bảo lưu trạng thái khi thoát game
    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt(BackgroundMusicKey, isBackgroundMusicOn ? 1 : 0);
        PlayerPrefs.SetInt(SoundEffectsKey, isSoundEffectsOn ? 1 : 0);
        PlayerPrefs.Save();
    }
}
