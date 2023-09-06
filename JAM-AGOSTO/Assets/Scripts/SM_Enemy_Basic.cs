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
                else if (enemyController.CanFlee() && _enemyType == EnemyType.Range_Basic)
                    enemyController.ActionRangedExplosion();
                else
                    enemyController.ActionAttack(1);
                break;
            //-------------TO DO: Arbol de comportamientos ataque cargado (JIJIJA)--------------------//
            case State.ChargeAttack:
                if (_attackType == AttackType.RangedExplosion) 
                {
                    enemyController.ActionRangedExplosion();
                }
                if(_attackType == AttackType.ChargeAttack) 
                {
                    if (_enemyType == EnemyType.Melee_Basic || _enemyType == EnemyType.Hybrid)
                        enemyController.ActionChargedMeleeAttack(1);
                    else if (_enemyType == EnemyType.Caster_Basic || _enemyType == EnemyType.Range_Basic)
                        enemyController.ActionChargedMeleeAttack(ATTACK_RANGE);
                }
                break;
            case State.Charging:
                if (_enemyType == EnemyType.Caster_Basic || _enemyType == EnemyType.Range_Basic) 
                {
                    //if (enemyController.CanFlee()) 
                    //{
                        _attackType = AttackType.RangedExplosion;
                    //}
                    //else 
                    //{
                    //    _attackType = AttackType.ChargeAttack;
                    //}
                }
                enemyController.ActionCharging(_attackType);
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
        //Si esta cargando el ataque no comprueba nada mas y realiza el ataque cargado
        if(_state == State.Charging) 
        {
            return State.ChargeAttack;
        }
        //Si esta en rango de vision y no esta en rango de ataque, se mueve hacia el jugador
        //En caso de que no ocurra nada de eso se queda Idle
        if (MapManager.Instance.IsEnemyInRange(gameObject, MOVEMENT_RANGE))
        {
            //Comprueba si esta a rango de ataque para hacer diferentes comprobaciones
            if (MapManager.Instance.IsEnemyInRange(gameObject, ATTACK_RANGE))
            {
                //Si esta a melee y es un enemigo de rango, decide si atacar o huir
                //En cambio si es un enemigo melee decide si atacar normal o hacer un ataque cargado
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
                //Si no esta a distancia melee, si es un enemigo a rango decide si hacer un ataque normal o cargar un ataque
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
