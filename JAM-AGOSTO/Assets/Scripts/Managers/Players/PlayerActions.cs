using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
//using UnityEngine.InputSystem;      

public class PlayerActions : Damager
{
    public Vector3Int coords;
    public MapManager mapManager;
    public ActionManager actionManager;
    public Animator animator;

    private void Awake()
    {
        BasicAttackRange = 1;
    }

    void Start()
    {
        transform.position = mapManager.cellToLocal(coords);
        mapManager.AddOccupiedTile(coords);
    }

    void Update()
    {

        if (actionManager.IsPlayerTurn() && (Input.inputString != "")) {
            //Actions();
            animator.SetBool("isAttacking", false);
            switch (Input.inputString.ToUpper()) {
                case "A":
                    if (mapManager.isCellTransitable(coords + Vector3Int.left)) {
                        actionManager.playerStarting();
                        StartCoroutine(move(Vector3Int.left));
                        flip("left");
                    }
                    break;
                case "D":
                    if (mapManager.isCellTransitable(coords + Vector3Int.right)) {
                        actionManager.playerStarting();
                        StartCoroutine(move(Vector3Int.right));
                        flip("right");
                    }
                    break;
                case "W":
                    if (mapManager.isCellTransitable(coords + Vector3Int.up)) {
                        actionManager.playerStarting();
                        StartCoroutine(move(Vector3Int.up));
                    }
                    break;
                case "S":
                    if (mapManager.isCellTransitable(coords + Vector3Int.down)) {
                        actionManager.playerStarting();
                        StartCoroutine(move(Vector3Int.down));
                    }
                    break;
                case "E":
                    mapManager.GetInteractuableInRange(coords, 1);
                    break;
                default:
                    break;
            }
        } 
        else if(actionManager.IsPlayerTurn() && Input.GetMouseButtonDown(0))
        {
            var mousePos = mapManager.GetClickPositionCell();
            GameObject enemyInPosition = mapManager.GetEnemyInPosition(mousePos);
            if (enemyInPosition && mapManager.IsEnemyInRange(enemyInPosition, BasicAttackRange))
            {
                animator.SetTrigger("TriggerAttack");
                actionManager.playerStarting();
                ApplyDamage(enemyInPosition.GetComponent<Health>(), DamageAmount);
                Debug.Log(enemyInPosition.GetComponent<Health>().Current);
                actionManager.playerDone();
            }
            else 
            {
                //animator.SetBool("isAttacking", false);
                Debug.Log("No hay enemigos cerca");
            }
        }
    }

    IEnumerator move(Vector3Int direction){
        mapManager.RemoveOccupiedTile(coords);
        coords += direction;
        transform.position = mapManager.cellToLocal(coords);
        mapManager.AddOccupiedTile(coords);
        Debug.Log("Movimiento a: " + coords);
        yield return new WaitForSeconds(0.1f);
        actionManager.playerDone();
    }

    public void flip(string direction){
        if(direction == "right"){
            GetComponent<SpriteRenderer>().flipX = false;
        }
        if(direction == "left"){
            GetComponent<SpriteRenderer>().flipX = true;
        } 
    }
}


