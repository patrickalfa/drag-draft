using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_Warrior_Charge : Card
{
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
        target.AddComponent<WarriorCharge>();
    }
}