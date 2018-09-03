using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GAME_STATE
{
    DRAWING,
    PLANNING,
    ACTING,
    SPECTATING
}

public class GameManager : MonoBehaviour
{
    private int m_ap;

    ///////////////////////////////////////////////////////////////////////////////

    /// <summary>
    /// Player's deck
    /// </summary>
    public DeckManager deck;
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

    private static GameManager m_instance;
    public static GameManager instance
    {
        get
        {
            if (m_instance == null)
                m_instance = FindObjectOfType<GameManager>();
            return m_instance;
        }
    }

    ///////////////////////////////////////////////////////////////////////////////

    private GAME_STATE _lateState;
    public GAME_STATE currentState;

    ///////////////////////////////////////////////////////////////////////////////

    private void Start()
    {
        _lateState = GAME_STATE.DRAWING;
        currentState = GAME_STATE.DRAWING;

        // DEBUG
        deck.Shuffle();
        deck.Invoke("Draw", .5f);
        deck.Invoke("Draw", 1f);
        deck.Invoke("Draw", 1.5f);
        deck.Invoke("Draw", 2f);
    }

    private void Update()
    {
        OnStateUpdate();
        if (_lateState != currentState)
            OnStateChange();
    }

    private void OnStateChange()
    {
        switch (currentState)
        {
            case GAME_STATE.DRAWING:
                deck.LockHand(false);
                break;
            case GAME_STATE.PLANNING:
                deck.UnlockHand();
                break;
            case GAME_STATE.ACTING:
                deck.LockHand(true);
                break;
            case GAME_STATE.SPECTATING:
                deck.LockHand(false);
                break;
        }

        _lateState = currentState;
    }

    private void OnStateUpdate()
    {
        switch (currentState)
        {
            case GAME_STATE.DRAWING:
                break;
            case GAME_STATE.PLANNING:
                break;
            case GAME_STATE.ACTING:
                break;
            case GAME_STATE.SPECTATING:
                break;
        }
    }

    public void SpotlightHero(Hero hero, bool state)
    {
        foreach (Hero h in heroes)
        {
            if (h != hero)
            {
                if (state)
                    h.Lock();
                else
                    h.Unlock();
            } 
        }
    }
}