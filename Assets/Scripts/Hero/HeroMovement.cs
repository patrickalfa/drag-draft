using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class HeroMovement : Draggable
{
    private Hero _hero;

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
            DrawLine();
        else
            HighlightHero();
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);

        GameManager.instance.SpotlightHero(_hero, false);
        GameManager.instance.currentState = GAME_STATE.PLANNING;

        Destroy(this);
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

    private void DrawLine()
    {
        float distance = Vector3.Distance(_startPosition, _transform.position);

        Color newColor = Color.white;
        if (distance > (_hero.speed - 1f))
            newColor = Color.Lerp(Color.yellow, Color.red, distance - (_hero.speed - 1f));
        else if (distance > (_hero.speed - 2f))
            newColor = Color.Lerp(Color.white, Color.yellow, distance - (_hero.speed - 2f));

        LineManager.instance.size = .1f;
        LineManager.instance.delta = .25f;
        LineManager.instance.color = newColor;
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
}