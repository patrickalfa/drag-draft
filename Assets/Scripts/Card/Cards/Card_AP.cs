using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_AP : Card
{
    private void Start()
    {
        GetComponent<CardDrag>().targetType = TARGET_TYPE.BOARD;
        GetComponent<CardDrag>().ownerType = HERO_TYPE.NONE;
    }

    public override void Action(Vector2 target)
    {
        base.Action(target);
        GameManager.instance.deck.Draw();
        //TODO: Increase AP
    }
}