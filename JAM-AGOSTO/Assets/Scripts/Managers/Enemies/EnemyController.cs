using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Damager
{
    public Vector3Int coords;
    public MapManager mapManager;
    public ActionManager actionManager;


    void Awake()
    {
        mapManager = FindAnyObjectByType<MapManager>();
        actionManager = FindAnyObjectByType<ActionManager>();
        mapManager.AddEnemy(gameObject);
    }


    // Start is called before the first frame update
    void Start()
    {
        coords = mapManager.localToCell(Vector3Int.FloorToInt(transform.position));
        //transform.position = mapManager.cellToLocal(coords);
        mapManager.AddOccupiedTile(coords);
        
    }

    //Metodo que realiza la acción de moverse hacia el jugador
    public void ActionMove() 
    {
        //Recibe la direccion del siguiente movimiento hacia el jugador
        Vector3Int dir = MapManager.Instance.FindNextMove(coords);

        MoveDirection(dir);
    }

    public void ActionAttack() 
    {
        ApplyDamage(MapManager.Instance.GetPlayer().GetComponent<Health>());
    }

    //Metodo que mueve al enemigo en la direccion indicada
    private void MoveDirection(Vector3 dir) 
    {
        if (dir.Equals(Vector3Int.left))
        {
            //Debug.Log(gameObject.name + ": me muevo hacia la izquierda");
            move(Vector3Int.left);
            flip("left");
        }
        else if (dir.Equals(Vector3Int.right))
        {
            //Debug.Log(gameObject.name + ": me muevo hacia la derecha");
            move(Vector3Int.right);
            flip("right");
        }
        else if (dir.Equals(Vector3Int.up))
        {
            //Debug.Log(gameObject.name + ": me muevo hacia arriba");
            move(Vector3Int.up);
        }
        else if (dir.Equals(Vector3Int.down))
        {
            //Debug.Log(gameObject.name + ": me muevo hacia abajo");
            move(Vector3Int.down);
        }
    }

    void move(Vector3Int direction)
    {
        if (mapManager.isCellTransitable(coords + direction))
        {
            mapManager.RemoveOccupiedTile(coords);
            coords += direction;
            transform.position = mapManager.cellToLocal(coords);
            mapManager.AddOccupiedTile(coords);
            //Debug.Log("Movimiento a: " + coords);
        }
    }

    private void flip(string direction)
    {
        if (direction == "right")
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        if (direction == "left")
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }
}
