using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public bool active;
    protected SpriteRenderer _sprite;

    ///////////////////////////////////////////////////////////////////////////////

    /// <summary>
    /// The amount of AP needed for the card to be played
    /// </summary>
    public int cost;

    ///////////////////////////////////////////////////////////////////////////////

    protected virtual void Start()
    {
        _sprite = transform.Find("Sprite").GetComponent<SpriteRenderer>();
    }

    protected virtual void Update()
    {
        if (!active)
        {
            if (GameManager.instance.ap >= cost)
                SetActive(true);
        }
        else
        {
            if (GameManager.instance.ap < cost)
                SetActive(false);
        }
    }

    public virtual void Action()
    {
        GameManager.instance.ap -= cost;
        GameManager.instance.currentState = GAME_STATE.ACTING;
        GameManager.instance.deck.Discard(this);
    }

    public virtual void Action(Vector2 target)
    {
        Action();
    }

    public virtual void Action(GameObject target)
    {
        Action();
    }

    public virtual void SetActive(bool state)
    {
        active = state;
        _sprite.color = state ? Color.white : new Color(1f, 1f, 1f, .5f);
    }
}
