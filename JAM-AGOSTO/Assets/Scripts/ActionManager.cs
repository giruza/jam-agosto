using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager : MonoBehaviour
{
    public List<GameObject> enemyList = new List<GameObject>();
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
            yield return new WaitForSeconds(1.0f);
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
    }

    public void playerDone(){
        turnStatus = 2;
    }

    public void EnemiesStarting(){
        turnStatus = 3;
    }

    public void EnemiesDone(){
        turnStatus = 0;
    }


}
