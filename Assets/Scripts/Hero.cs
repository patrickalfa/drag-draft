using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour, IDamageable
{
    private int m_hp;

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
    public int range;
    /// <summary>
    /// Maximum distance the hero may cover on a movement
    /// </summary>
    public int speed;
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

    // TODO
    public void Move()
    {
    }

    /// <summary>
    /// Attacks the targeted IDamageable
    /// </summary>
    /// <param name="target">Target of the attack</param>
    public void Attack(IDamageable target)
    {
        target.TakeDamage(damage);
    }

    /// <summary>
    /// Damages the hero
    /// </summary>
    /// <param name="damage">Amount of damage taken</param>
    /// <returns>True if the hero is killed, False if the hero survived</returns>
    public bool TakeDamage(int damage)
    {
        hp = Mathf.Clamp(hp - damage, 0, maxHP);
        return hp == 0;
    }
}