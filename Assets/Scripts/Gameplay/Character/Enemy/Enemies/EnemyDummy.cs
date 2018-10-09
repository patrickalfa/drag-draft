using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyDummy : Enemy
{
    public override bool Act()
    {
        if (!base.Act())
            return false;

        _sprite.transform.DOBlendableRotateBy(Vector3.forward * 180f, 1f);

        Invoke("EndAction", 1f);
        return true;
    }
}