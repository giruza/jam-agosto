using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InteractuableList : MonoBehaviour
{
    public List<GameObject> interactuable = new();

    public void AddInteractuableTile(GameObject objectItem)
    {
        interactuable.Add(objectItem);
    }

    public void RemoveInteractuableTile(GameObject objectItem){
        interactuable.Remove(objectItem);
    }

    public List<GameObject> GetListItem()
    {
        return interactuable;
    }


}
