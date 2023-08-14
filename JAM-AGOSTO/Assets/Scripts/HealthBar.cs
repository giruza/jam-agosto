using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Progresive _health;
    [SerializeField] private Slider _fillImage;

    private void OnEnable() => _health.OnChange += UpdateBar;
    private void OnDisable() => _health.OnChange -= UpdateBar;

    private void UpdateBar() 
    {
        print(_health.Ratio);
        //_fillImage.fillAmount = _health.Ratio;
        _fillImage.value = _health.Ratio;
    }
}
