using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactuable : MonoBehaviour
{
    public MapManager mapManager;
    public Vector3Int coords;
    
    void Awake(){
        transform.position = mapManager.cellToLocal(coords);
        mapManager.AddOccupiedTile(coords);
        mapManager.AddInteractuable(gameObject, coords);
        mapManager.AddEnemy(gameObject, coords);
    }

    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Use(){
        Debug.Log(name + ": Hola");
    }
}
