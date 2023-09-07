using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class DonationPanelManager : Damager
{
    public static DonationPanelManager Instance;
    public string buildingName;
    public GameObject playerStats;
    //static GameObject playerStats;
    public GameObject donationPanel;
    static Slider donationSlider;
    private int donations;
    [SerializeField] private GameObject startShopMenu;
    
    private void Awake()
    {
        // Necesario para poder llamarlo desde la clase DonationMessageTest.cs
        Instance = this;
    }

    private void Start()
    {
        // Al iniciar el juego, recuperamos las donaciones guardadas (si existen)
        //PlayerPrefs.DeleteAll();
        //donations = PlayerPrefs.GetInt(buildingName, 0);
    }

    public void DonationPanelData()
    {
        if (playerStats.GetComponent<Resources>().Current > 0)
        {
            startShopMenu.SetActive(false);
            donationPanel.SetActive(true);
            //playerStats = MapManager.Instance.GetPlayer();
            donationSlider = donationPanel.transform.Find("Donation slider").GetComponent<Slider>();
            donationSlider.maxValue = playerStats.GetComponent<Resources>().Current;
            donationPanel.transform.Find("Max value slider").GetComponent<TextMeshProUGUI>().text = playerStats.transform.GetComponent<Resources>().Current.ToString();
            donationPanel.transform.Find("Current cuantity").GetComponent<TextMeshProUGUI>().text = "Cantidad actual: " + donationSlider.value.ToString();

            Button add10Button = donationPanel.transform.Find("+10 button").GetComponent<Button>();
            Button add100Button = donationPanel.transform.Find("+100 button").GetComponent<Button>();
            Button subtract10Button = donationPanel.transform.Find("-10 button").GetComponent<Button>();
            Button subtract100Button = donationPanel.transform.Find("-100 button").GetComponent<Button>();
            Button cancelButton = donationPanel.transform.Find("Cancel button").GetComponent<Button>();
            Button donateButton = donationPanel.transform.Find("Donate button").GetComponent<Button>();
            
            add10Button.onClick.RemoveAllListeners();
            add100Button.onClick.RemoveAllListeners();
            subtract10Button.onClick.RemoveAllListeners();
            subtract100Button.onClick.RemoveAllListeners();
            cancelButton.onClick.RemoveAllListeners();
            donateButton.onClick.RemoveAllListeners();

            add10Button.onClick.AddListener(() => Add10Souls());
            add100Button.onClick.AddListener(() => Add100Souls());
            subtract10Button.onClick.AddListener(() => Subtract10Souls());
            subtract100Button.onClick.AddListener(() => Subtract100Souls());
            cancelButton.onClick.AddListener(() => ResetDonations());
            donateButton.onClick.AddListener(() => SaveDonations());
        }
    }

    public void DonationSliderData()
    {
        // Hago esta función aparte para que cada vez que actualicemos el slider solo tenga que ejecutar una línea de código, que es la única que se actualiza
        donationPanel.transform.Find("Current cuantity").GetComponent<TextMeshProUGUI>().text = "Cantidad actual: " + donationSlider.value.ToString();
    }

    public void ResetDonations()
    {
        startShopMenu.SetActive(true);
        donationPanel.SetActive(false);
        donationSlider = donationPanel.transform.Find("Donation slider").GetComponent<Slider>();
        donationSlider.value = 0;
    }

    public void SaveDonations()
    {
        if (donationSlider.value > 0)
        {
            startShopMenu.SetActive(true);
            donationPanel.SetActive(false);

            donationSlider = donationPanel.transform.Find("Donation slider").GetComponent<Slider>();
            //ApplyDamage(MapManager.Instance.GetPlayer().GetComponent<Health>(), DamageAmount);
            ApplyDamage(playerStats.GetComponent<Resources>(), Convert.ToInt32(donationSlider.value));
            //donations += Convert.ToInt32(donationSlider.value);
            SetDonationOperation(Convert.ToInt32(donationSlider.value));
            donationSlider.value = 0;
        }
    }

    public int GetDonations()
    {
        //donations = PlayerPrefs.GetInt(buildingName);
        //return donations;
        return PlayerPrefs.GetInt(buildingName);
    }

    public void SetDonationOperation(int cuantity)
    {
        //donations += cuantity;
        donations = PlayerPrefs.GetInt(buildingName) + cuantity;
        PlayerPrefs.SetInt(buildingName, donations);
        PlayerPrefs.Save();
    }

    public void Add10Souls()
    {
        if (donationSlider.value <= (donationSlider.maxValue - 10))
        {
            donationSlider = donationPanel.transform.Find("Donation slider").GetComponent<Slider>();
            donationSlider.value += 10;
            Debug.Log("Pole sumo 10");
        }
    }

    public void Add100Souls()
    {
        if (donationSlider.value <= (donationSlider.maxValue - 100))
        {
            donationSlider = donationPanel.transform.Find("Donation slider").GetComponent<Slider>();
            donationSlider.value += 100;
            Debug.Log("Pole sumo 100");
        }
    }

    public void Subtract10Souls()
    {
        if (donationSlider.value >= 10)
        {
            donationSlider = donationPanel.transform.Find("Donation slider").GetComponent<Slider>();
            donationSlider.value -= 10;
            Debug.Log("Pole resto 10");
        }
    }

    public void Subtract100Souls()
    {
        if (donationSlider.value >= 100)
        {
            donationSlider = donationPanel.transform.Find("Donation slider").GetComponent<Slider>();
            donationSlider.value -= 100;
            Debug.Log("Pole resto 100");
        }
    }
}
