using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HeroAttack : Targetable
{
    protected Hero _hero;

    protected override void Start()
    {
        base.Start();

        _hero = GetComponent<Hero>();
        raisedSortingOrder = 9;
        targetType = TARGET_TYPE.ENEMY;
        temporary = false;
        range = _hero.range;
        lineColor = new Color(.25f, .55f, 1f, 1f);

        GameManager.instance.SpotlightHero(_hero, true);
    }

    protected override void Update()
    {
        base.Update();

        if (!dragging)
            HighlightHero();
    }

    protected override void Action()
    {
        GameManager.instance.SpotlightHero(_hero, false);
        GameManager.instance.currentState = GAME_STATE.PLANNING;

        Invoke("DoDamage", .1f);

        base.Action();
    }

    protected virtual void DoDamage()
    {
        _targetObj.GetComponent<IDamageable>().TakeDamage(_hero.damage);
    }

    protected void HighlightHero()
    {
        TargetManager.instance.size = 1.25f;
        TargetManager.instance.color = new Color(.25f, .55f, 1f, .75f);
        TargetManager.instance.sortingOrder = -1;
        TargetManager.instance.DrawMarker(_transform.position);
    }
}