using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   // Para que sepa lo que es Text e Image
using TMPro;

public class ItemShopPanelManager : MonoBehaviour
{
    public static ItemShopPanelManager Instance;
    public GameObject itemPanelUI;
    //public GameObject shopWarehouse;
    GameObject instantiatedPanel;
    GameObject shopWarehouse;

    private void Awake()
    {
        // Necesario para poder llamarlo desde la clase InventoryManager.cs
        Instance = this;
    }
    
    public void ItemPanelData(Item item)
    {
        // Para empezar, buscamos al objeto padre para poder meter dentro a su hijo, o manipularlo si ya existiese
        GameObject itemPanelVessel = GameObject.Find("ItemPanelVessel");

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
        shopWarehouse = GameObject.Find("Shop warehouse");
        instantiatedPanel.SetActive(true);
        shopWarehouse.SetActive(false);

        /* Y para terminar cogemos los atributos del objeto que nos ha pasado el botón como parámetro de entrada y los usamos para rellenar los sub-objetos del panel
        (nombre, descripción e icono) */
        instantiatedPanel.transform.Find("ItemName").GetComponent<TextMeshProUGUI>().text = item.itemName;
        instantiatedPanel.transform.Find("ItemDescription").GetComponent<TextMeshProUGUI>().text = item.itemDescription;
        instantiatedPanel.transform.Find("ItemIcon").GetComponent<Image>().sprite = item.icon;
        //instantiatedPanel.transform.Find("ItemPrice").GetComponent<TextMeshProUGUI>().text = item.price[item.phase].ToString() + "$";
        //instantiatedPanel.transform.Find("LevelItemText").GetComponent<TextMeshProUGUI>().text = "Nivel actual: " + item.phase.ToString();
        instantiatedPanel.transform.Find("UpgradeItemText").GetComponent<TextMeshProUGUI>().text = "+" + item.upgrade[item.phase].ToString() + "%";
        instantiatedPanel.transform.Find("ItemPhase").GetComponent<Slider>().value = item.phase;
        instantiatedPanel.transform.Find("ItemPhase").GetComponent<Slider>().maxValue = item.maxPhase;

        Button closeButton = instantiatedPanel.transform.Find("CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() => DestroyShopItemPanel());
    }

    void DestroyShopItemPanel()
    {
        ShopManager.Instance.ListShopItems();
        instantiatedPanel.SetActive(false);
        shopWarehouse.SetActive(true);
    }

}
