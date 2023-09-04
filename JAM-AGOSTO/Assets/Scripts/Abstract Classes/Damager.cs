using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Damager : MonoBehaviour
{
    [SerializeField] private int _damageAmount;
    [SerializeField] private int _basicAttackRange;

    protected void ApplyDamage(IDamageable damageable, int damage) => damageable.Damage(damage);

    //-----------------Borrar este metodo y ponerlo en Healer-----------------------//
    protected void ApplyHeal(IHealable healable) => healable.Heal(_damageAmount);

    public int BasicAttackRange
    {
        get 
        {
            return _basicAttackRange;
        }

        set 
        {
            _basicAttackRange = value;
        }
    }

    public int DamageAmount
    {
        get
        {
            return _damageAmount;
        }

        set
        {
            _damageAmount = value;
        }
    }
}
