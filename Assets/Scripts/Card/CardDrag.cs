using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class CardDrag : Draggable
{
    [Tooltip("Type of the targeting method of the card")]
    /// <summary>
    /// Type of the targeting method of the card
    /// </summary>
    public TARGET_TYPE targetType;
    /// <summary>
    /// Type of the card owner
    /// </summary>
    public HERO_TYPE ownerType;

    private Card _card;
    private GameObject _targetObj;
    private Vector2 _targetPos;

    #region Interface Implementations

    public override void OnBeginDrag(PointerEventData eventData)
    {
        if (!_card.active)
            return;

        base.OnBeginDrag(eventData);
    }

    public override void OnDrag(PointerEventData eventData)
    {
        if (!_card.active)
            return;

        base.OnDrag(eventData);
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        if (!_card.active)
            return;

        base.OnEndDrag(eventData);

        switch (targetType)
        {
            case TARGET_TYPE.HERO:
                CheckActionObject(); break;
            case TARGET_TYPE.ENEMY:
                CheckActionObject(); break;
            case TARGET_TYPE.BOARD:
                CheckActionPosition(); break;
            case TARGET_TYPE.NONE:
                ResetPosition(); break;
        }

        //CheckActionDiscard();
    }

    #endregion

    protected override void Start()
    {
        base.Start();

        _card = GetComponent<Card>();
    }

    protected void Update()
    {
        if (dragging)
        {
            switch (targetType)
            {
                case TARGET_TYPE.HERO:
                    CheckTargetHero(); break;
                case TARGET_TYPE.ENEMY:
                    CheckTargetEnemy(); break;
                case TARGET_TYPE.BOARD:
                    CheckTargetBoard(); break;
            }

            //CheckTargetDiscard();
        }
        else
        {
            _targetObj = null;
            _targetPos = Vector2.zero;
        }
    }

    public void MoveForward()
    {
        _transform.DOKill();
        _transform.DOMove(Vector3.up * -3.75f, .25f);
    }

    // CHECKS //////////////////////////////

    private void CheckTargetHero()
    {
        Collider2D col = Physics2D.OverlapCircle(_transform.position, .1f, LayerMask.GetMask("Hero"));

        if (col &&
            (ownerType == HERO_TYPE.NONE ||
            col.GetComponent<Hero>().type == ownerType))
        {
            _targetObj = col.gameObject;

            GraphicsManager.target.size = 1.5f;
            GraphicsManager.target.color = new Color(1f, 1f, 1f, .5f);
            GraphicsManager.target.sortingOrder = -1;
            GraphicsManager.target.shape = TARGET_SHAPE.CIRCLE;
            GraphicsManager.target.DrawMarker(_targetObj.transform.position);
        }
        else
            _targetObj = null;
    }

    private void CheckTargetEnemy()
    {
        Collider2D col = Physics2D.OverlapCircle(_transform.position, .1f, LayerMask.GetMask("Enemy"));

        if (col)
        {
            _targetObj = col.gameObject;

            GraphicsManager.target.size = 1.5f;
            GraphicsManager.target.color = new Color(1f, 1f, 1f, .5f);
            GraphicsManager.target.sortingOrder = -1;
            GraphicsManager.target.DrawMarker(_targetObj.transform.position);
        }
        else
            _targetObj = null;
    }

    private void CheckTargetBoard()
    {
        if (_transform.position.y > -3f)
        {
            _targetPos = _transform.position;

            GraphicsManager.target.size = 1.5f;
            GraphicsManager.target.color = new Color(1f, 1f, 1f, .5f);
            GraphicsManager.target.sortingOrder = 1;
            GraphicsManager.target.shape = TARGET_SHAPE.CIRCLE;
            GraphicsManager.target.DrawMarker(_transform.position);
        }
    }

    private void CheckTargetDiscard()
    {
        Collider2D col = Physics2D.OverlapCircle(_transform.position, .1f, LayerMask.GetMask("Discard"));

        if (col)
        {
            GraphicsManager.target.size = 1.5f;
            GraphicsManager.target.color = new Color(1f, 0f, 0f, .5f);
            GraphicsManager.target.sortingOrder = 11;
            GraphicsManager.target.shape = TARGET_SHAPE.CIRCLE;
            GraphicsManager.target.DrawMarker(col.transform.position);
        }
    }

    private void CheckActionPosition()
    {
        if (_transform.position.y > -3f)
            _card.Action(_targetPos);
        else
            ResetPosition();
    }

    private void CheckActionObject()
    {
        if (_targetObj)
        {
            _card.Action(_targetObj);
        }
        else
            ResetPosition();

        _targetObj = null;
    }

    private void CheckActionDiscard()
    {
        Collider2D col = Physics2D.OverlapCircle(_transform.position, .1f, LayerMask.GetMask("Discard"));

        if (col)
            GameManager.instance.deck.Discard(_card);
    }
}