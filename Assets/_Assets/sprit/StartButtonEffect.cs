using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class StartButtonEffect : MonoBehaviour
{
    private Tween scaleTween;
    public Button startButton;
    private CanvasGroup canvasGroup;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }

        scaleTween = transform.DOScale(1.2f, 0.5f)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);

        startButton.onClick.AddListener(FadeOutButton);
    }

    private void FadeOutButton()
    {
        if (UIController.isPanelOpen) return; // Nếu Panel đang mở, không cho ấn

        if (scaleTween != null) scaleTween.Kill(); // Hủy Tween trước

        canvasGroup.DOFade(0, 0.5f).OnComplete(() =>
        {
            gameObject.SetActive(false); // Ẩn sau khi fade xong
        });
    }
}
