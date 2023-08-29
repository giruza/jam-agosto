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
        // Para empezar, buscamos al objeto padre para poder meter dentro a su hijo, o manipularlo si ya existiese
        GameObject itemPanelVessel = GameObject.Find("ItemPanelVessel");
        GameObject instantiatedPanel;

        // Mira si itemPanelVessel tiene ya su hijo creado
        if (itemPanelVessel.transform.childCount > 0)
        {
            // En tal caso lo guarda en la variable para después poder manipularlo
            Debug.Log("Ya existe instantiatedPanel, por lo que no hace falta crearlo de nuevo.");
            instantiatedPanel = itemPanelVessel.transform.GetChild(0).gameObject;
        }
        else
        {
            // Y en caso de que no, lo creamos
            Debug.Log("No existe aún instantiatedPanel.");
            instantiatedPanel = Instantiate(itemPanelUI, itemPanelVessel.transform);    // Habia que instanciarlo coñooooo
        }

        // Lo activamos por si lo tuviesemos desactivado de haberlo cerrado previamente
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
