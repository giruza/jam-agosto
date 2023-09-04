using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SquareGrid : MonoBehaviour
{
    [SerializeField] private NodeBase nodeBasePrefab;
    [SerializeField] private Tilemap tilemap;

    public Dictionary<Vector3Int, NodeBase> GenerateGrid()
    {
        //Diccionario de nodos y creacion de la grid en la escena
        var tiles = new Dictionary<Vector3Int, NodeBase>();
        var grid = new GameObject
        {
            name = "Grid"
        };

        //Creacion de los nodos dentro del tilemap
        foreach (var pos in tilemap.cellBounds.allPositionsWithin) 
        {
            Vector3Int location = new Vector3Int(pos.x, pos.y);

            var tile = Instantiate(nodeBasePrefab, grid.transform);
            tile.Init(MapManager.Instance.isCellWalkable(location), location);
            if (tiles.ContainsKey(location)) 
            {
                Debug.Log("Se ha repetido");
                break;
            }
            tiles.Add(location, tile);
        }
        return tiles;
    }
}
