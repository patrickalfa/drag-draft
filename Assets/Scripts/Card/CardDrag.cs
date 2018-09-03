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

    private Card _card;
    private GameObject _targetObj;
    private Vector2 _targetPos;

    #region Interface Implementations

    public override void OnEndDrag(PointerEventData eventData)
    {
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
                CheckAction(); break;
        }

        CheckActionDiscard();
    }

    #endregion

    protected override void Start()
    {
        base.Start();

        _card = GetComponent<Card>();
    }

    protected override void Update()
    {
        base.Update();

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

            CheckTargetDiscard();
        }
        else
        {
            _targetObj = null;
            _targetPos = Vector2.zero;
        }
    }

    // CHECKS //////////////////////////////

    private void CheckTargetHero()
    {
        Collider2D col = Physics2D.OverlapCircle(_transform.position, .1f, LayerMask.GetMask("Hero"));

        if (col)
        {
            _targetObj = col.gameObject;

            TargetManager.instance.size = 1.5f;
            TargetManager.instance.color = new Color(1f, 1f, 1f, .5f);
            TargetManager.instance.sortingOrder = -1;
            TargetManager.instance.shape = TARGET_SHAPE.CIRCLE;
            TargetManager.instance.DrawMarker(_targetObj.transform.position);
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

            TargetManager.instance.size = 1.5f;
            TargetManager.instance.color = new Color(1f, 1f, 1f, .5f);
            TargetManager.instance.sortingOrder = -1;
            TargetManager.instance.DrawMarker(_targetObj.transform.position);
        }
        else
            _targetObj = null;
    }

    private void CheckTargetBoard()
    {
    }

    private void CheckTargetDiscard()
    {
        Collider2D col = Physics2D.OverlapCircle(_transform.position, .1f, LayerMask.GetMask("Discard"));

        if (col)
        {
            TargetManager.instance.size = 1.5f;
            TargetManager.instance.color = new Color(1f, 0f, 0f, .5f);
            TargetManager.instance.sortingOrder = 11;
            TargetManager.instance.shape = TARGET_SHAPE.CIRCLE;
            TargetManager.instance.DrawMarker(col.transform.position);
        }
    }

    private void CheckAction()
    {
    }

    private void CheckActionPosition()
    {
    }

    private void CheckActionObject()
    {
        if (_targetObj)
        {
            _card.Action(_targetObj);
            GameManager.instance.deck.Discard(_card);
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