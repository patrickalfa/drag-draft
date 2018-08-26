using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int m_ap;

    ///////////////////////////////////////////////////////////////////////////////

    /// <summary>
    /// Player's deck
    /// </summary>
    public Deck deck;
    /// <summary>
    /// Player's heroes
    /// </summary>
    public List<Hero> heroes;
    /// <summary>
    /// Maximum amount of action points that the player can have
    /// </summary>
    public int maxAP;
    /// <summary>
    /// Current amount of action points the player has
    /// </summary>
    public int ap
    {
        get
        {
            return m_ap;
        }
        set
        {
            m_ap = value;
        }
    }
}
