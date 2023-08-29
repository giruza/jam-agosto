using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Health : Progresive, IDamageable, IHealable
{
    [SerializeField] private UnityEvent OnDie;

    public void Damage(float amount)
    {
        Current -= amount;

        if (Current <= 0)
        {
            OnDie.Invoke();
        }
    }

    public void Heal(float amount)
    {
        Current += amount;

        if(Current > MaxValue) 
            Current = MaxValue;
    }

    public void Morirse() 
    {
        Debug.Log("C muere");
        Destroy(gameObject);
    }
}
