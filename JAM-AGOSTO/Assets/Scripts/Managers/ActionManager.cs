using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager : MonoBehaviour
{
    public GameObject player;
    private Spawner enemySpawner;

    private void Awake()
    {
        GameManager.GameStateChanged += EnemyTurn;
    }

    void Start()
    {
        enemySpawner = GameObject.Find("EnemySpawner").GetComponent<Spawner>();
    }

    private void OnDestroy()
    {
        GameManager.GameStateChanged -= EnemyTurn;
    }

    private void EnemyTurn(GameState state) 
    {
        if(state == GameState.EnemyTurn) 
        {
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

    public void EnemiesStarting(){
        enemySpawner.Spawn();
    }

    public void EnemiesDone(){
        //Cambiar al turno del jugador
        GameManager.Instance.SetGameState(GameState.PlayerTurn);  
    }

    public void changeColor(GameObject objectColored, Color coloringColor){
        objectColored.GetComponent<Renderer>().material.color = coloringColor;
    }
}
