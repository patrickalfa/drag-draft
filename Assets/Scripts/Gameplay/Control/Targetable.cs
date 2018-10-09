using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public enum TARGET_TYPE
{
    HERO,
    ENEMY,
    BOARD,
    AREA_HERO,
    AREA_ENEMY,
    NONE
}

[RequireComponent(typeof(ShadowCaster))]
public class Targetable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Tooltip("Sorting order while raised when targeting.")]
    /// <summary>
    /// Sorting order while raised when targeting
    /// </summary>
    public int raisedSortingOrder;
    [Tooltip("Color of the line drawn when targeting.")]
    /// <summary>
    /// Color of the line drawn when targeting
    /// </summary>
    public Color lineColor;
    [Tooltip("Range of the targeting.")]
    /// <summary>
    /// Range of the targeting
    /// </summary>
    public float range;
    [Tooltip("Size of the targeting.")]
    /// <summary>
    /// Size of the targeting
    /// </summary>
    public float size;
    [Tooltip("Type of the targeting method of the card.")]
    /// <summary>
    /// Type of the targeting method of the card
    /// </summary>
    public TARGET_TYPE targetType;
    [Tooltip("If the action is temporary or permanent")]
    /// <summary>
    /// If the action is temporary or permanent
    /// </summary>
    public bool temporary;

    protected Transform _transform;
    protected Transform _sprite;
    protected ShadowCaster _shadow;

    protected GameObject _targetObj;
    protected Vector2 _targetPos;
    protected GameObject[] _targetMultiple;

    protected float _zDistanceToCamera;
    protected int _sortingOrder;

    protected bool dragging = false;

    ///////////////////////////////////////////////////////////////////////////////

    #region Interface Implementations

    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        _zDistanceToCamera = Mathf.Abs(_transform.position.z - Camera.main.transform.position.z);

        dragging = true;

        //-------------------------------------------------------------------------

        _sprite.GetComponent<SpriteRenderer>().sortingOrder = raisedSortingOrder;
        _shadow.sortingOrder = raisedSortingOrder - 2;

        _sprite.DOComplete();
        _sprite.DOBlendableScaleBy(Vector3.one * .1f, .1f);
        _sprite.DOLocalMove((Vector3.up + Vector3.right) * .1f, .1f);
    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        if (Input.touchCount > 1)
            return;

        _targetPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
                            Input.mousePosition.y, _zDistanceToCamera));
        DragTo(_targetPos);
    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {
        dragging = false;

        if (targetType == TARGET_TYPE.HERO || targetType == TARGET_TYPE.ENEMY)
        {
            if (_targetObj != null)
                Action();
        }
        else if (targetType == TARGET_TYPE.AREA_HERO || targetType == TARGET_TYPE.AREA_ENEMY)
        {
            if (_targetMultiple != null)
                Action();
        }
        else
            Action();

        //-------------------------------------------------------------------------

        _sprite.GetComponent<SpriteRenderer>().sortingOrder = _sortingOrder;
        _shadow.sortingOrder = _sortingOrder - 1;

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
        _sortingOrder = _sprite.GetComponent<SpriteRenderer>().sortingOrder;
        _shadow = GetComponent<ShadowCaster>();

        _shadow.Setup(_sprite, _sortingOrder - 1);
    }

    protected virtual void Update()
    {
        if (dragging)
        {
            DrawLine();

            switch (targetType)
            {
                case TARGET_TYPE.HERO:
                    CheckTargetHero(); break;
                case TARGET_TYPE.ENEMY:
                    CheckTargetEnemy(); break;
                case TARGET_TYPE.BOARD:
                    CheckTargetBoard(); break;
                case TARGET_TYPE.AREA_HERO:
                    CheckTargetAreaHero(); break;
                case TARGET_TYPE.AREA_ENEMY:
                    CheckTargetAreaEnemy(); break;
            }
        }
        else
        {
            _targetObj = null;
            _targetPos = Vector2.zero;
            _targetMultiple = null;
        }
    }

    protected virtual void OnDisable()
    {
        _shadow.SetShadowActive(false);
    }

    ///////////////////////////////////////////////////////////////////////////////

    protected virtual void DragTo(Vector3 newPos)
    {
        if (Vector3.Distance(_transform.position, newPos) > range)
        {
            Vector3 norm = (newPos - _transform.position).normalized;
            _targetPos = _transform.position + (norm * range);
        }
    }

    protected virtual void DrawLine()
    {
        GraphicsManager.line.size = .15f;
        GraphicsManager.line.delta = .25f;
        GraphicsManager.line.color = lineColor;
        GraphicsManager.line.sortingOrder = raisedSortingOrder;
        GraphicsManager.line.DrawDottedLine(_transform.position, _targetPos);

        GraphicsManager.target.size = .25f;
        GraphicsManager.target.color = lineColor;
        GraphicsManager.target.sortingOrder = raisedSortingOrder;
        GraphicsManager.target.shape = TARGET_SHAPE.CIRCLE;
        GraphicsManager.target.DrawMarker(_targetPos);
    }

    protected virtual void Action()
    {
        if (temporary)
            Destroy(this);
        else
            enabled = false;
    }

    // CHECKS //////////////////////////////

    private void CheckTargetHero()
    {
        Collider2D col = Physics2D.OverlapCircle(_targetPos, .1f, LayerMask.GetMask("Hero"));

        if (col)
        {
            _targetObj = col.gameObject;

            GraphicsManager.target.size = 1.25f;
            GraphicsManager.target.sortingOrder = -1;
            GraphicsManager.target.shape = TARGET_SHAPE.CIRCLE;
            GraphicsManager.target.color = new Color(lineColor.r, lineColor.g, lineColor.b, .5f);
            GraphicsManager.target.DrawMarker(_targetObj.transform.position);
        }
        else
            _targetObj = null;
    }

    private void CheckTargetEnemy()
    {
        Collider2D col = Physics2D.OverlapCircle(_targetPos, .1f, LayerMask.GetMask("Enemy"));

        if (col)
        {
            _targetObj = col.gameObject;

            GraphicsManager.target.size = 1.25f;
            GraphicsManager.target.sortingOrder = -1;
            GraphicsManager.target.shape = TARGET_SHAPE.CIRCLE;
            GraphicsManager.target.color = new Color(lineColor.r, lineColor.g, lineColor.b, .5f);
            GraphicsManager.target.DrawMarker(_targetObj.transform.position);
        }
        else
            _targetObj = null;
    }

    private void CheckTargetBoard()
    {
        _targetPos = _transform.position;

        GraphicsManager.target.size = 1.5f;
        GraphicsManager.target.color = new Color(lineColor.r, lineColor.g, lineColor.b, .5f);
        GraphicsManager.target.sortingOrder = 1;
        GraphicsManager.target.shape = TARGET_SHAPE.CIRCLE;
        GraphicsManager.target.DrawMarker(_transform.position);
    }

    private void CheckTargetAreaHero()
    {
        GraphicsManager.target.size = size;
        GraphicsManager.target.sortingOrder = -1;
        GraphicsManager.target.shape = TARGET_SHAPE.CIRCLE;
        GraphicsManager.target.color = new Color(lineColor.r, lineColor.g, lineColor.b, .5f);
        GraphicsManager.target.DrawMarker(_targetPos);

        Collider2D[] cols = Physics2D.OverlapCircleAll(_targetPos, size * .5f, LayerMask.GetMask("Hero"));

        int length = cols.Length;
        if (length > 0)
        {
            _targetMultiple = new GameObject[length];
            for (int i = 0; i < length; i++)
            {
                _targetMultiple[i] = cols[i].gameObject;
                GraphicsManager.target.size = 1.25f;
                GraphicsManager.target.DrawMarker(_targetMultiple[i].transform.position);
            }
        }
        else
            _targetMultiple = null;
    }

    private void CheckTargetAreaEnemy()
    {
        GraphicsManager.target.size = size;
        GraphicsManager.target.sortingOrder = -1;
        GraphicsManager.target.shape = TARGET_SHAPE.CIRCLE;
        GraphicsManager.target.color = new Color(lineColor.r, lineColor.g, lineColor.b, .5f);
        GraphicsManager.target.DrawMarker(_targetPos);

        Collider2D[] cols = Physics2D.OverlapCircleAll(_targetPos, size * .5f, LayerMask.GetMask("Enemy"));

        int length = cols.Length;
        if (length > 0)
        {
            _targetMultiple = new GameObject[length];
            for (int i = 0; i < length; i++)
            {
                _targetMultiple[i] = cols[i].gameObject;
                GraphicsManager.target.size = 1.25f;
                GraphicsManager.target.DrawMarker(_targetMultiple[i].transform.position);
            }
        }
        else
            _targetMultiple = null;
    }
}