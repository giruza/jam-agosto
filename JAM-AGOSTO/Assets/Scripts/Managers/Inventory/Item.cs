using UnityEngine;

[CreateAssetMenu(fileName ="New Item",menuName ="Item/Create new Item")]

public class Item : ScriptableObject
{
    public int id;                  // identificador del objeto
    public string itemName;         // nombre del objeto
    public string itemDescription;  // descripción del objeto
    public int value;               // valor del objeto (que usara en caso de mejorar algun stat o recuperar vida)
    public Sprite icon;             // icono del objeto
}
