using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_AP : Card
{
    protected override void Start()
    {
        base.Start();

        cost = 0;

        GetComponent<CardDrag>().targetType = TARGET_TYPE.BOARD;
        GetComponent<CardDrag>().ownerType = HERO_TYPE.NONE;
    }

    public override void Action(Vector2 target)
    {
        base.Action(target);
        GameManager.instance.deck.Draw();
        GameManager.instance.ap++;
    }
}