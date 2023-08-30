using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   // Para que sepa lo que es Text e Image
using TMPro;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance;
    public List<Item> Items;// = new List<Item>();

    public Transform ItemContent;
    public GameObject InventoryItem;

    private void Awake()
    {
        Instance = this;
    }

    public void ListShopItems()
    {
        // Limpia el contenido de la lista antes de abrir el menú
        // Al final comento esto porque por algún motivo se me estaban borrando los objetos la primera vez que cerraba el menú
        foreach (Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }
        

        foreach (Item item in Items)
        {
            // Primero instancia el objeto, es decir, lo crea en el canvas
            GameObject obj = Instantiate(InventoryItem, ItemContent);
            // Después le asigna el icono que va a tener, usando el correspondiente al objeto que sea
            obj.GetComponent<Image>().sprite = item.icon;
            obj.transform.Find("ItemPhase").GetComponent<Slider>().value = item.phase;
            obj.transform.Find("ItemPhase").GetComponent<Slider>().maxValue = item.maxPhase;
            obj.transform.Find("ItemUpgrade").GetComponent<TextMeshProUGUI>().text = "+" + item.upgrade[item.phase].ToString() + "%";
        
            // Y para terminar cogemos su componente de botón para usar la función de clickar, que nos envia a otra función de la clase asociada al Panel de info
            Button buttonComponent = obj.GetComponent<Button>();
            buttonComponent.onClick.AddListener(() => ItemShopPanelManager.Instance.ItemPanelData(item));
        }

    }

}
