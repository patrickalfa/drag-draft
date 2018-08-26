using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class HeroMover : Draggable
{
    private Hero _hero;

    protected override void Start()
    {
        base.Start();

        _hero = GetComponent<Hero>();
    }

    protected override void Update()
    {
        base.Update();

        if (dragging)
            UpdateLine();
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

    private void UpdateLine()
    {
        float distance = Vector3.Distance(_startPosition, _transform.position);

        Color newColor = Color.white;
        if (distance > (_hero.speed - 1f))
            newColor = Color.Lerp(Color.yellow, Color.red, distance - (_hero.speed - 1f));
        else if (distance > (_hero.speed - 2f))
            newColor = Color.Lerp(Color.white, Color.yellow, distance - (_hero.speed - 2f));

        LineManager.Instance.Size = .1f;
        LineManager.Instance.Delta = .25f;
        LineManager.Instance.color = newColor;
        LineManager.Instance.sortingOrder = 8;
        LineManager.Instance.DrawDottedLine(_startPosition, _transform.position);
    }
}