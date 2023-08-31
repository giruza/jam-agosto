using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SM_Enemy_Melee : SM_Enemy 
{
    public override void Turn() 
    {
        ActualizarEstado();

        //Realizar las acciones
        switch (_state)
        {
            case State.Idle:
                Debug.Log(gameObject.name + ": Callao");
                break;
            case State.Attack:
                //Action Atacar
                Debug.Log("Estoy Atacando");
                enemyController.ActionAttack();
                break;
            case State.Move:
                enemyController.ActionMove();
                break;
            default:
                break;
        }
        //Terminar turno
    }


    //Metodo para comprobar el estado del enemigo
    private void ActualizarEstado() 
    {
        State estado = State.Idle;

        if (MapManager.Instance.IsEnemyInRange(gameObject, ATTACK_RANGE))
        {
            Debug.Log(MapManager.Instance.IsEnemyInRange(gameObject, ATTACK_RANGE));
            estado = State.Attack;
        }
        else if(MapManager.Instance.IsEnemyInRange(gameObject, MOVEMENT_RANGE)) 
        {
            Debug.Log(MapManager.Instance.IsEnemyInRange(gameObject, MOVEMENT_RANGE));
            estado = State.Move;
        }

        _state = estado;
    }
}
