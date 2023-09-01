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
        mapManager.AddEnemy(gameObject);
    }

    void Start()
    {
        transform.position = mapManager.cellToLocal(coords);
        mapManager.AddOccupiedTile(coords);
        
    }

    //Metodo que realiza la acción de moverse hacia el jugador
    public void ActionMove() 
    {
        //Recibe la direccion del siguiente movimiento hacia el jugador
        Vector3Int dir = MapManager.Instance.FindNextMove(coords);

        MoveDirection(dir);
    }

    //Metodo que realiza la acción de ataque
    public void ActionAttack() 
    {
        ApplyDamage(MapManager.Instance.GetPlayer().GetComponent<Health>(), DamageAmount);
    }

    public void ActionMagicAttack() 
    {
        int damageInkDepleated = DamageAmount * 5;

        if (MapManager.Instance.GetPlayer().GetComponent<Mana>().Current == 0)
        {
            ApplyDamage(MapManager.Instance.GetPlayer().GetComponent<Health>(), damageInkDepleated);
        }
        else 
        {
            ApplyDamage(MapManager.Instance.GetPlayer().GetComponent<Health>(), DamageAmount);
            ApplyDamage(MapManager.Instance.GetPlayer().GetComponent<Mana>(), DamageAmount * 3);
        }
    }

    public void ActionFlee() 
    {
        //Esto es horrible en algun momento lo mejoro
        Vector3Int dir = MapManager.Instance.FindNextMove(coords);

        MoveDirection(dir * -1);
    }

    public bool CanFlee() 
    {
        //Lógica de cuando huir

        int rnd = Random.Range(0, 2);
        if (rnd == 0)
        {
            return true;
        }
        else 
        {
            return false;
        }
    }

    //Metodo que mueve al enemigo en la direccion indicada
    private void MoveDirection(Vector3 dir) 
    {
        if (dir.Equals(Vector3Int.left))
        {
            move(Vector3Int.left);
            flip("left");
        }
        else if (dir.Equals(Vector3Int.right))
        {
            move(Vector3Int.right);
            flip("right");
        }
        else if (dir.Equals(Vector3Int.up))
        {
            move(Vector3Int.up);
        }
        else if (dir.Equals(Vector3Int.down))
        {
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
