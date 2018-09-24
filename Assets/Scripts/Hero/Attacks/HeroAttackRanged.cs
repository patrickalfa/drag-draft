using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HeroAttackRanged : HeroAttack
{
    [Header("RANGED:")]
    public Sprite projectileSprite;
    public float projectileScale;

    protected override void Action()
    {
        Transform p = new GameObject("Projectile", typeof(SpriteRenderer)).transform;
        p.position = _transform.position;
        p.localScale = Vector3.one * projectileScale;
        p.GetComponent<SpriteRenderer>().color = Color.black; // DEBUG
        p.GetComponent<SpriteRenderer>().sprite = projectileSprite;
        p.GetComponent<SpriteRenderer>().sortingOrder = _sprite.GetComponent<SpriteRenderer>().sortingOrder;

        Vector3 offset = _targetObj.transform.position - _transform.position;
        p.rotation = Quaternion.LookRotation(Vector3.forward, offset);

        p.DOMove(_targetObj.transform.position, .1f).OnComplete(() =>
        {
            Destroy(p.gameObject);
        });

        base.Action();
    }
}