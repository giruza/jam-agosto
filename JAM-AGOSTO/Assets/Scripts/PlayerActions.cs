using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
//using UnityEngine.InputSystem;      

public class PlayerActions : MonoBehaviour
{
    public Vector3Int coords;
    public MapManager mapManager;
    public ActionManager actionManager;
    
    /*
    public CustomInput input = null;
    private Vector3 movement = Vector3.zero;
    void Awake(){
        input = new CustomInput();
    }

    void OnEnable(){
        input.Enable();
        input.Player.Movement.performed += OnMovementPerformed;
        input.Player.Movement.canceled += OnMovementCancelled;
    }

    private void OnMovementPerformed(InputAction.CallbackContext value){
        movement = value.ReadValue<Vector3>();
    }
    private void OnMovementCancelled(InputAction.CallbackContext value){
        movement = Vector3.zero;
    }
    */
    // Start is called before the first frame update
    void Start()
    {
        transform.position = mapManager.cellToLocal(coords);
        mapManager.AddOccupiedTile(coords);
    }

    // Update is called once per frame
    void Update()
    {
        if(actionManager.IsPlayerTurn() && (Input.inputString != "") /*Input.anyKey*/){
            //Actions();
            switch(Input.inputString.ToUpper()){ 
                case "A": 
                    if (mapManager.isCellTransitable(coords+Vector3Int.left)){
                        actionManager.playerStarting();
                        StartCoroutine(move(Vector3Int.left));
                        flip("left");
                    }
                    break;
                case "D":
                    if (mapManager.isCellTransitable(coords+Vector3Int.right)){
                        actionManager.playerStarting();
                        StartCoroutine(move(Vector3Int.right));
                        flip("right");
                    }
                    break;
                case "W":
                    if (mapManager.isCellTransitable(coords+Vector3Int.up)){
                        actionManager.playerStarting();
                        StartCoroutine(move(Vector3Int.up));
                    }
                    break;
                case "S":
                    if (mapManager.isCellTransitable(coords+Vector3Int.down)){
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
        else if (actionManager.IsPlayerTurn() && Input.GetMouseButtonDown(0))
        {
            actionManager.playerStarting();
            coords = mapManager.GetClickPositionCell();
            GameObject enemyInPosition = mapManager.GetEnemyInPosition(coords);
            if (enemyInPosition)
            {
                Debug.Log("Enemigo en la celda");
            }
            else 
            {
                Debug.Log("No hay enemigos cerca");
            }
            actionManager.playerDone();
        }
    }

/*
    void Actions(){
        if (Input.GetKey(KeyCode.A))
        {
            StartCoroutine(move(Vector3Int.left));
        }
        if (Input.GetKey(KeyCode.D))
        {
            StartCoroutine(move(Vector3Int.right));
        }
        if (Input.GetKey(KeyCode.W))
        {
            StartCoroutine(move(Vector3Int.up));
        }
        if (Input.GetKey(KeyCode.S))
        {
            StartCoroutine(move(Vector3Int.down));
        }

    }
*/

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

    /*void OnGUI() {
        Event e = Event.current;
        if (e.isKey)
            Debug.Log("Detected key code: " + e.keyCode);
        
    }*/
}


