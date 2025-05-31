using UnityEngine;
using UnityEngine.EventSystems;

public class DropArea : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null)
            return;

        // ✅ DragKeyword bileşeni var mı?
        DragKeyword drag = eventData.pointerDrag.GetComponent<DragKeyword>();
        if (drag == null)
        {
            Debug.LogWarning("DragKeyword component bulunamadı.");
            return;
        }

        // ❗ Eğer zaten içeride bir keyword varsa yerleştirme
        foreach (Transform child in transform)
        {
            if (child.GetComponent<DragKeyword>() != null)
            {
                Debug.Log("⚠️ Burada zaten bir kelime var.");
                return;
            }
        }

        // ✅ Yerleştir
        Transform dragged = eventData.pointerDrag.transform;
        dragged.SetParent(transform, false); // scale & anchors korunur

        RectTransform draggedRect = dragged.GetComponent<RectTransform>();
        draggedRect.anchorMin = new Vector2(0.5f, 0.5f);
        draggedRect.anchorMax = new Vector2(0.5f, 0.5f);
        draggedRect.pivot = new Vector2(0.5f, 0.5f);
        draggedRect.anchoredPosition = Vector2.zero;
        draggedRect.localScale = Vector3.one;

        // 🎯 DragKeyword’a haber ver (parametre gönderiyoruz!)
        drag.MarkAsDropped(this.transform); // ✅ burada düzeltildi

        Debug.Log("✅ Drop gerçekleşti: " + gameObject.name);
    }
}