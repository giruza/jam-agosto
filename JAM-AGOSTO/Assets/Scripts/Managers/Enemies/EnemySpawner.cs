using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : Spawner
{

    void Start()
    {
        gridPos = new Vector3Int(0, 0, 0); // Establece la posición en la cuadrícula
        spawnRange = 5; // Establece el rango del spawner
    }
    public override void Spawn()
    {
        int counter = 0;
        Vector3Int spawnPosition;
        do 
        {
            // Calcula una posición aleatoria dentro del rango del spawner
            spawnPosition = gridPos + new Vector3Int(Random.Range(-spawnRange, spawnRange), Random.Range(-spawnRange, spawnRange), 0);
            //Debug.Log("Intentando spawnear en" + spawnPosition);
            counter++;
        }
        while (!MapManager.Instance.isCellTransitable(spawnPosition) && counter < 10);
        // Instancia el enemigo en la posición calculada
        Instantiate(prefab, MapManager.Instance.cellToLocal(spawnPosition), Quaternion.identity);
        //Debug.Log("Spawn en " + spawnPosition);




    }
    private Vector3Int getSpawnCoords()
    {
        Vector3Int parentPos = MapManager.Instance.localToCell(Vector3Int.FloorToInt(transform.parent.position));
        
        return parentPos;
    }
}
