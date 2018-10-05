using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_Defense : Card
{
    private void Start()
    {
        GetComponent<CardDrag>().targetType = TARGET_TYPE.HERO;
        GetComponent<CardDrag>().ownerType = HERO_TYPE.NONE;
    }

    public override void Action(GameObject target)
    {
        if (target.GetComponent<StatusHeroDefending>())
            GetComponent<CardDrag>().ResetPosition();
        else
        {
            base.Action(target);
            target.AddComponent<StatusHeroDefending>();
        }
    }
}