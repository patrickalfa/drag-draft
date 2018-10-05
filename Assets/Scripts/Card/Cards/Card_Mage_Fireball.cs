using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_Mage_Fireball : Card
{
    private void Start()
    {
        GetComponent<CardDrag>().targetType = TARGET_TYPE.HERO;
        GetComponent<CardDrag>().ownerType = HERO_TYPE.MAGE;
    }

    public override void Action(GameObject target)
    {
        base.Action(target);
        target.AddComponent<MageFireball>();
    }
}