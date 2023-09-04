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
            _movementRange = MOVEMENT_RANGE;
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
            _attackRange = ATTACK_RANGE;
        }
    }

    public abstract void Turn();

    [Serializable]
    public enum State { Idle, Attack, Move, Flee}

    public enum EnemyType { Melee_Basic, Range_Basic, Hybrid, Caster_Basic}
}
