using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class DonationPanelManager : Damager
{
    public static DonationPanelManager Instance;
    public GameObject playerStats;
    //static GameObject playerStats;
    public GameObject donationPanel;
    static Slider donationSlider;
    public int donations;
    [SerializeField] private GameObject startShopMenu;
    
    private void Awake()
    {
        // Necesario para poder llamarlo desde la clase DonationMessageTest.cs
        Instance = this;
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
            donations += Convert.ToInt32(donationSlider.value);
            donationSlider.value = 0;
        }
    }

    public int GetDonations()
    {
        return donations;
    }

    public void SetDonationOperation(int cuantity)
    {
        donations += cuantity;
    }
}
