using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_Archer_DoubleShot : Card
{
    private ArcherDoubleShot _cardAction;

    protected override void Start()
    {
        base.Start();

        cost = 1;

        GetComponent<CardDrag>().targetType = TARGET_TYPE.HERO;
        GetComponent<CardDrag>().ownerType = HERO_TYPE.ARCHER;
    }

    public override void Action(GameObject target)
    {
        base.Action(target);
        _cardAction = target.AddComponent<ArcherDoubleShot>();
    }

    public override void Cancel()
    {
        base.Cancel();
        Destroy(_cardAction);
    }
}