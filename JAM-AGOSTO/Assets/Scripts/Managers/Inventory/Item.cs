using UnityEngine;

[CreateAssetMenu(fileName ="New Item",menuName ="Item/Create new Item")]

public class Item : ScriptableObject
{
    public int id;                  // identificador del objeto
    public string itemName;         // nombre del objeto
    public string itemDescription;  // descripción del objeto
    public int value;               // valor del objeto (que usara en caso de mejorar algun stat o recuperar vida)
    public Sprite icon;             // icono del objeto
    public int phase;               // fase actual a la que tenemos mejorada el objeto
    public int maxPhase;            // fase maxima hasta la que se puede mejorar el objeto
    public int price;               // precio por el cual podemos mejorar una fase del objeto (mirar si queremos que siempre sea el mismo)
}
