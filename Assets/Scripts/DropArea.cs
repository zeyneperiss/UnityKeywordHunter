using UnityEngine;
using UnityEngine.EventSystems;

public class DropArea : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null)
            return;

        // ❗ Sadece zaten kelime varsa engelle
        foreach (Transform child in transform)
        {
            if (child.GetComponent<DragKeyword>() != null)
            {
                Debug.Log("Zaten burada bir kelime var!");
                return;
            }
        }

        // ✅ Koymaya izin ver
        eventData.pointerDrag.transform.SetParent(this.transform);
        eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

        // DragKeyword’a bilgi ver
        DragKeyword drag = eventData.pointerDrag.GetComponent<DragKeyword>();
        if (drag != null)
        {
            drag.MarkAsDropped();
        }

        Debug.Log("Drop gerçekleşti: " + gameObject.name);
    }
}
