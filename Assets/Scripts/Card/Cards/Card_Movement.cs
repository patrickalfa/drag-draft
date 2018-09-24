using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_Movement : Card
{
	private void Start ()
    {
        GetComponent<CardDrag>().targetType = TARGET_TYPE.HERO;
	}

    public override void Action(GameObject target)
    {
        base.Action(target);
        target.GetComponent<HeroMovement>().enabled = true;
    }
}
