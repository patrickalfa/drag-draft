using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class CardDrag : Draggable
{
    private Card _card;

    #region Interface Implementations

    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);

        ResetPosition();
    }

    #endregion

    protected override void Start()
    {
        base.Start();

        _card = GetComponent<Card>();
    }
}