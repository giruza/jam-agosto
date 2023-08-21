using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class movimiento : MonoBehaviour
{
    public Vector3Int coords;
    public MapManager mapManager;
    public InteractuableList interactuableList;


    // Start is called before the first frame update
    void Start()
    {
        move(Vector3Int.zero);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            checkInteraction();
        }
        

        if (Input.GetKeyDown(KeyCode.A))
        {
            move(Vector3Int.left);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            move(Vector3Int.right);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            move(Vector3Int.up);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            move(Vector3Int.down);
        }
    }
    void move(Vector3Int direction){
        if (checkMove(coords+direction)){
            coords += direction;
            transform.position = mapManager.cellToLocal(coords);
            Debug.Log("Movimiento a: " + coords);
        } else {
            Debug.Log("No pasarás");
        }
    }

    bool checkMove(Vector3Int newCoords){
        return mapManager.isCellTransitable(newCoords);
    }

    void checkInteraction()
    {
        foreach(GameObject semen in interactuableList.GetListItem())
        {
            Debug.Log(semen);
        }
    }
}


