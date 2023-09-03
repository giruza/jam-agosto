using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DonationPanelManager : MonoBehaviour
{
    public GameObject playerStats;
    public GameObject donationPanel;
    public void DonationPanelData()
    {
        Slider donationSlider = donationPanel.transform.Find("Donation slider").GetComponent<Slider>();
        donationSlider.maxValue = playerStats.GetComponent<Resources>().Initial;
        donationPanel.transform.Find("Max value slider").GetComponent<TextMeshProUGUI>().text = playerStats.transform.GetComponent<Resources>().Initial.ToString();
        donationPanel.transform.Find("Current cuantity").GetComponent<TextMeshProUGUI>().text = "Cantidad actual: " + donationSlider.value.ToString();
    }

    public void SaveDonations()
    {
        // tengo que usar la funcion damage de resources, aunque para eso tengo que ver como dañan por ejemplo a claramaria los enemigos, que usara lo mismo con la vida
        // y atmbien tengo que guardar los recursos que coja en una variable permanente para cada tienda
    }
}
