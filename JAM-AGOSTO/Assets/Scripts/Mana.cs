using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mana : Progresive, IDamageable, IHealable
{
    public void Damage(float amount) 
    {
        Current -= amount;

        if (Current <= 0)
        {
            Current = 0;
        }
    }

    public void Heal(float amount)
    {
        Current += amount;

        if (Current > MaxValue)
            Current = MaxValue;
    }
}
