using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private Mana _mana;
    [SerializeField] private Resources _resources;
    [SerializeField] private Slider _healthFillImage;
    [SerializeField] private Slider _manaFillImage;
    [SerializeField] private TextMeshProUGUI resourcesText;

    private void OnEnable() 
    {
        _health.OnChange += UpdateBar;
        _mana.OnChange += UpdateBar;
        _resources.OnChange += UpdateBar;
    }
    private void OnDisable() 
    {
        _health.OnChange -= UpdateBar;
        _mana.OnChange -= UpdateBar;
        _resources.OnChange -= UpdateBar;
    } 

    private void UpdateBar() 
    {
        _healthFillImage.value = _health.Ratio;
        _manaFillImage.value = _mana.Ratio;
        resourcesText.text = _resources.Current.ToString();
    }
}
