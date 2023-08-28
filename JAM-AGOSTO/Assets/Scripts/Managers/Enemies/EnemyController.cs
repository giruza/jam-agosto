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

    public void Action()
    {
        Vector3Int dir = MapManager.Instance.FindNextMove(coords);

        Debug.Log(gameObject.name + ": " + dir);

        if (dir.Equals(Vector3Int.left)) 
        {
            Debug.Log(gameObject.name + ": me muevo hacia la izquierda");
            move(Vector3Int.left);
        }
        else if(dir.Equals(Vector3Int.right))
        {
            Debug.Log(gameObject.name + ": me muevo hacia la derecha");
            move(Vector3Int.right);
        }
        else if (dir.Equals(Vector3Int.up))
        {
            Debug.Log(gameObject.name + ": me muevo hacia arriba");
            move(Vector3Int.up);
        }
        else if (dir.Equals(Vector3Int.down))
        {
            Debug.Log(gameObject.name + ": me muevo hacia abajo");
            move(Vector3Int.down);
        }

        //switch (dir){
        //    case Vector3Int v when v.Equals(Vector3Int.left):
        //        move(Vector3Int.left);
        //        flip("left");
        //        break;
        //    case Vector3Int v when v.Equals(Vector3Int.right):
        //        move(Vector3Int.right);
        //        flip("right");
        //        break;
        //    case Vector3Int v when v.Equals(Vector3Int.up):
        //        move(Vector3Int.up);
        //        break;
        //    case Vector3Int v when v.Equals(Vector3Int.down):
        //        move(Vector3Int.down);
        //        break;
        //    default:
        //        break;
        //}
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
