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
        GraphicsManager.target.size = 1.25f;
        GraphicsManager.target.color = new Color(lineColor.r, lineColor.g, lineColor.b, .75f);
        GraphicsManager.target.sortingOrder = -1;
        GraphicsManager.target.DrawMarker(_transform.position);
    }

    protected void Explode()
    {
        GameObject ball = new GameObject("Fireball");
        ball.AddComponent<SpriteRenderer>().sprite = GraphicsManager.target.sprites[(int)TARGET_SHAPE.CIRCLE]; // DEBUG
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