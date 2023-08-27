using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Resources : Progresive, IDamageable, IHealable
{    
    public void Heal(float amount)
    {
        Current += amount;

        if (Current > MaxValue)
            Current = MaxValue;
    }

    public void Damage(float amount) 
    {
        Current -= amount;

        if (Current <= 0)
        {
            Current = 0;
        }
    }
}
