using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Collider2D))]
public class Tooltip : MonoBehaviour, IBeginDragHandler, IPointerDownHandler, IPointerUpHandler
{
    #region Interface Implementations

    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        CancelInvoke("Hold");
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        Invoke("Hold", .5f);
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        CancelInvoke("Hold");
    }

    #endregion

    protected virtual void Hold()
    {
    }
}