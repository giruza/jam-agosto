using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPanelDestroyer : MonoBehaviour
{
    public GameObject closePanelButton;
    public void DestroyItemPanel()
    {
        Debug.Log("Entrando en DestroyItemPanel()");
        Transform instantiatedPanel = closePanelButton.transform.parent;

        if (instantiatedPanel == null)
        {
            Debug.Log("ERROR: No hay ItemPanel(Clone) que destruir.");
        }
        else
        {
            //Destroy(instantiatedPanel.gameObject);
            instantiatedPanel.gameObject.SetActive(false);
        }
        
    }
}
