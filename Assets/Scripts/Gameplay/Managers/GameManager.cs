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
    /// List of the player's heroes
    /// </summary>
    public List<Hero> heroes;
    /// <summary>
    /// List of enmies in play
    /// </summary>
    public List<Enemy> enemies;
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
            UIManager.instance.ChangeText("TxtAP", ap + "/" + maxAP);
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
        deck.DrawHand();
        ap = maxAP;
    }

    private void Update()
    {
        OnStateUpdate();
        if (_lateState != currentState)
            OnStateChange();

        // DEBUG
        if (Input.GetKeyDown(KeyCode.Space))
            SkipTurn();
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

    public void SkipTurn()
    {
        if (currentState != GAME_STATE.PLANNING)
            return;

        deck.DiscardHand();

        StartCoroutine(WaitForEnemies());
        currentState = GAME_STATE.SPECTATING;
    }

    private IEnumerator WaitForEnemies()
    {
        foreach (Enemy e in enemies)
        {
            if (e.gameObject.activeSelf)
            {
                e.Act();

                while (e.acting)
                    yield return null;
            }
        }

        deck.DrawHand();
        ap = maxAP;
        currentState = GAME_STATE.PLANNING;
    }
}