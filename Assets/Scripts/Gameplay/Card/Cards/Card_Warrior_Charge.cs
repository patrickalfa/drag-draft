using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_Warrior_Charge : Card
{
    private WarriorCharge _cardAction;

    protected override void Start()
    {
        base.Start();

        cost = 1;

        GetComponent<CardDrag>().targetType = TARGET_TYPE.HERO;
        GetComponent<CardDrag>().ownerType = HERO_TYPE.WARRIOR;
    }

    public override void Action(GameObject target)
    {
        base.Action(target);
        _cardAction = target.AddComponent<WarriorCharge>();
    }

    public override void Cancel()
    {
        base.Cancel();
        Destroy(_cardAction);
    }
}