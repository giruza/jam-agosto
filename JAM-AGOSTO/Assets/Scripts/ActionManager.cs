using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager : MonoBehaviour
{
    public List<GameObject> enemyList = new List<GameObject>();
    public GameObject player;
    public int turnStatus;

    void Awake(){
    }
    // Start is called before the first frame update
    void Start()
    {
        turnStatus = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsEnemyTurn()){
            StartCoroutine(EnemyActions());
        }
    }


    IEnumerator EnemyActions(){
        EnemiesStarting();
        foreach (GameObject enemy in enemyList){
            enemy.GetComponent<EnemyController>().Action();
            changeColor(enemy, Color.red);
            yield return new WaitForSeconds(1.0f);
            changeColor(enemy, Color.white);
        }
        EnemiesDone();

    }
    public void AddEnemy(GameObject enemy){
        enemyList.Add(enemy);
    }

    public bool IsPlayerTurn(){
        if (turnStatus == 0){
            return true;
        } else{
            return false;
        }
    }

    public bool IsEnemyTurn(){
        if (turnStatus == 2){
            return true;
        } else{
            return false;
        }
    }

    public void playerStarting(){
        turnStatus = 1;
        changeColor(player, Color.green);
    }

    public void playerDone(){
        turnStatus = 2;
        changeColor(player, Color.white);
    }

    public void EnemiesStarting(){
        turnStatus = 3;
    }

    public void EnemiesDone(){
        turnStatus = 0;
    }

    public void changeColor(GameObject objectColored, Color coloringColor){
        objectColored.GetComponent<Renderer>().material.color = coloringColor;
    }

}
