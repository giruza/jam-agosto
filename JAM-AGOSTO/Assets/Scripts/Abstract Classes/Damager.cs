using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Damager : MonoBehaviour
{
    [SerializeField] private float _damageAmount;
    [SerializeField] private int _basicAttackRange;

    protected void ApplyDamage(IDamageable damageable) => damageable.Damage(_damageAmount);

    //Borrar este metodo y ponerlo en Healer
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
}
