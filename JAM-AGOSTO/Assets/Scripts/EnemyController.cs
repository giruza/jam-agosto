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
        transform.position = mapManager.cellToLocal(coords);
        mapManager.AddOccupiedTile(coords);
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    void move(Vector3Int direction){
        if (mapManager.isCellTransitable(coords+direction)){
            mapManager.RemoveOccupiedTile(coords);
            coords += direction;
            transform.position = mapManager.cellToLocal(coords);
            mapManager.AddOccupiedTile(coords);
            //Debug.Log("Movimiento a: " + coords);
        } else {
            //Debug.Log("No pasarás");
        }
    }

    public void Action(){
        int IA = Random.Range(1, 5);

        switch (IA){
            case 1:
                move(Vector3Int.left);
                flip("left");
                break;
            case 2:
                move(Vector3Int.right);
                flip("right");
                break;
            case 3:
                move(Vector3Int.up);
                break;
            case 4:
                move(Vector3Int.down);
                break;
            default:
                break;
        }
        
        
        
        Debug.Log(name + ": AAAARGH!");
    }

        public void flip(string direction){
        if(direction == "right"){
            GetComponent<SpriteRenderer>().flipX = true;
        }
        if(direction == "left"){
            GetComponent<SpriteRenderer>().flipX = false;
        } 
    }

}
