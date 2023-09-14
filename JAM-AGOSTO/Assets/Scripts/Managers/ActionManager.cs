using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager : MonoBehaviour
{
    //public List<GameObject> enemyList = new List<GameObject>();
    public GameObject player;
    public int turnStatus;
    private Spawner enemySpawner;

    void Start()
    {
        enemySpawner = GameObject.Find("EnemySpawner").GetComponent<Spawner>();
        turnStatus = 0;
    }

    void Update()
    {
        if (IsEnemyTurn()){
            StartCoroutine(EnemyActions());
        }
    }


    IEnumerator EnemyActions(){
        EnemiesStarting();
        foreach (GameObject enemy in MapManager.Instance.GetEnemyList()){
            enemy.GetComponent<SM_Enemy>().Turn();
            //changeColor(enemy, Color.red);
            yield return new WaitForSeconds(0.05f);
            //changeColor(enemy, Color.white);
        }
        EnemiesDone();

    }

    public bool IsPlayerTurn()
    {
        /*if (turnStatus == 0){
            return true;
        } else{
            return false;
        }*/
        return GameManager.Instance.GameState == GameState.PlayerTurn;
    }

    public bool IsEnemyTurn()
    {
        return GameManager.Instance.GameState == GameState.EnemyTurn;
    }

    /*
    public void playerStarting(){
        turnStatus = 1;
        changeColor(player, Color.green);
        
    }

    public void playerDone(){
        turnStatus = 2;
        changeColor(player, Color.white);
    }
    */

    public void EnemiesStarting(){
        enemySpawner.Spawn();
        turnStatus = 3;
    }

    public void EnemiesDone(){
        turnStatus = 0;
    }

    public void changeColor(GameObject objectColored, Color coloringColor){
        objectColored.GetComponent<Renderer>().material.color = coloringColor;
    }
}
