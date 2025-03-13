using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scroller : MonoBehaviour
{
    [SerializeField] private RawImage _img;
    [SerializeField] private float _scrollSpeed = 0.5f; // Điều chỉnh tốc độ cuộn

    void Update()
    {
        // Di chuyển theo trục Y (dọc)
        _img.uvRect = new Rect(_img.uvRect.position + new Vector2(0, _scrollSpeed) * Time.deltaTime, _img.uvRect.size);
    }
}