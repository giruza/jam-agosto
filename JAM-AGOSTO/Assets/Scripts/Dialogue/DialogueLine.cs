
using UnityEngine;

[System.Serializable]

public class DialogueLine
{
    //Linea de texto a mostrar 

    [TextArea] public string dialogue;

    //Nombre del personaje

    public string speaker;

    //Current sprite

    public Sprite sprite;

}