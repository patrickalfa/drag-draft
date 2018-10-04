using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_Warrior_Charge : Card
{
    private void Start()
    {
        GetComponent<CardDrag>().targetType = TARGET_TYPE.HERO;
        GetComponent<CardDrag>().ownerType = HERO_TYPE.WARRIOR;
    }

    public override void Action(GameObject target)
    {
        base.Action(target);
        target.AddComponent<WarriorCharge>();
    }
}