using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_Movement : Card
{
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
        target.GetComponent<HeroMovement>().enabled = true;
    }
}
