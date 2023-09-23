using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Health : Progresive, IDamageable, IHealable
{
    [SerializeField] private UnityEvent OnDie;
    [SerializeField] private UnityEvent OnHit;

    public Animator animator;

    public void Damage(float amount)
    {
        Current -= amount;
        OnHit?.Invoke();

        if (Current <= 0)
        {
            OnDie.Invoke();
        }
    }

    public void DamageToPlayer()
    {
        animator.SetTrigger("TriggerAttacked");
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

    public void MuerteEnemigo() 
    {
        MapManager.Instance.RemoveEnemy(gameObject);
        MapManager.Instance.RemoveOccupiedTile(gameObject.GetComponent<EnemyController>().coords);
        Destroy(gameObject);
    }
}
