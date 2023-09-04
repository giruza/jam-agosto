using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DonationMessageTest : MonoBehaviour
{
    public void Update()
    {
        this.gameObject.GetComponent<TextMeshProUGUI>().text = "Donaciones disponibles: " + DonationPanelManager.Instance.GetDonations().ToString();
    }
}
