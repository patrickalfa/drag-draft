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
            GraphicsManager.target.size = 1.5f;
            GraphicsManager.target.color = new Color(0f, 1f, 0f, .5f);
            GraphicsManager.target.sortingOrder = 11;
            GraphicsManager.target.shape = TARGET_SHAPE.CIRCLE;
            GraphicsManager.target.DrawMarker(transform.position);
        }
    }
}