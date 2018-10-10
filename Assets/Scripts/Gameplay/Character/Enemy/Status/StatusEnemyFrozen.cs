using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StatusEnemyFrozen : MonoBehaviour
{
    private Transform _transform;
    private Enemy _enemy;
    private SpriteRenderer _enemySprt;
    private Transform _overlay;
    private int _lateHP;

    private void Start()
    {
        _transform = transform;
        _enemy = GetComponent<Enemy>();
        _enemySprt = _transform.Find("Sprite").GetComponent<SpriteRenderer>();
        _lateHP = _enemy.hp;

        _enemy.canAct = false;

        CreateOverlay();

        GameManager.instance.currentState = GAME_STATE.PLANNING;
    }

    private void Update()
    {
        if (_enemy.acting || _lateHP != _enemy.hp)
        {
            _enemy.canAct = true;

            _overlay.DOShakePosition(.25f, .1f, 25).OnComplete(() =>
            {
                Destroy(_overlay.gameObject);
            });

            Destroy(this);
        }
    }

    private void CreateOverlay()
    {
        _overlay = new GameObject("Overlay").transform;
        _overlay.gameObject.AddComponent<SpriteRenderer>().sprite = GraphicsManager.target.sprites[(int)TARGET_SHAPE.DIAMOND]; // DEBUG
        _overlay.position = _enemySprt.transform.position;
        _overlay.parent = _enemySprt.transform;
        _overlay.GetComponent<SpriteRenderer>().color = new Color(.3f, .38f, 72f, .75f);
        _overlay.GetComponent<SpriteRenderer>().sortingOrder = _enemySprt.sortingOrder + 1;
        _overlay.localScale = Vector3.one * 1.25f;
    }
}