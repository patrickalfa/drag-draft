using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class HeroMovement : Draggable
{
    private Hero _hero;
    private bool isValid;

    protected override void Start()
    {
        base.Start();

        _hero = GetComponent<Hero>();
        raisedSortingOrder = 9;

        GameManager.instance.SpotlightHero(_hero, true);
    }

    protected override void Update()
    {
        base.Update();


        if (dragging)
        {
            DrawLine();
            CheckValidMovement();
        }
        else
            HighlightHero();
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);

        if (isValid)
        {
            GameManager.instance.SpotlightHero(_hero, false);
            GameManager.instance.currentState = GAME_STATE.PLANNING;
            enabled = false;
        }
        else
            ResetPosition();  
    }

    protected override void DragTo(Vector3 newPos)
    {
        if (Vector3.Distance(_startPosition, newPos) <= _hero.speed)
        {
            _transform.position = newPos;
        }
        else
        {
            Vector3 norm = (newPos - _startPosition).normalized;
            _transform.position = _startPosition + (norm * _hero.speed);
        }
    }

    protected override void ResetPosition()
    {
        base.ResetPosition();

        Color c = _sprite.GetComponent<SpriteRenderer>().color;
        c.a = 1f;
        _sprite.GetComponent<SpriteRenderer>().color = c;
    }

    private void DrawLine()
    {
        LineManager.instance.size = .15f;
        LineManager.instance.delta = .25f;
        LineManager.instance.color = new Color(.55f, 1f, .9f, 1f);
        LineManager.instance.sortingOrder = 8;
        LineManager.instance.DrawDottedLine(_startPosition, _transform.position);
    }

    private void HighlightHero()
    {
        TargetManager.instance.size = 1.25f;
        TargetManager.instance.color = new Color(.55f, 1f, .9f, .75f);
        TargetManager.instance.sortingOrder = -1;
        TargetManager.instance.DrawMarker(_startPosition);
    }

    private void CheckValidMovement()
    {
        isValid = true;
        Collider2D[] cols = Physics2D.OverlapCircleAll(_transform.position, .5f, LayerMask.GetMask("Hero", "Enemy"));

        foreach (Collider2D col in cols)
        {
            if (col && (col.gameObject != gameObject))
            {
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