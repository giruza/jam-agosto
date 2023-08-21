using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueDisplayer : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private Image spriteImage;
    public DialogueObject currentDialogue;

    public int dialogueIndex = -1; // Inicializado en -1 para que comience desde el principio
    private bool isTyping = false;
    private bool allTextDisplayed = false;

    private void Start()
    {
        Display();
    }
    

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (dialogueBox.activeSelf)
            {
                if (isTyping)
                {
                    // Si está escribiendo, mostrar todo el texto de una vez
                    DisplayAllText();
                }
                else if (!allTextDisplayed)
                {
                    AdvanceDialogue();
                }
                else
                {
                    // Si ya se mostró todo el texto, avanzar al siguiente diálogo
                    AdvanceDialogue();
                }
            }
            else
            {
                dialogueBox.SetActive(false);
            }
        }
    }

    private void DisplayAllText()
    {
        // Mostrar todo el texto restante
        dialogueText.text = currentDialogue.dialogueLines[dialogueIndex].dialogue;
        isTyping = false;
        allTextDisplayed = true;
    }

    private void AdvanceDialogue()
    {
        dialogueIndex++;
        allTextDisplayed = false; // Reiniciar el estado de mostrar todo el texto

        // Si hemos mostrado todos los diálogos, ocultar el cuadro de diálogo
        if (dialogueIndex >= currentDialogue.dialogueLines.Length)
        {
            dialogueBox.SetActive(false);
            dialogueIndex = -1;
        }
        else
        {
            DialogueLine currentLine = currentDialogue.dialogueLines[dialogueIndex];

            spriteImage.sprite = currentLine.sprite;
            nameText.text = currentLine.speaker;

            dialogueText.text = ""; // Vaciar el texto antes de iniciar la mecanografía
            StartCoroutine(TypeDialogue(currentLine.dialogue));
        }
    }

    private IEnumerator TypeDialogue(string text)
    {
        isTyping = true;
        int displayedCharacters = 0;

        while (displayedCharacters < text.Length)
        {
            dialogueText.text = text.Substring(0, displayedCharacters + 1);
            displayedCharacters++;

            yield return new WaitForSeconds(0.03f); // Tiempo entre caracteres
        }

        isTyping = false;
    }

    public void DisplayDialogue(DialogueObject dialogueObject)
    {
        dialogueIndex = -1; // Reiniciar el índice al principio
        dialogueBox.SetActive(true);
        AdvanceDialogue(); // Mostrar la primera línea de diálogo
    }

    public void Display()
    {
        DisplayDialogue(currentDialogue);
    }

}
