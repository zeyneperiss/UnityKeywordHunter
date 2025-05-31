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
    private Transform targetDropArea;

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
        droppedOnValidArea = false;
        targetDropArea = null;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;

        if (!droppedOnValidArea || targetDropArea == null)
        {
            // Geçersiz bırakıldıysa geri dön
            transform.SetParent(originalParent);
            rectTransform.anchoredPosition = originalPosition;
        }
        else
        {
            // ✅ Drop alanına yerleştir
            transform.SetParent(targetDropArea);
            rectTransform.anchoredPosition = Vector2.zero;
        }
    }

    // DropArea'dan buraya çağrılacak
    public void MarkAsDropped(Transform dropArea)
    {
        droppedOnValidArea = true;
        targetDropArea = dropArea;
    }
}