using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   // Para que sepa lo que es Text e Image
using TMPro;

public class ItemShopPanelManager : MonoBehaviour
{
    public static ItemShopPanelManager Instance;
    [SerializeField] private string buildingName;
    public GameObject itemPanelUI;
    //public GameObject shopWarehouse;
    GameObject instantiatedPanel;
    //GameObject shopWarehouse;
    static GameObject shopWarehouseCloseButton;

    public void Start()
    {
        // Necesario hacer esto porque si no de alguna manera daba error y no se recargaba bien el botón de cerrar panel del almacén
        if (shopWarehouseCloseButton == null)
            shopWarehouseCloseButton = GameObject.Find("Warehouse close button");
    }

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
        //shopWarehouse = GameObject.Find("Shop warehouse");
        instantiatedPanel.SetActive(true);
        //shopWarehouse.SetActive(false);   Ya no hace falta desactivarlo
        if (shopWarehouseCloseButton)
            shopWarehouseCloseButton.SetActive(false);

        /* Y para terminar cogemos los atributos del objeto que nos ha pasado el botón como parámetro de entrada y los usamos para rellenar los sub-objetos del panel
        (nombre, descripción e icono) */
        instantiatedPanel.transform.Find("ItemName").GetComponent<TextMeshProUGUI>().text = item.itemName;
        instantiatedPanel.transform.Find("ItemDescription").GetComponent<TextMeshProUGUI>().text = item.itemDescription;
        instantiatedPanel.transform.Find("ItemIcon").GetComponent<Image>().sprite = item.icon;
        //instantiatedPanel.transform.Find("ItemPrice").GetComponent<TextMeshProUGUI>().text = item.price[item.phase].ToString() + "$";
        //instantiatedPanel.transform.Find("LevelItemText").GetComponent<TextMeshProUGUI>().text = "Nivel actual: " + item.phase.ToString();
        instantiatedPanel.transform.Find("UpgradeItemText").GetComponent<TextMeshProUGUI>().text = "+" + item.upgrade[item.phase].ToString() + "%";
        instantiatedPanel.transform.Find("CurrentSoulsText").GetComponent<TextMeshProUGUI>().text = item.souls.ToString();
        if (item.phase < item.maxPhase)
            instantiatedPanel.transform.Find("SoulsToNextUpgradeText").GetComponent<TextMeshProUGUI>().text = (item.price[item.phase] - item.souls).ToString();
        else
            instantiatedPanel.transform.Find("SoulsToNextUpgradeText").GetComponent<TextMeshProUGUI>().text = "---";
        instantiatedPanel.transform.Find("ItemPhase").GetComponent<Slider>().value = item.phase;
        instantiatedPanel.transform.Find("ItemPhase").GetComponent<Slider>().maxValue = item.maxPhase;

        // Guardo asi la llamada a los botones porque si no se stackean y acaban sumando y restando más de la cuenta
        Button closeButton = instantiatedPanel.transform.Find("CloseButton").GetComponent<Button>();
        Button Add10Button = instantiatedPanel.transform.Find("+10Button").GetComponent<Button>();
        Button Add100Button = instantiatedPanel.transform.Find("+100Button").GetComponent<Button>();
        Button Subtract10Button = instantiatedPanel.transform.Find("-10Button").GetComponent<Button>();
        Button Subtract100Button = instantiatedPanel.transform.Find("-100Button").GetComponent<Button>();
        Button ReturnAllButton = instantiatedPanel.transform.Find("ReturnAllButton").GetComponent<Button>();
        
        closeButton.onClick.RemoveAllListeners();
        Add10Button.onClick.RemoveAllListeners();
        Add100Button.onClick.RemoveAllListeners();
        Subtract10Button.onClick.RemoveAllListeners();
        Subtract100Button.onClick.RemoveAllListeners();
        ReturnAllButton.onClick.RemoveAllListeners();

        closeButton.onClick.AddListener(() => DestroyShopItemPanel());
        Add10Button.onClick.AddListener(() => Add10Souls(item));
        Add100Button.onClick.AddListener(() => Add100Souls(item));
        Subtract10Button.onClick.AddListener(() => Subtract10Souls(item));
        Subtract100Button.onClick.AddListener(() => Subtract100Souls(item));
        ReturnAllButton.onClick.AddListener(() => ReturnAllSouls(item));
    }

    void DestroyShopItemPanel()
    {
        UpdateDataItemWarehouse();

        /* Esta era la función antigua, aunque ha quedado desfasada ya que como he explicado en el párrafo anterior, accedia a una lista de objetos de otra
        tienda y no a la correspondiente. */
        //ShopManager.Instance.ListShopItems();
        instantiatedPanel.SetActive(false);
        shopWarehouseCloseButton.SetActive(true);
        //shopWarehouse.SetActive(true);    Ya no hace falta porque no se llega a ocultar
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
        instantiatedPanel.transform.Find("CurrentSoulsText").GetComponent<TextMeshProUGUI>().text = item.souls.ToString();
        if (item.phase < item.maxPhase)
            instantiatedPanel.transform.Find("SoulsToNextUpgradeText").GetComponent<TextMeshProUGUI>().text = (item.price[item.phase] - item.souls).ToString();
        else
            instantiatedPanel.transform.Find("SoulsToNextUpgradeText").GetComponent<TextMeshProUGUI>().text = "---";
        instantiatedPanel.transform.Find("ItemPhase").GetComponent<Slider>().value = item.phase;

        UpdateDataItemWarehouse();
    }

    void UpdateDataItemWarehouse()
    {
        /* Es un poco guarrada todo este trozo de código, pero de esta forma fuerzo a que la función "ListShopItems()" que se ejecuta es la de esta tienda, 
        y no de la última en haber sido creada. Así se actualizan las barras de las fases y sus respectivas mejoras desde el menú de "Shop warehouse",
        y no es necesario salir y entrar en la tienda para ver las estadísticas de los items/buffos actualizadas. De todas formas a ver si hay una manera
        no muy laboriosa de hacer esto más bonito y no depender de hacer un padre tan largo y posteriormente buscar el botón que desencadena la acción
        con un Find(). */
        GameObject buildingButton = null;

        switch (buildingName)
        {
            case "Edificio 1":
                buildingButton = GameObject.Find("Button 1");
                break;
            case "Edificio 2":
                buildingButton = GameObject.Find("Button 2");
                break;
            case "Edificio 3":
                buildingButton = GameObject.Find("Button 3");
                break;
            default:
                buildingButton = GameObject.Find("Button 1");
                break;
        }
        ShopManager shopManager = buildingButton.GetComponent<ShopManager>();
        shopManager.ListShopItems();
    }

}
