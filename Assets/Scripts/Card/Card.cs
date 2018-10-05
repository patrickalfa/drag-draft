using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    protected bool m_revealed;
    protected SpriteRenderer _renderer;

    ///////////////////////////////////////////////////////////////////////////////

    /// <summary>
    /// The amount of AP needed for the card to be played
    /// </summary>
    public int cost;
    /// <summary>
    /// Sprites of the respective face of the card
    /// </summary>
    public Sprite frontFace, backFace;
    /// <summary>
    /// Current orientation of the card
    /// </summary>
    public bool revealed
    {
        get
        {
            return m_revealed;
        }
        set
        {
            m_revealed = value;
            _renderer.sprite = (revealed ? frontFace : backFace);
        }
    }

    ///////////////////////////////////////////////////////////////////////////////

    public virtual void Action()
    {
        GameManager.instance.currentState = GAME_STATE.ACTING;
        GameManager.instance.deck.Discard(this);
    }

    public virtual void Action(Vector2 target)
    {
        GameManager.instance.currentState = GAME_STATE.ACTING;
        GameManager.instance.deck.Discard(this);
    }

    public virtual void Action(GameObject target)
    {
        GameManager.instance.currentState = GAME_STATE.ACTING;
        GameManager.instance.deck.Discard(this);
    }
}
