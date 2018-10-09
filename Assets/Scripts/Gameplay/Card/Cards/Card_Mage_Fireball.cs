using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_Mage_Fireball : Card
{
    private MageFireball _cardAction;

    protected override void Start()
    {
        base.Start();

        cost = 2;

        GetComponent<CardDrag>().targetType = TARGET_TYPE.HERO;
        GetComponent<CardDrag>().ownerType = HERO_TYPE.MAGE;
    }

    public override void Action(GameObject target)
    {
        base.Action(target);
        _cardAction = target.AddComponent<MageFireball>();
    }

    public override void Cancel()
    {
        base.Cancel();
        Destroy(_cardAction);
    }
}