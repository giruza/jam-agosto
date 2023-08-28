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

    [SerializeField] private TextMeshProUGUI currentHealthText;
    [SerializeField] private TextMeshProUGUI maxHealthText;
    [SerializeField] private TextMeshProUGUI currentManaText;
    [SerializeField] private TextMeshProUGUI maxManaText;
    [SerializeField] private TextMeshProUGUI currentResourcesText;
    [SerializeField] private TextMeshProUGUI maxResourcesText;

    //Prueba Objetos Usables
    [SerializeField] private Image[] _objects;

    // Ponemos un metodo Start() para actualizar los datos del menu de personaje antes de que se actualicen en UpdateBar(), que esto solo sucede cuando alguno de sus componentes varia
    void Start()
    {
        currentHealthText.text = _health.Current.ToString();
        maxHealthText.text = _health.MaxValue.ToString();
        currentManaText.text = _mana.Current.ToString();
        maxManaText.text = _mana.MaxValue.ToString();
        currentResourcesText.text = _resources.Current.ToString();
        maxResourcesText.text = _resources.MaxValue.ToString();
    }

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
        
        // Añado tambien estas lineas para actualizar las estadisticas dentro del menu de personaje
        currentHealthText.text = _health.Current.ToString();
        maxHealthText.text = _health.MaxValue.ToString();
        currentManaText.text = _mana.Current.ToString();
        maxManaText.text = _mana.MaxValue.ToString();
        currentResourcesText.text = _resources.Current.ToString();
        maxResourcesText.text = _resources.MaxValue.ToString();
    }

    public void UseObject(Sprite sprite, int objeto)
    {
        if (_objects[objeto].sprite == null)
        {
            _objects[objeto].sprite = sprite;
        }
        else 
        {
            _objects[objeto].sprite = null;
        }
    }
}
