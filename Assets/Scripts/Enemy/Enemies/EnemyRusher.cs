using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyRusher : Enemy
{
    public override bool Act()
    {
        if (!base.Act())
            return false;

        _target = Target_ClosestHero();

        if (Check_TargetInRange())
            Action_Attack();
        else
            Action_MoveTowardsTarget();

        Invoke("EndAction", 1f);
        return true;
    }
}