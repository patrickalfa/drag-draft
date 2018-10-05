using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MageFireball : Targetable
{
    protected Hero _hero;
    protected IDamageable[] _enemies;

    protected override void Start()
    {
        base.Start();

        _hero = GetComponent<Hero>();
        raisedSortingOrder = 9;
        targetType = TARGET_TYPE.AREA_ENEMY;
        temporary = true;
        range = 3f;
        size = 3f;
        lineColor = new Color(.3f, .38f, 72f, 1f);

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

        _enemies = new IDamageable[_targetMultiple.Length];
        for (int i = 0; i < _targetMultiple.Length; i++)
            _enemies[i] = _targetMultiple[i].GetComponent<IDamageable>();

        Explode();
        Invoke("DoDamage", .1f);
    }

    protected virtual void DoDamage()
    {
        foreach (IDamageable e in _enemies)
            e.TakeDamage(2);
    }

    protected void HighlightHero()
    {
        TargetManager.instance.size = 1.25f;
        TargetManager.instance.color = new Color(lineColor.r, lineColor.g, lineColor.b, .75f);
        TargetManager.instance.sortingOrder = -1;
        TargetManager.instance.DrawMarker(_transform.position);
    }

    protected void Explode()
    {
        GameObject ball = new GameObject("Fireball");
        ball.AddComponent<SpriteRenderer>().sprite = TargetManager.instance.sprites[(int)TARGET_SHAPE.CIRCLE]; // DEBUG
        ball.GetComponent<SpriteRenderer>().sortingOrder = raisedSortingOrder;
        ball.GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 0f, .75f); // DEBUG
        ball.transform.position = _targetPos;
        ball.transform.localScale = Vector3.zero;
        ball.transform.DOScale(size, .1f).SetEase(Ease.OutBounce).OnComplete(() =>
        {
            ball.GetComponent<SpriteRenderer>().DOFade(0f, .1f).OnComplete(() =>
            {
                Destroy(ball);
                base.Action();
            });
        });
    }
}