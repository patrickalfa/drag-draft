using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_Mage_Freeze : Card
{
    private MageFreeze _cardAction;

    protected override void Start()
    {
        base.Start();

        cost = 1;

        GetComponent<CardDrag>().targetType = TARGET_TYPE.HERO;
        GetComponent<CardDrag>().ownerType = HERO_TYPE.MAGE;
    }

    public override void Action(GameObject target)
    {
        base.Action(target);
        _cardAction = target.AddComponent<MageFreeze>();
    }

    public override void Cancel()
    {
        base.Cancel();
        Destroy(_cardAction);
    }
}