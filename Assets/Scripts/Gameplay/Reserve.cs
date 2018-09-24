using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class Reserve : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool _over = false;

    #region Interface Implementations

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        _over = true;
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        _over = false;

        if (GameManager.instance.currentState == GAME_STATE.PLANNING)
            GameManager.instance.deck.Draw();
    }

    #endregion

    private void Update()
    {
        if (_over && GameManager.instance.currentState == GAME_STATE.PLANNING)
        {
            TargetManager.instance.size = 1.5f;
            TargetManager.instance.color = new Color(0f, 1f, 0f, .5f);
            TargetManager.instance.sortingOrder = 11;
            TargetManager.instance.shape = TARGET_SHAPE.CIRCLE;
            TargetManager.instance.DrawMarker(transform.position);
        }
    }
}