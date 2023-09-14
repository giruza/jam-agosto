using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyController : Damager
{
    public Vector3Int coords;
    public MapManager mapManager;
    public ActionManager actionManager;

    private Vector3Int baseAttackPosition;
    private Vector3Int[] baseAttackDirection;

    //Area Explosion Rango
    private readonly Vector3Int[] neighbourPositions =
    {
        Vector3Int.up,
        Vector3Int.right,
        Vector3Int.down,
        Vector3Int.left,
    
        // if you also wanted to get diagonal neighbours
        Vector3Int.up + Vector3Int.right,
        Vector3Int.up + Vector3Int.left,
        Vector3Int.down + Vector3Int.right,
        Vector3Int.down + Vector3Int.left
    };

    //Area Ataque en Linea Recta
    private static readonly Vector3Int[] lineUpPositions =
    {
        Vector3Int.up,
        new Vector3Int(0, 2),
        new Vector3Int(0, 3),
        new Vector3Int(0, 4),
    };
    private static readonly Vector3Int[] lineDownPositions =
    {
        Vector3Int.down,
        new Vector3Int(0, -2),
        new Vector3Int(0, -3),
        new Vector3Int(0, -4),
    };
    private static readonly Vector3Int[] lineRightPositions =
    {
        Vector3Int.right,
        new Vector3Int(2, 0),
        new Vector3Int(3, 0),
        new Vector3Int(4, 0),
    };
    private static readonly Vector3Int[] lineLeftPositions =
    {
        Vector3Int.left,
        new Vector3Int(-2, 0),
        new Vector3Int(-3, 0),
        new Vector3Int(-4, 0),
    };

    private readonly Vector3Int[][] linePositions = new Vector3Int[][] 
    {
        lineUpPositions,
        lineDownPositions,
        lineRightPositions,
        lineLeftPositions
    };

    //Area Melee
    private static readonly Vector3Int[] meleeUpPositions =
    {
        Vector3Int.up,
        new Vector3Int(-1, 1),
        new Vector3Int(1, 1),
    };
    private static readonly Vector3Int[] meleeDownPositions =
    {
        Vector3Int.down,
        new Vector3Int(-1, -1),
        new Vector3Int(1, -1),
    };
    private static readonly Vector3Int[] meleeRightPositions =
    {
        Vector3Int.right,
        new Vector3Int(1, 1),
        new Vector3Int(1, -1),
    };
    private static readonly Vector3Int[] meleeLeftPositions =
    {
        Vector3Int.left,
        new Vector3Int(-1, 1),
        new Vector3Int(-1, -1),
    };

    private readonly Vector3Int[][] meleePositions = new Vector3Int[][]
    {
        meleeUpPositions,
        meleeDownPositions,
        meleeRightPositions,
        meleeLeftPositions
    };

    void Awake()
    {
        mapManager = FindAnyObjectByType<MapManager>();
        actionManager = FindAnyObjectByType<ActionManager>();
        mapManager.AddEnemy(gameObject);
    }

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

    //Metodo que realiza la acción de ataque
    public void ActionAttack(int damageMultiplier) 
    {
        ApplyDamage(MapManager.Instance.GetPlayer().GetComponent<Health>(), DamageAmount * damageMultiplier);
        Debug.Log("Ha hecho: " + damageMultiplier + " daño");
    }

    //Metodo que realiza la acción de ataque magico
    public void ActionMagicAttack(int damageMultiplier) 
    {
        int damageInkDepleated = DamageAmount * 5;

        if (MapManager.Instance.GetPlayer().GetComponent<Mana>().Current == 0)
        {
            ApplyDamage(MapManager.Instance.GetPlayer().GetComponent<Health>(), damageInkDepleated * damageMultiplier);
        }
        else 
        {
            ApplyDamage(MapManager.Instance.GetPlayer().GetComponent<Health>(), DamageAmount * damageMultiplier);
            ApplyDamage(MapManager.Instance.GetPlayer().GetComponent<Mana>(), DamageAmount * 3 * damageMultiplier);
        }
    }

    //Metodo que realiza el ataque cargado
    public void ActionChargedMeleeAttack(int range) 
    {
        int damageMultiplier = 2;

        if (MapManager.Instance.IsEnemyInRange(gameObject, range))
        {
            if (gameObject.GetComponent<SM_Enemy>()._enemyType == SM_Enemy.EnemyType.Caster_Basic)
            {
                ActionMagicAttack(damageMultiplier);
            }
            else 
            {
                ActionAttack(damageMultiplier);
            }
        }
        else 
        {
            Debug.Log("Toi triste");
        }
        actionManager.changeColor(gameObject, Color.white);
    }

    //Metodo que deja al enemigo en estado de carga
    public void ActionCharging(SM_Enemy.AttackType attackType) 
    {
        //Dependiendo del tipo de ataque hace una cosa u otra
        switch (attackType) 
        {
            case SM_Enemy.AttackType.ChargeAttack:
                actionManager.changeColor(gameObject, Color.yellow);
                break;
            case SM_Enemy.AttackType.RangedExplosion:
                //Si es la explosion, marca el area donde va a explotar en el siguiente turno
                baseAttackPosition = new Vector3Int(MapManager.Instance.GetPlayerCoords().x + Random.Range(-1, 2), MapManager.Instance.GetPlayerCoords().y + Random.Range(-1, 2));
                baseAttackDirection = neighbourPositions;
                MapManager.Instance.ChangeTileColor(baseAttackPosition, neighbourPositions, Color.yellow);
                Debug.Log("Cargando ataque explosivo");
                break;
            case SM_Enemy.AttackType.LineAttack:
                baseAttackDirection = GetDirectionAreaAttack(linePositions);

                MapManager.Instance.ChangeTileColor(baseAttackPosition, baseAttackDirection, Color.yellow);
                Debug.Log("Cargando ataque linea recta");
                break;
            case SM_Enemy.AttackType.AreaMeleeAttack:
                baseAttackDirection = GetDirectionAreaAttack(meleePositions);

                MapManager.Instance.ChangeTileColor(baseAttackPosition, baseAttackDirection, Color.yellow);
                Debug.Log("Cargando ataque melee en area");
                break;
            default:
                break;
        }
    }

    //Se comprueba si el jugador esta en algun tile del ataque y si esta, le hace daño
    public void ActionAreaAttack(int damage) 
    {
        MapManager.Instance.ChangeTileColor(baseAttackPosition, baseAttackDirection, Color.red);

        if (baseAttackPosition == MapManager.Instance.GetPlayerCoords())
        {
            ActionAttack(damage);
        }

        foreach (var pos in baseAttackDirection)
        {
            if ((pos + baseAttackPosition) == MapManager.Instance.GetPlayerCoords())
            {
                ActionAttack(damage);
            }
        }

        MapManager.Instance.ChangeTileColor(baseAttackPosition, baseAttackDirection, Color.white);
    }

    //Metodo que realiza la acción de huir de los enemigos ranged
    public void ActionFlee() 
    {
        Vector3Int dir = MapManager.Instance.FindNextMove(coords);

        //Si no se puede mover 
        if (MapManager.Instance.isCellTransitable(coords + (dir * -1)))
        {
            MoveDirection(dir * -1);
        }
        else
        {
            dir = new Vector3Int(dir.y, dir.x);

            if (Random.Range(0, 2) == 0)
            {
                dir *= -1;
            }

            if (MapManager.Instance.isCellTransitable(coords + dir))
            {
                Debug.Log("Se mueve hacia un lado");
                MoveDirection(dir);
            }
            else if(MapManager.Instance.isCellTransitable(coords + (dir * -1)))
            {
                Debug.Log("Se mueve hacia el otro lado");
                MoveDirection(dir * -1);
            }
            else 
            {
                //Si no se pueden mover, que los ranged ataquen
                //---------------Poner un ataque melee para los ranged mas debil (?)---------//
                ActionMagicAttack(1);
            }
        }

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

    //Metodo que comprueba si el jugador esta en linea con el enemigo
    public bool CheckPlayerInLine()
    {
        Vector3 prueba = Vector3.Normalize(MapManager.Instance.GetPlayerCoords() - coords);
        //Debug.Log(prueba); 

        if (prueba.Equals(Vector3.up) || prueba.Equals(Vector3.down) || prueba.Equals(Vector3.right) || prueba.Equals(Vector3.left)) 
        {
            return true;
        }
        
        return false;
    }

    //Metodo que actualiza la posicion base del ataque y las direcciones del mismo
    private Vector3Int[] GetDirectionAreaAttack(Vector3Int[][] positions) 
    {
        Vector3 prueba = Vector3.Normalize(MapManager.Instance.GetPlayerCoords() - coords);

        if (prueba.Equals(Vector3.up))
        {
            Debug.Log("Direccion arriba");
            baseAttackPosition = Vector3Int.up + coords;
            return positions[0];
        } 
        else if (prueba.Equals(Vector3.down)) 
        {
            Debug.Log("Direccion abajo");
            baseAttackPosition = Vector3Int.down + coords;
            return positions[1];
        }
        else if (prueba.Equals(Vector3.right))
        {
            Debug.Log("Direccion derecha");
            baseAttackPosition = Vector3Int.right + coords;
            return positions[2];
        }
        else if (prueba.Equals(Vector3.left))
        {
            Debug.Log("Direccion izquierda");
            baseAttackPosition = Vector3Int.left + coords;
            return positions[3];
        }

        return lineDownPositions;
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
