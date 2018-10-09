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
        GraphicsManager.line.size = .15f;
        GraphicsManager.line.delta = .25f;
        GraphicsManager.line.color = new Color(1f, .64f, .25f, 1f);
        GraphicsManager.line.sortingOrder = 8;
        GraphicsManager.line.DrawDottedLine(_startPosition, _transform.position);
    }

    protected override void HighlightHero()
    {
        GraphicsManager.target.size = 1.25f;
        GraphicsManager.target.color = new Color(1f, .64f, .25f, .75f);
        GraphicsManager.target.sortingOrder = -1;
        GraphicsManager.target.DrawMarker(_startPosition);
    }

    private void CheckTarget(Collider2D col)
    {
        if (col)
        {
            _target = col.transform;

            GraphicsManager.target.size = 1.25f;
            GraphicsManager.target.sortingOrder = -1;
            GraphicsManager.target.shape = TARGET_SHAPE.CIRCLE;
            GraphicsManager.target.color = new Color(1f, .64f, .25f, .5f);
            GraphicsManager.target.DrawMarker(_target.transform.position);

            Vector3 direction = (_target.position - _startPosition).normalized;
            _knockedPos = _target.position + (direction * 1.5f);

            GraphicsManager.line.size = .15f;
            GraphicsManager.line.delta = .25f;
            GraphicsManager.line.color = new Color(1f, .64f, .25f, .5f);
            GraphicsManager.line.sortingOrder = 8;
            GraphicsManager.line.DrawDottedLine(_target.position, _knockedPos);
            GraphicsManager.target.DrawMarker(_knockedPos);
        }
        else
            _target = null;
    }

    protected override void CheckValidMovement()
    {
        isValid = true;
        Collider2D[] cols = Physics2D.OverlapCircleAll(_transform.position, .5f, LayerMask.GetMask("Hero", "Enemy"));

        bool foundTarget = false;
        foreach (Collider2D col in cols)
        {
            if (col)
            {
                if (col.GetComponent<Enemy>() && !foundTarget)
                {
                    foundTarget = true;
                    CheckTarget(col);
                }
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