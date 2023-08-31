using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pathfinding: MonoBehaviour 
{
    //Herramientas de Debug
    private static readonly Color PathColor = new Color(0.65f, 0.35f, 0.35f);
    private static readonly Color OpenColor = new Color(.4f, .6f, .4f);
    private static readonly Color ClosedColor = new Color(0.35f, 0.4f, 0.5f);

    //Metodo de Pathfinding A*
    public static List<NodeBase> FindPath(NodeBase startNode, NodeBase targetNode) 
    {
        var toSearch = new List<NodeBase>() { startNode };
        var processed = new List<NodeBase>();

        while (toSearch.Any()) 
        {
            var current = toSearch[0];
            foreach (var t in toSearch)
                if (t.F < current.F || t.F == current.F && t.H < current.H) current = t;

            processed.Add(current);
            toSearch.Remove(current);

            //Herramienta de Debug
            current.SetColor(ClosedColor);

            //Devolver el camino hacia el objetivo 
            if (current == targetNode) 
            {
                var currentPathTile = targetNode;
                var path = new List<NodeBase>();
                var count = 100;
                while (currentPathTile != startNode) 
                {
                    path.Add(currentPathTile);
                    currentPathTile = currentPathTile.Connection;
                    count--;
                    if (count < 0) break;
                }

                //Herramientas de Debug

                foreach (var tile in path) tile.SetColor(PathColor);
                startNode.SetColor(PathColor);
                Debug.Log(path.Count);

                return path;
            }

            //Añadir los nodos vecinos al nodo que estas buscando
            //------------Maybe ponerlo en los nodos-------------
            var neighbors = new List<NodeBase>();

            foreach (var tile in NodeBase.Dirs.Select(dir => MapManager.Instance.GetTileAtPosition(current.Pos + dir)).Where(tile => tile != null))
            {
                neighbors.Add(tile);
            }

            //Hacer calculos por cada vecino que no se haya procesado y se pueda caminar
            foreach (var neighbor in neighbors.Where(t => t.Walkable && !processed.Contains(t))) 
            {
                var inSearch = toSearch.Contains(neighbor);

                var costToNeighbor = current.G + current.GetDistance(neighbor);

                if (!inSearch || costToNeighbor < neighbor.G) 
                {
                    neighbor.SetG(costToNeighbor);
                    neighbor.SetConnection(current);

                    if (!inSearch)
                    {
                        neighbor.SetH(neighbor.GetDistance(targetNode));
                        toSearch.Add(neighbor);

                        //Herramienta de Debug
                        neighbor.SetColor(OpenColor);
                    }
                }
            }
        }
        return null;
    }
}
