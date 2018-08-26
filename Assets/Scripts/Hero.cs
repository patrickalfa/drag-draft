using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour, IDamageable
{
    private int m_hp;

    ///////////////////////////////////////////////////////////////////////////////

    /// <summary>
    /// The maximum amount of health point that the hero may have
    /// </summary>
    public int maxHP;
    /// <summary>
    /// The amount of damage the hero will give on its basic attack
    /// </summary>
    public int damage;
    /// <summary>
    /// The maximum range of the hero's basic attack
    /// </summary>
    public int range;
    /// <summary>
    /// The maximum distance the hero may cover on a movement
    /// </summary>
    public int speed;
    /// <summary>
    /// The amount of health the hero currently has
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
    /// <param name="target">The target of the attack</param>
    public void Attack(IDamageable target)
    {
        target.TakeDamage(damage);
    }

    /// <summary>
    /// Damages the hero
    /// </summary>
    /// <param name="damage">The amount of damage taken</param>
    /// <returns>True if the hero is killed, False if the hero survived</returns>
    public bool TakeDamage(int damage)
    {
        hp = Mathf.Clamp(hp - damage, 0, maxHP);
        return hp == 0;
    }
}