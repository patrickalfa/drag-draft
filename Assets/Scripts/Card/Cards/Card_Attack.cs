using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_Attack : Card
{
    private void Start()
    {
        GetComponent<CardDrag>().targetType = TARGET_TYPE.HERO; //DEBUG
    }

    public override void Action(GameObject target)
    {
        base.Action(target);
        target.GetComponent<HeroAttack>().enabled = true;
    }
}
