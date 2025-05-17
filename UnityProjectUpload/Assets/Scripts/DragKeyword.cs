using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragKeyword : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    private Transform originalParent;
    private Vector2 originalPosition;

    private bool droppedOnValidArea = false;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GameObject.FindObjectOfType<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        originalPosition = rectTransform.anchoredPosition;

        transform.SetParent(canvas.transform);
        canvasGroup.blocksRaycasts = false;
        droppedOnValidArea = false; // Her drag başlangıcında resetle
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;

        // Eğer geçerli bir drop alanına bırakılmadıysa, geri dön
        if (!droppedOnValidArea)
        {
            transform.SetParent(originalParent);
            rectTransform.anchoredPosition = originalPosition;
        }
    }

    // DropArea'dan buraya çağrılacak
    public void MarkAsDropped()
    {
        droppedOnValidArea = true;
    }
}
