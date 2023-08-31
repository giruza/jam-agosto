using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SM_Enemy : MonoBehaviour
{
    [Header("Variables Serializables")]

    [SerializeField] private int _movement_range;
    [SerializeField] private int _attack_range;

    public State _state;

    [Header("Referencias")]
    [SerializeField] public EnemyController enemyController;

    public int MOVEMENT_RANGE
    {
        get 
        {
            return _movement_range;
        }

        set 
        {
            _movement_range = MOVEMENT_RANGE;
        }
    }

    public int ATTACK_RANGE
    {
        get
        {
            return _attack_range;
        }

        set
        {
            _attack_range = ATTACK_RANGE;
        }
    }

    public abstract void Turn();

    [Serializable]
    public enum State { Idle, Attack, Move}
}
