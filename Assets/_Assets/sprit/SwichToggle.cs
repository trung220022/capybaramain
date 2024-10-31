using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SwichToggle : MonoBehaviour
{
    [SerializeField] RectTransform uiHandleRectTransform;
    [SerializeField] Color bgbgmActiveColor;
    

    Image bgbgmImage;

    Color bgbgmDefaultColor;

    Toggle toggle;

    Vector2 handlePosotion;

    void Awake()
    {
        toggle = GetComponent<Toggle>();

        handlePosotion = uiHandleRectTransform.anchoredPosition;

        bgbgmImage = uiHandleRectTransform.parent.GetComponent<Image>();
        
        bgbgmDefaultColor = bgbgmImage.color;
        

        toggle.onValueChanged.AddListener(OnSwitch);

        if (toggle.isOn)
            OnSwitch (true);
    }

    void OnSwitch(bool on)
    {
        uiHandleRectTransform.anchoredPosition = on ? handlePosotion * -1 : handlePosotion;
        bgbgmImage.color = on ? bgbgmActiveColor : bgbgmDefaultColor;
       
    }

    void OnDestroy()
    {
        
    }
}
