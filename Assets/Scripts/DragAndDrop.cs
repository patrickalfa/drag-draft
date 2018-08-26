using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler
{
    private Transform _transform;
    private Vector3 _startPosition;
    private Vector3 _offsetToMouse;
    private float _zDistanceToCamera;

    private bool dragging = false;
    private float maxDistance = 3f;

    #region Interface Implementations

    public void OnBeginDrag(PointerEventData eventData)
    {
        _startPosition = _transform.position;
        _zDistanceToCamera = Mathf.Abs(_startPosition.z - Camera.main.transform.position.z);

        _offsetToMouse = _startPosition - Camera.main.ScreenToWorldPoint(
            new Vector3(Input.mousePosition.x, Input.mousePosition.y, _zDistanceToCamera)
        );

        dragging = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (Input.touchCount > 1)
            return;

        Vector3 _newPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
                            Input.mousePosition.y, _zDistanceToCamera)) + _offsetToMouse;
        DragTo(_newPos);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _offsetToMouse = Vector3.zero;

        dragging = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _transform.localScale = Vector3.one * 1.1f;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _transform.localScale = Vector3.one;
    }

    #endregion

    private void Start()
    {
        _transform = transform;
    }

    private void Update()
    {
        if (dragging)
            UpdateLine();
    }

    private void UpdateLine()
    {
        float distance = Vector3.Distance(_startPosition, _transform.position);

        Color newColor = Color.white;
        if (distance > (maxDistance - 1f))
            newColor = Color.Lerp(Color.yellow, Color.red, distance - (maxDistance - 1f));
        else if (distance > (maxDistance - 2f))
            newColor = Color.Lerp(Color.white, Color.yellow, distance - (maxDistance - 2f));

        LineManager.Instance.Size = .1f;
        LineManager.Instance.Delta = .25f;
        LineManager.Instance.color = newColor;
        LineManager.Instance.sortingOrder = -1;
        LineManager.Instance.outline = true;
        LineManager.Instance.outlineWidth = .05f;
        LineManager.Instance.outlineColor = Color.white;
        LineManager.Instance.DrawDottedLine(_startPosition, _transform.position);
    }

    private void DragTo(Vector3 newPos)
    {
        if (Vector3.Distance(_startPosition, newPos) <= maxDistance)
        {
            _transform.position = newPos;
        }
        else
        {
            Vector3 norm = (newPos - _startPosition).normalized;
            _transform.position = _startPosition + (norm * maxDistance);
        }
    }
}