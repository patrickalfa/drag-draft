using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Character : MonoBehaviour, IDamageable
{
    protected int m_hp;

    ///////////////////////////////////////////////////////////////////////////////

    /// <summary>
    /// Maximum amount of health points that the hero can have
    /// </summary>
    public int maxHP;
    /// <summary>
    /// Amount of damage the hero will give on its basic attack
    /// </summary>
    public int damage;
    /// <summary>
    /// Maximum range of the hero's basic attack
    /// </summary>
    public float range;
    /// <summary>
    /// Maximum distance the hero may cover on a movement
    /// </summary>
    public float speed;
    /// <summary>
    /// Amount of health the hero currently has
    /// </summary>
    public int hp
    {
        get
        {
            return m_hp;
        }
        set
        {
            m_hp = value;
        }
    }

    ///////////////////////////////////////////////////////////////////////////////

    protected Transform _transform;
    protected SpriteRenderer _sprite;

    ///////////////////////////////////////////////////////////////////////////////

    protected virtual void Start()
    {
        _transform = transform;
        _sprite = _transform.Find("Sprite").GetComponent<SpriteRenderer>();

        hp = maxHP;
    }

    /// <summary>
    /// Damages the hero
    /// </summary>
    /// <param name="damage">Amount of damage taken</param>
    /// <returns>True if the hero is killed, False if the hero survived</returns>
    public virtual bool TakeDamage(int damage)
    {
        hp = Mathf.Clamp(hp - damage, 0, maxHP);

        _sprite.transform.DOShakePosition(.25f, .1f, 25).OnComplete(() =>
        {
            if (hp == 0)
            {
                _sprite.transform.DOScale(0f, .25f);
                Invoke("DestroySelf", .25f);
            }
        });

        return hp == 0;
    }

    public virtual void Lock()
    {
        Color c = _sprite.color;
        c.a = .5f;
        _sprite.color = c;
    }

    public virtual void Unlock()
    {
        Color c = _sprite.color;
        c.a = 1f;
        _sprite.color = c;
    }

    protected void DestroySelf()
    {
        gameObject.SetActive(false);
        //Destroy(gameObject);
    }
}