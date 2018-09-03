using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [Tooltip("Sorting order while raised when dragging.")]
    /// <summary>
    /// Sorting order while raised when dragging
    /// </summary>
    public int raisedSortingOrder;

    protected Transform _transform;
    protected Transform _sprite;
    protected GameObject _shadow;

    protected Vector3 _startPosition;
    protected Vector3 _offsetToMouse;
    protected float _zDistanceToCamera;
    protected int _sortingOrder;

    protected bool dragging = false;

    ///////////////////////////////////////////////////////////////////////////////

    #region Interface Implementations

    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        _startPosition = _transform.position;
        _zDistanceToCamera = Mathf.Abs(_startPosition.z - Camera.main.transform.position.z);

        _offsetToMouse = _startPosition - Camera.main.ScreenToWorldPoint(
            new Vector3(Input.mousePosition.x, Input.mousePosition.y, _zDistanceToCamera)
        );

        dragging = true;
    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        if (Input.touchCount > 1)
            return;

        Vector3 _newPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
                            Input.mousePosition.y, _zDistanceToCamera)) + _offsetToMouse;
        DragTo(_newPos);
    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {
        _offsetToMouse = Vector3.zero;

        dragging = false;
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        _sprite.GetComponent<SpriteRenderer>().sortingOrder = raisedSortingOrder;
        _shadow.GetComponent<SpriteRenderer>().sortingOrder = raisedSortingOrder - 2;

        _sprite.DOComplete();
        _sprite.DOBlendableScaleBy(Vector3.one * .1f, .1f);
        _sprite.DOLocalMove((Vector3.up + Vector3.right) * .1f, .1f);
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        _sprite.GetComponent<SpriteRenderer>().sortingOrder = _sortingOrder;
        _shadow.GetComponent<SpriteRenderer>().sortingOrder = _sortingOrder - 1;

        _sprite.DOComplete();
        _sprite.DOBlendableScaleBy(Vector3.one * -.1f, .1f);
        _sprite.DOLocalMove(Vector3.zero, .1f);
    }

    #endregion

    ///////////////////////////////////////////////////////////////////////////////

    protected virtual void Start()
    {
        _transform = transform;
        _sprite = _transform.Find("Sprite");
        _startPosition = _transform.position;
        _sortingOrder = _sprite.GetComponent<SpriteRenderer>().sortingOrder;
        CreateShadow();
    }

    protected virtual void Update()
    {
    }

    protected virtual void OnDestroy()
    {
        Destroy(_shadow);
    }

    ///////////////////////////////////////////////////////////////////////////////

    protected virtual void DragTo(Vector3 newPos)
    {
        _transform.position = newPos;
    }

    protected virtual void CreateShadow()
    {
        _shadow = Instantiate(_sprite.gameObject, _sprite.position, Quaternion.identity, _transform);
        _shadow.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f, .5f);
        _shadow.GetComponent<SpriteRenderer>().sortingOrder = _sortingOrder - 1;
        _shadow.name = "Shadow";
    }

    protected virtual void ResetPosition()
    {
        _transform.DOComplete();
        _transform.DOMove(_startPosition, .5f);
    }
}