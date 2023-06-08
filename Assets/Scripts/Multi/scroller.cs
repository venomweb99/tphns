using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scroller : MonoBehaviour
{
    public void ScrollToBottom()
    {
        
        ScrollRect scrollRect = GetComponent<ScrollRect>();
    scrollRect.verticalNormalizedPosition = 0f;

    Canvas.ForceUpdateCanvases();
    scrollRect.verticalScrollbar.value = 0f;

    }
}
