using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SM_Enemy : MonoBehaviour
{
    [Header("Variables Serializables")]

    [SerializeField] private int _movementRange;
    [SerializeField] private int _attackRange;

    public State _state;
    public EnemyType _enemyType;
    public AttackType _attackType;

    [Header("Pesos Ataques")]
    [SerializeField] [Range(0f, 1f)] private float _pesoRngExplosion;
    [SerializeField] [Range(0f, 1f)] private float _pesoLineAttack;
    [SerializeField] [Range(0f, 1f)] private float _pesoChargeAttack;

    [Header("Referencias")]
    [SerializeField] public EnemyController enemyController;

    public int MOVEMENT_RANGE
    {
        get 
        {
            return _movementRange;
        }

        set 
        {
            _movementRange = value;
        }
    }

    public int ATTACK_RANGE
    {
        get
        {
            return _attackRange;
        }

        set
        {
            _attackRange = value;
        }
    }

    public float Peso_Ranged_Explosion
    {
        get
        {
            return _pesoRngExplosion;
        }

        set
        {
            _pesoRngExplosion = value;
        }
    }

    public float Peso_Line_Attack
    {
        get
        {
            return _pesoLineAttack;
        }

        set
        {
            _pesoLineAttack = value;
        }
    }

    public float Peso_Charge_Attack
    {
        get
        {
            return _pesoChargeAttack;
        }

        set
        {
            _pesoChargeAttack = value;
        }
    }

    public abstract void Turn();

    [Serializable]
    public enum State { Idle, Attack, Move, Flee, Charging, ChargeAttack}

    public enum EnemyType { Melee_Basic, Range_Basic, Hybrid, Caster_Basic}

    public enum AttackType { RangedExplosion, ChargeAttack, LineAttack, AreaMeleeAttack }
}
