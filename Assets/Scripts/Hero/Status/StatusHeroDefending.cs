using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusHeroDefending : MonoBehaviour
{
    private Transform _transform;
    private Hero _hero;
    private int _lateHP;
    private SpriteRenderer _heroSprt;
    private Transform _outline;

    private void Start()
    {
        _transform = transform;
        _hero = GetComponent<Hero>();
        _lateHP = _hero.hp;
        _heroSprt = _transform.Find("Sprite").GetComponent<SpriteRenderer>();

        CreateOutline();

        GameManager.instance.currentState = GAME_STATE.PLANNING;
    }

    private void Update()
    {
        if (_lateHP > _hero.hp)
        {
            _hero.hp = _lateHP;
            Destroy(_outline.gameObject);
            Destroy(this);
        }

        _lateHP = _hero.hp;
    }

    private void CreateOutline()
    {
        _outline = Instantiate(_heroSprt.transform, _heroSprt.transform.position, Quaternion.identity, _heroSprt.transform);
        _outline.GetComponent<SpriteRenderer>().color = new Color(.6f, 1f, .55f);
        _outline.GetComponent<SpriteRenderer>().sortingOrder = _heroSprt.sortingOrder - 1;
        _outline.localScale = Vector3.one * 1.25f;
        _outline.name = "Outline";
    }
}