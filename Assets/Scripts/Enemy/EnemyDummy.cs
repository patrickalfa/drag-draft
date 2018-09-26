using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyDummy : Enemy
{
    public override void Act()
    {
        base.Act();
        _sprite.transform.DOBlendableRotateBy(Vector3.forward * 180f, 1f);
        Invoke("EndAction", 1f);
    }
}