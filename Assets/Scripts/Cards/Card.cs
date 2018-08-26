using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    private bool m_revealed;
    private SpriteRenderer _renderer;

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

    // TODO
    public void Set()
    {
    }

    // TODO
    public void Action()
    {
    }
}
