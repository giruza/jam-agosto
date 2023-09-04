using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SM_Enemy_Basic : SM_Enemy 
{
    public override void Turn() 
    {
        _state = ActualizarEstado();

        //Realizar las acciones
        switch (_state)
        {
            case State.Idle:
                //A�adir Metodo moverse de forma random o en rutas
                Debug.Log(gameObject.name + ": Callao");
                break;
            case State.Attack:
                //Action Atacar
                if (_enemyType == EnemyType.Caster_Basic)
                    enemyController.ActionMagicAttack();
                else
                    enemyController.ActionAttack();
                break;
            case State.Move:
                //Accion moverse al jugador
                enemyController.ActionMove();
                break;
            case State.Flee:
                //Accion huir del jugador
                enemyController.ActionFlee();
                break;
            default:
                break;
        }
        //Terminar turno
    }


    //Metodo para comprobar el estado del enemigo
    private State ActualizarEstado() 
    {
        if (MapManager.Instance.IsEnemyInRange(gameObject, MOVEMENT_RANGE))
        {
            if (MapManager.Instance.IsEnemyInRange(gameObject, ATTACK_RANGE))
            {
                if (MapManager.Instance.IsEnemyInRange(gameObject, 1) && (_enemyType == EnemyType.Range_Basic || _enemyType == EnemyType.Caster_Basic))
                {
                    if (enemyController.CanFlee())
                    {
                        return State.Flee;
                    }
                    else
                    {
                        return State.Attack;
                    }
                }
                else
                {
                    return State.Attack;
                }
            }
            else 
            {
                return State.Move;
            }
        }
        return State.Idle;
    }
}
