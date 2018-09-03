using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;

public class DeckManager : MonoBehaviour
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
    /// Y-Position of the player's hand
    /// </summary>
    private float _yPos = -4f;
    private float _timeToDraw = .5f;

    ///////////////////////////////////////////////////////////////////////////////

    /// <summary>
    /// Draws a card from the reserve pile to the hand of the player
    /// </summary>
    public void Draw()
    {
        if (reserve.Count > 0 && hand.Count < 5)
        {
            GameManager.instance.currentState = GAME_STATE.DRAWING;
            StopCoroutine("WaitForDrawToComplete");
            StartCoroutine("WaitForDrawToComplete");

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

        card.transform.DOKill();
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
            Vector3 newPos = Vector3.up * _yPos;
            newPos.x = -(dist * count * .5f) + ((i + .5f) * dist);
            hand[i].DOComplete();
            hand[i].transform.DOMove(newPos, .5f);
        }
    }

    /// <summary>
    /// Lock the cards in the player's hand to prevent they from playing them
    /// </summary>
    /// <param name="retreat">Whether the card will retreat or not</param>
    public void LockHand(bool retreat)
    {
        foreach (Card c in hand)
            c.GetComponent<Collider2D>().enabled = false;

        if (retreat)
        {
            _yPos = -5f;
            RearrangeHand();
        }
    }

    /// <summary>
    /// Unlock the cards in the player's hand to allow they from playing them
    /// </summary>
    public void UnlockHand()
    {
        foreach (Card c in hand)
            c.GetComponent<Collider2D>().enabled = true;

        _yPos = -4f;
        RearrangeHand();
    }

    private IEnumerator WaitForDrawToComplete()
    {
        yield return new WaitForSeconds(_timeToDraw);
        GameManager.instance.currentState = GAME_STATE.PLANNING;
    }
}