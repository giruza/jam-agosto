using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   // Para que sepa lo que es Text e Image

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<Item> Items = new List<Item>();

    public Transform ItemContent;
    public GameObject InventoryItem;

    private void Awake()
    {
        Instance = this;
    }

    public void Add(Item item)
    {
        Items.Add(item);
    }

    public void Remove(Item item)
    {
        Items.Remove(item);
    }

    public void ListItems()
    {
        // Limpia el contenido de la lista antes de abrir el menú
        foreach (Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }

        foreach (var item in Items)
        {
            GameObject obj = Instantiate(InventoryItem, ItemContent);
            var itemName = obj.transform.Find("ItemName").GetComponent<Text>();
            var itemDescription = obj.transform.Find("ItemDescription").GetComponent<Text>();
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
        
            itemName.text = item.itemName;
            itemDescription.text = item.itemDescription;
            itemIcon.sprite = item.icon;
        }
    }
}
