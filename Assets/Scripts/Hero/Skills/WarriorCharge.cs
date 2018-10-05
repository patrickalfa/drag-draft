using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class WarriorCharge : HeroMovement
{
    private Transform _target;
    private Vector3 _knockedPos;

    protected override void Start()
    {
        base.Start();
        temporary = true;
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        if (isValid && _target)
        {
            _target.DOMove(_knockedPos, .1f).OnComplete(() =>
            {
                _target.GetComponent<IDamageable>().TakeDamage(_hero.damage);
            });
        }

        base.OnEndDrag(eventData);
    }

    protected override void DrawLine()
    {
        LineManager.instance.size = .15f;
        LineManager.instance.delta = .25f;
        LineManager.instance.color = new Color(1f, .64f, .25f, 1f);
        LineManager.instance.sortingOrder = 8;
        LineManager.instance.DrawDottedLine(_startPosition, _transform.position);
    }

    protected override void HighlightHero()
    {
        TargetManager.instance.size = 1.25f;
        TargetManager.instance.color = new Color(1f, .64f, .25f, .75f);
        TargetManager.instance.sortingOrder = -1;
        TargetManager.instance.DrawMarker(_startPosition);
    }

    private void CheckTarget(Collider2D col)
    {
        if (col)
        {
            _target = col.transform;

            TargetManager.instance.size = 1.25f;
            TargetManager.instance.sortingOrder = -1;
            TargetManager.instance.shape = TARGET_SHAPE.CIRCLE;
            TargetManager.instance.color = new Color(1f, .64f, .25f, .5f);
            TargetManager.instance.DrawMarker(_target.transform.position);

            Vector3 direction = (_target.position - _startPosition).normalized;
            _knockedPos = _target.position + (direction * 1.5f);

            LineManager.instance.size = .15f;
            LineManager.instance.delta = .25f;
            LineManager.instance.color = new Color(1f, .64f, .25f, .5f);
            LineManager.instance.sortingOrder = 8;
            LineManager.instance.DrawDottedLine(_target.position, _knockedPos);
            TargetManager.instance.DrawMarker(_knockedPos);
        }
        else
            _target = null;
    }

    protected override void CheckValidMovement()
    {
        isValid = true;
        Collider2D[] cols = Physics2D.OverlapCircleAll(_transform.position, .5f, LayerMask.GetMask("Hero", "Enemy"));

        foreach (Collider2D col in cols)
        {
            if (col)
            {
                if (col.GetComponent<Enemy>())
                    CheckTarget(col);
                else if (col.gameObject != gameObject)
                    isValid = false;
            }
        }

        //------------------------------------------------------------

        Color c = _sprite.GetComponent<SpriteRenderer>().color;

        if (isValid)
            c.a = 1f;
        else
            c.a = .5f;

        _sprite.GetComponent<SpriteRenderer>().color = c;
        _shadow.SetShadowActive(isValid);
    }
}