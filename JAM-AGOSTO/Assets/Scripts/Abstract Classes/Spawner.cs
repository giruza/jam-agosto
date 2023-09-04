using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public abstract class Spawner : MonoBehaviour
{
    [SerializeField]
    public GameObject prefab; // El objeto que se generará
    //[SerializeField]
    //private float spawnInterval = 1f; //Cantidad de turnos [WIP] // El intervalo de tiempo entre la generación de objetos

    [SerializeField]
    public int spawnRange; // Rango donde pueden spawnear los enemigos

    public int visionRange; // Rango de vision del spawner (la distancia a la que tienes que estar tú para que empiece a funcionar)
    
    public Vector3Int spawnerPosition; // Posición del spawner en la grid 

    public abstract void Spawn(); // Método abstracto que debe implementarse 


}
