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

        instantiatedPanel.transform.Find("+10Button").GetComponent<Button>().onClick.AddListener(() => Add10Souls(item));
        instantiatedPanel.transform.Find("+100Button").GetComponent<Button>().onClick.AddListener(() => Add100Souls(item));
        instantiatedPanel.transform.Find("-10Button").GetComponent<Button>().onClick.AddListener(() => Subtract10Souls(item));
        instantiatedPanel.transform.Find("-100Button").GetComponent<Button>().onClick.AddListener(() => Subtract100Souls(item));
        instantiatedPanel.transform.Find("ReturnAllButton").GetComponent<Button>().onClick.AddListener(() => ReturnAllSouls(item));
    }

    void DestroyShopItemPanel()
    {
        ShopManager.Instance.ListShopItems();
        instantiatedPanel.SetActive(false);
        shopWarehouse.SetActive(true);
    }

    public void Add10Souls(Item item)
    {
        int donations = DonationPanelManager.Instance.GetDonations();

        if (donations >= 10)
        {
            if (item.souls < (item.price[item.maxPhase - 1] - 10))
            {
                DonationPanelManager.Instance.SetDonationOperation(-10);
                item.souls += 10;

                UpdateItem(item, true);
            }
            else if (item.souls < item.price[item.maxPhase - 1])
            {
                DonationPanelManager.Instance.SetDonationOperation(item.souls - item.price[item.maxPhase - 1]);
                item.souls += (item.price[item.maxPhase - 1] - item.souls);

                UpdateItem(item, true);
            }
        }
        else if (donations > 0)
        {
            if (item.souls < (item.price[item.maxPhase - 1] - donations))
            {
                DonationPanelManager.Instance.SetDonationOperation(-donations);
                item.souls += donations;

                UpdateItem(item, true);
            }
            else if (item.souls < item.price[item.maxPhase - 1])
            {
                DonationPanelManager.Instance.SetDonationOperation(item.souls - item.price[item.maxPhase - 1]);
                item.souls += (item.price[item.maxPhase - 1] - item.souls);

                UpdateItem(item, true);
            }
        }
    }

    public void Add100Souls(Item item)
    {
        int donations = DonationPanelManager.Instance.GetDonations();

        if (donations >= 100)
        {
            if (item.souls < (item.price[item.maxPhase - 1] - 100))
            {
                DonationPanelManager.Instance.SetDonationOperation(-100);
                item.souls += 100;

                UpdateItem(item, true);
            }
            else if (item.souls < item.price[item.maxPhase - 1])
            {
                DonationPanelManager.Instance.SetDonationOperation(item.souls - item.price[item.maxPhase - 1]);
                item.souls += (item.price[item.maxPhase - 1] - item.souls);

                UpdateItem(item, true);
            }
        }
        else if (donations > 0)
        {
            if (item.souls < (item.price[item.maxPhase - 1] - donations))
            {
                DonationPanelManager.Instance.SetDonationOperation(-donations);
                item.souls += donations;

                UpdateItem(item, true);
            }
            else if (item.souls < item.price[item.maxPhase - 1])
            {
                DonationPanelManager.Instance.SetDonationOperation(item.souls - item.price[item.maxPhase - 1]);
                item.souls += (item.price[item.maxPhase - 1] - item.souls);

                UpdateItem(item, true);
            }
        }
    }

    public void Subtract10Souls(Item item)
    {
        if (item.souls >= 10)
        {
            DonationPanelManager.Instance.SetDonationOperation(10);
            item.souls += -10;

            UpdateItem(item, false);
        }
        else if (item.souls > 0)
        {
            DonationPanelManager.Instance.SetDonationOperation(item.souls);
            item.souls = 0;

            UpdateItem(item, false);
        }
    }

    public void Subtract100Souls(Item item)
    {
        if (item.souls >= 100)
        {
            DonationPanelManager.Instance.SetDonationOperation(100);
            item.souls += -100;

            UpdateItem(item, false);
        }
        else if (item.souls > 0)
        {
            DonationPanelManager.Instance.SetDonationOperation(item.souls);
            item.souls = 0;

            UpdateItem(item, false);
        }
    }

    public void ReturnAllSouls(Item item)
    {
        DonationPanelManager.Instance.SetDonationOperation(item.souls);
        item.souls = 0;

        UpdateItem(item, false);
    }

    void UpdateItem(Item item, bool sum)
    {
        if (sum)
        {
            while ((item.phase < item.maxPhase) && (item.souls >= item.price[item.phase]))
            {
                item.phase++;
            }
        }
        else
        {
            while ((item.phase > 0) && (item.souls < item.price[item.phase - 1]))
            {
                item.phase--;
            }
        }

        Debug.Log("Mejora: +" + item.upgrade[item.phase].ToString() + "%");
        instantiatedPanel.transform.Find("UpgradeItemText").GetComponent<TextMeshProUGUI>().text = "+" + item.upgrade[item.phase].ToString() + "%";
        instantiatedPanel.transform.Find("ItemPhase").GetComponent<Slider>().value = item.phase;
    }

}
