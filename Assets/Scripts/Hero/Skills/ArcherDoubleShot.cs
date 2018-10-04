using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ArcherDoubleShot : Targetable
{
    private Hero _hero;
    private HeroAttackRanged _attackRanged;
    private Transform _targetTransform;

    protected override void Start()
    {
        base.Start();

        _hero = GetComponent<Hero>();
        _attackRanged = GetComponent<HeroAttackRanged>();
        raisedSortingOrder = 9;
        targetType = TARGET_TYPE.ENEMY;
        temporary = true;
        range = _hero.range;
        lineColor = new Color(.3f, .56f, .29f, 1f);

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
        _targetTransform = _targetObj.transform;
        StartCoroutine(DoubleShoot());
    }

    protected virtual void DoDamage()
    {
        _targetTransform.GetComponent<IDamageable>().TakeDamage(_hero.damage);
    }

    protected void HighlightHero()
    {
        TargetManager.instance.size = 1.25f;
        TargetManager.instance.color = new Color(.3f, .56f, .29f, .75f);
        TargetManager.instance.sortingOrder = -1;
        TargetManager.instance.DrawMarker(_transform.position);
    }

    private void Shoot()
    {
        Transform p = new GameObject("Projectile", typeof(SpriteRenderer)).transform;
        p.position = _transform.position;
        p.localScale = Vector3.one * _attackRanged.projectileScale;
        p.GetComponent<SpriteRenderer>().color = Color.black; // DEBUG
        p.GetComponent<SpriteRenderer>().sprite = _attackRanged.projectileSprite;
        p.GetComponent<SpriteRenderer>().sortingOrder = _sprite.GetComponent<SpriteRenderer>().sortingOrder;

        Vector3 offset = _targetTransform.position - _transform.position;
        p.rotation = Quaternion.LookRotation(Vector3.forward, offset);

        p.DOMove(_targetTransform.position, .1f).OnComplete(() =>
        {
            Destroy(p.gameObject);
        });
    }

    private IEnumerator DoubleShoot()
    {
        Shoot();
        yield return new WaitForSeconds(.1f);
        DoDamage();
        Shoot();
        yield return new WaitForSeconds(.1f);
        DoDamage();

        base.Action();
    }
}
