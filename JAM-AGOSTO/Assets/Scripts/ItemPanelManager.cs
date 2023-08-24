using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   // Para que sepa lo que es Text e Image
using TMPro;

public class ItemPanelManager : MonoBehaviour
{
    public static ItemPanelManager Instance;
    public GameObject itemPanelUI;

    private void Awake()
    {
        // Necesario para poder llamarlo desde la clase InventoryManager.cs
        Instance = this;
    }
    
    public void ItemPanelData(Item item)
    {
        // Para empezar, buscamos si ya existe el objeto que se crea de la instancia
        GameObject instantiatedPanel = GameObject.Find("ItemPanel(Clone)");

        if (instantiatedPanel == null)
        {
            // Si no lo encuentra no existe, por lo que debe crearlo como un sub-objeto de ItemPanelVessel, ya presente en el canvas
            Debug.Log("No existe instantiatedPanel.");
            GameObject ItemPanelVessel = GameObject.Find("ItemPanelVessel");
            instantiatedPanel = Instantiate(itemPanelUI, ItemPanelVessel.transform);    // Habia que instanciarlo coñooooo
        }

        // Activamos el objeto por si acaso (no creo ni que sea necesario)
        instantiatedPanel.SetActive(true);

        /* Y para terminar cogemos los atributos del objeto que nos ha pasado el botón como parámetro de entrada y los usamos para rellenar los sub-objetos del panel
        (nombre, descripción e icono) */
        var nameItemText = instantiatedPanel.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
        var descriptionItemText = instantiatedPanel.transform.Find("ItemDescription").GetComponent<TextMeshProUGUI>();
        var iconItemImage = instantiatedPanel.transform.Find("ItemIcon").GetComponent<Image>();

        nameItemText.text = item.itemName;
        descriptionItemText.text = item.itemDescription;
        iconItemImage.sprite = item.icon;
    }

}
