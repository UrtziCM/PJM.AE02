using UnityEngine;

public class CanvasToSafeAreaResizer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RectTransform uiTransform = GetComponent<RectTransform>();
        uiTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Screen.safeArea.width/2);
        uiTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Screen.safeArea.height/2);
        uiTransform.position = Screen.safeArea.center;
    }

}
