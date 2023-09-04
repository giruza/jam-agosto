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
                //Añadir Metodo moverse de forma random o en rutas
                //Debug.Log(gameObject.name + ": Callao");
                break;
            case State.Attack:
                //Action Atacar
                if (_enemyType == EnemyType.Caster_Basic)
                    enemyController.ActionMagicAttack(1);
                else
                    enemyController.ActionAttack(1);
                break;
            //-------------TO DO: Arbol de comportamientos ataque cargado (JIJIJA)--------------------//
            case State.ChargeAttack:
                if (_enemyType == EnemyType.Melee_Basic || _enemyType == EnemyType.Hybrid)
                    enemyController.ActionChargedMeleeAttack(1);
                else if (_enemyType == EnemyType.Caster_Basic || _enemyType == EnemyType.Range_Basic)
                    enemyController.ActionChargedMeleeAttack(ATTACK_RANGE);
                break;
            case State.Charging:
                enemyController.ActionCharging();
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
        if(_state == State.Charging) 
        {
            return State.ChargeAttack;
        }
        if (MapManager.Instance.IsEnemyInRange(gameObject, MOVEMENT_RANGE))
        {
            if (MapManager.Instance.IsEnemyInRange(gameObject, ATTACK_RANGE))
            {
                if (MapManager.Instance.IsEnemyInRange(gameObject, 1))
                {
                    if ((_enemyType == EnemyType.Range_Basic || _enemyType == EnemyType.Caster_Basic))
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
                        if (enemyController.CanFlee())
                        {
                            return State.Charging;
                        }
                        else 
                        {
                            return State.Attack;
                        }
                    }
                }
                else
                {
                    if ((_enemyType == EnemyType.Range_Basic || _enemyType == EnemyType.Caster_Basic)) 
                    {
                        if (enemyController.CanFlee())
                        {
                            return State.Charging;
                        }
                        else
                        {
                            return State.Attack;
                        }
                    }
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
