using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPanelDestroyer : MonoBehaviour
{
    public void DestroyItemPanel()
    {
        Debug.Log("Entrando en DestroyItemPanel()");
        GameObject instantiatedPanel = GameObject.Find("ItemPanel(Clone)");

        if (instantiatedPanel == null)
        {
            Debug.Log("ERROR: No hay ItemPanel(Clone) que destruir.");
        }
        else
        {
            Destroy(instantiatedPanel.gameObject);
        }
    }
}
