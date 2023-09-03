using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : Spawner
{
   public override void Spawn()
{
    int counter = 0;
    Vector3Int spawnPosition;
    
    // Obtén las coordenadas del jugador
    Vector3Int playerCoords = MapManager.Instance.GetPlayerCoords();

    // Calcula la distancia entre las coordenadas del jugador y las coordenadas generadas
    int distanceToPlayer = Mathf.RoundToInt(Vector3Int.Distance(spawnerPosition, playerCoords));
    //Debug.Log("La distancia con el jugador es: " + distanceToPlayer);
    
    do 
    {
        // Calcula una posición aleatoria dentro del rango del spawner
        spawnPosition = spawnerPosition + new Vector3Int(Random.Range(-spawnRange, spawnRange), Random.Range(-spawnRange, spawnRange), 0);

        // Comprueba si la distancia es menor o igual a 5 casillas
        if (distanceToPlayer <= visionRange)
        {
            // Verifica si la posición es transitable
            if (MapManager.Instance.isCellTransitable(spawnPosition))
            {
                // Instancia el enemigo en la posición calculada
                Instantiate(prefab, MapManager.Instance.cellToLocal(spawnPosition), Quaternion.identity);

                //Debug.Log("Spawn en " + spawnPosition);
                break; // Sale del bucle una vez que se ha generado una posición válida
            }
        }        
        counter++;
    }
    while (counter < 10);
}









}
