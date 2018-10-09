using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyKiter : Enemy
{
    public override bool Act()
    {
        if (!base.Act())
            return false;

        _target = Target_ClosestHero();

        if (Check_TargetInRange())
        {
            if (Check_InHeroRange())
                Action_MoveAwayFromTarget();
            else
                Action_Attack();
        }
        else
            Action_MoveTowardsTarget();

        Invoke("EndAction", 1f);
        return true;
    }
}