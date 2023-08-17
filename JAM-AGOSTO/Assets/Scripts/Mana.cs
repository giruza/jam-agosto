using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mana : Progresive, IDamageable
{
    public void Damage(float amount) 
    {
        Current -= amount;

        if (Current <= 0)
        {
            Current = 0;
        }
    }

    public void Regenerate(float amount)
    {
        Current += amount;

        if (Current > Initial)
            Current = Initial;
    }
}
