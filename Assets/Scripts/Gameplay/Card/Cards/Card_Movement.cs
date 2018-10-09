using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_Movement : Card
{
    private HeroMovement _cardAction;

    protected override void Start()
    {
        base.Start();

        cost = 1;

        GetComponent<CardDrag>().targetType = TARGET_TYPE.HERO;
        GetComponent<CardDrag>().ownerType = HERO_TYPE.NONE;
    }

    public override void Action(GameObject target)
    {
        base.Action(target);

        _cardAction = target.GetComponent<HeroMovement>();
        _cardAction.enabled = true;
    }

    public override void Cancel()
    {
        base.Cancel();
        _cardAction.enabled = false;
    }
}
