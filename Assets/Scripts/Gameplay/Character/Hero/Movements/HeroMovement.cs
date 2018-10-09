using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class HeroMovement : Draggable
{
    public bool temporary = false;

    protected Hero _hero;
    protected bool isValid;

    protected override void Start()
    {
        base.Start();

        _hero = GetComponent<Hero>();
        raisedSortingOrder = 9;

        GameManager.instance.SpotlightHero(_hero, true);
    }

    protected void Update()
    {
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

            if (temporary)
                Destroy(this);
            else
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

    public override void ResetPosition()
    {
        base.ResetPosition();

        Color c = _sprite.GetComponent<SpriteRenderer>().color;
        c.a = 1f;
        _sprite.GetComponent<SpriteRenderer>().color = c;
    }

    protected virtual void DrawLine()
    {
        GraphicsManager.line.size = .15f;
        GraphicsManager.line.delta = .25f;
        GraphicsManager.line.color = new Color(.55f, 1f, .9f, 1f);
        GraphicsManager.line.sortingOrder = 8;
        GraphicsManager.line.DrawDottedLine(_startPosition, _transform.position);
    }

    protected virtual void HighlightHero()
    {
        GraphicsManager.target.size = 1.25f;
        GraphicsManager.target.color = new Color(.55f, 1f, .9f, .75f);
        GraphicsManager.target.sortingOrder = -1;
        GraphicsManager.target.DrawMarker(_startPosition);
    }

    protected virtual void CheckValidMovement()
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