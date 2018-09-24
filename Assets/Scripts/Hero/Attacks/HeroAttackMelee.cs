using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HeroAttackMelee : HeroAttack
{
    protected override void Action()
    {
        Vector3 _pos = _transform.position;
        _transform.DOMove(_targetObj.transform.position, .1f).OnComplete(() =>
        {
            _transform.DOMove(_pos, .1f);
        });

        base.Action();
    }
}