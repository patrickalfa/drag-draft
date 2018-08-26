using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Deck : MonoBehaviour
{
    /// <summary>
    /// Reserve/draw pile of the player
    /// </summary>
    public List<Card> reserve;
    /// <summary>
    /// Discard pile of the player
    /// </summary>
    public List<Card> discard;
    /// <summary>
    /// Current hand of the player
    /// </summary>
    public List<Card> hand;

    ///////////////////////////////////////////////////////////////////////////////

    /// <summary>
    /// Draws a card from the reserve pile to the hand of the player
    /// </summary>
    public void Draw()
    {
        if (reserve.Count > 0)
        {
            hand.Add(reserve[0]);
            reserve.RemoveAt(0);
        }
    }

    /// <summary>
    /// Shuffles the reserve/draw pile of the player
    /// </summary>
    public void Shuffle()
    {
        reserve = reserve.OrderBy(x => Random.value).ToList();
    }
}
