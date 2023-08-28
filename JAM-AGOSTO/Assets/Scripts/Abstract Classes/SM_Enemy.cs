using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SM_Enemy : MonoBehaviour
{
    [SerializeField] private int _movement_range;
    [SerializeField] private int _attack_range;

    public State _state;

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

    [Serializable]
    public enum State { Idle, Attack, Move}
}
