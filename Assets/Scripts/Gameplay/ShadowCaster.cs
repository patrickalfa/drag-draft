using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowCaster : MonoBehaviour
{
    private Transform _transform;
    private Transform _shadow;
    private Transform _sprite;
    private int _sortingOrder;

    public int sortingOrder
    {
        get
        {
            return _sortingOrder;
        }

        set
        {
            _sortingOrder = value;
            _shadow.GetComponent<SpriteRenderer>().sortingOrder = _sortingOrder;
        }
    }

    private void Awake()
    {
        _transform = transform;
    }

    public void Setup(Transform sprite, int sortingOrder)
    {
        _sprite = sprite;
        _sortingOrder = sortingOrder;

        if (_shadow)
            Destroy(_shadow.gameObject);

        CreateShadow();
    }

    public void SetShadowActive(bool active)
    {
        _shadow.gameObject.SetActive(active);
    }

    private void CreateShadow()
    {
        _shadow = Instantiate(_sprite, _sprite.position, Quaternion.identity, _transform);
        _shadow.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f, .5f);
        _shadow.GetComponent<SpriteRenderer>().sortingOrder = _sortingOrder - 1;
        _shadow.name = "Shadow";
    }
}
