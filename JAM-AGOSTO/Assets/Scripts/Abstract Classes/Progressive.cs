using System;
using UnityEngine;

public abstract class Progresive : MonoBehaviour 
{
    [SerializeField] private float _initial;
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
    public float Ratio => _current / _initial;
    public Action OnChange;

    private void Awake() => _current = _initial;
}