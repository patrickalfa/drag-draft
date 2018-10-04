using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Enemy : Character
{
    public bool acting = false;

    protected Transform _target;

    public virtual void Act()
    {
        acting = true;
    }

    public virtual void EndAction()
    {
        acting = false;
    }

    ///// TARGETS /////

    protected Transform Target_ClosestHero()
    {
        Hero closest = null;
        float distance = -1f;

        foreach (Hero h in GameManager.instance.heroes)
        {
            if (!h.gameObject.activeSelf)
                continue;

            float d = Vector2.Distance(_transform.position, h.transform.position);

            if (!closest || d < distance)
            {
                closest = h;
                distance = d;
            }
        }

        return closest.transform;
    }

    ///// CHECKS /////

    protected bool Check_TargetInRange()
    {
        float distance = Vector2.Distance(_transform.position, _target.position);
        return distance <= (range + .5f);
    }

    protected bool Check_InHeroRange()
    {
        Transform closest = Target_ClosestHero();

        float distance = Vector2.Distance(_transform.position, closest.position);
        return distance < (closest.GetComponent<Hero>().range);
    }

    ///// ACTIONS /////

    protected void Action_Attack()
    {
        Vector3 _pos = _transform.position;
        _transform.DOMove(_target.position, .1f).OnComplete(() =>
        {
            _transform.DOMove(_pos, .1f);
            _target.GetComponent<IDamageable>().TakeDamage(damage);
        });
    }

    protected void Action_MoveTowardsTarget()
    {
        float distance = Vector2.Distance(_transform.position, _target.position);

        Vector3 newPos;
        if ((distance - range) > speed)
        {
            newPos = Vector3.MoveTowards(_transform.position, _target.position, speed);
        }
        else
            newPos = Vector3.MoveTowards(_transform.position, _target.position, (distance - range));

        _transform.DOMove(newPos, .5f);
    }

    protected void Action_MoveAwayFromTarget()
    {
        Vector3 newDirection = (_transform.position - _target.position).normalized;
        Vector3 newPos = _target.position + (newDirection * range);

        _transform.DOMove(newPos, .5f);
    }
}