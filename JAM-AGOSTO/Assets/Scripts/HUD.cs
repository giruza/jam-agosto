using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class HUD : Progresive, IDamageable
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

        if(Current > Initial) 
            Current = Initial;
    }
}
