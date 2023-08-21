using UnityEngine;

public class Interactuable : MonoBehaviour
{
    public GameObject dialogoCanvas;


    private bool playerInRange = false;

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.F))
        {
            if (dialogoCanvas.activeSelf)
            {
                dialogoCanvas.SetActive(false);
            }
            else
            {
                dialogoCanvas.SetActive(true);
                dialogoCanvas.GetComponent<DialogueDisplayer>().Display();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        dialogoCanvas.transform.Find("DialogueBox").gameObject.SetActive(true);

        Debug.Log("Colisiona");
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Descolisiona");

        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            dialogoCanvas.SetActive(false); 
        }
    }
}
