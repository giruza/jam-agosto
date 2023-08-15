using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public Vector3Int coords;
    public MapManager mapManager;
    public ActionManager actionManager;


    void Awake(){
        actionManager.AddEnemy(gameObject);
    }


    // Start is called before the first frame update
    void Start()
    {
        move(Vector3Int.zero);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void move(Vector3Int direction){
        if (checkMove(coords+direction)){
            coords += direction;
            transform.position = mapManager.cellToLocal(coords);
            //Debug.Log("Movimiento a: " + coords);
        } else {
            //Debug.Log("No pasarás");
        }
    }

    bool checkMove(Vector3Int newCoords){
        return mapManager.isCellTransitable(newCoords);
    }

    public bool Action(){
        Debug.Log(this.name + " actua");
        return true;
    }

    IEnumerator Waiting(float seconds) {
        yield return new WaitForSeconds(seconds);
        print(Time.time);
    }

}
