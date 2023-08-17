using System;
using UnityEngine;

public abstract class Progresive : MonoBehaviour 
{
    [SerializeField] private float _initial;
    [SerializeField] private float _maxValue;
    private float _current;

    public float Current
    {
        get
        {
            return _current;
        }

        set
        {
            _current = value;
            OnChange?.Invoke();
        }
    }

    public float Initial => _initial;

    public float MaxValue 
    {
        get
        {
            return _maxValue;
        }

        set
        {
            _maxValue = value;
            OnChange?.Invoke();
        }
    }
    public float Ratio => _current / _initial;
    public Action OnChange;

    private void Awake() => _current = _initial;
}