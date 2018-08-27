using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;

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
            RearrangeHand();
        }
    }

    /// <summary>
    /// Discard a card from the hand of the player, moving it to the discard pile 
    /// </summary>
    /// <param name="card">Card to be discarded</param>
    public void Discard(Card card)
    {
        hand.Remove(card);
        discard.Add(card);
        RearrangeHand();

        card.transform.DOComplete();
        card.transform.DOMove(Vector3.up * -10f, .5f);
    }

    /// <summary>
    /// Shuffles the reserve/draw pile of the player
    /// </summary>
    public void Shuffle()
    {
        reserve = reserve.OrderBy(x => Random.value).ToList();
    }

    /// <summary>
    /// Rearranges the of the player with the cards on the 'hand' list
    /// </summary>
    public void RearrangeHand()
    {
        int count = hand.Count;
        float dist = .75f;

        print(count);

        for (int i = 0; i < count; i++)
        {
            Vector3 newPos = Vector3.up * -4f;
            newPos.x = -(dist * count * .5f) + ((i + .5f) * dist);
            hand[i].DOComplete();
            hand[i].transform.DOMove(newPos, .5f);
        }
    }
}
