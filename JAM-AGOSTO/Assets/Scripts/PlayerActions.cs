using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerActions : MonoBehaviour
{
    public Vector3Int coords;
    public MapManager mapManager;
    public ActionManager actionManager;



    // Start is called before the first frame update
    void Start()
    {
        move(Vector3Int.zero);
    }

    // Update is called once per frame
    void Update()
    {
        if(actionManager.IsPlayerTurn()){
            Action();
        }
        
    }

    void Action(){
        if (Input.GetKeyDown(KeyCode.A))
        {
            StartCoroutine(move(Vector3Int.left));
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            StartCoroutine(move(Vector3Int.right));
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            StartCoroutine(move(Vector3Int.up));
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            StartCoroutine(move(Vector3Int.down));
        }
    }


    IEnumerator move(Vector3Int direction){
        if (mapManager.isCellTransitable(coords+direction)){
            actionManager.playerStarting();
            coords += direction;
            transform.position = mapManager.cellToLocal(coords);
            Debug.Log("Movimiento a: " + coords);
            yield return new WaitForSeconds(1.0f);
            actionManager.playerDone();

        } else {
            Debug.Log("No pasarás");
        }
    }
}


