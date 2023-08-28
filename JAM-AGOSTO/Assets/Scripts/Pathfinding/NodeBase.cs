using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NodeBase : MonoBehaviour
{
    [SerializeField] protected SpriteRenderer _renderer;

    public List<NodeBase> Neighbors { get; private set; }

    public static readonly List<Vector3Int> Dirs = new ()
    {
        new Vector3Int(0, 1), new Vector3Int(-1, 0), new Vector3Int(0, -1), new Vector3Int(1, 0),
    };

    public NodeBase Connection { get; private set; }
    public bool Walkable { get; private set; }
    public float G { get; private set; }
    public float H { get; private set; }
    public float F => G + H;

    public void SetConnection(NodeBase nodeBase) => Connection = nodeBase;

    public void SetG(float g) => G = g;

    public void SetH(float h) => H = h;

    public Vector3Int Pos { get; private set; }

    //Inicializar los nodos
    public void Init(bool walkable, Vector3Int pos) 
    {
        Walkable = walkable;
        Pos = pos;
        transform.position = Pos;
    }

    //---------Terminar de implementar los vecinos en el nodo----------
    public void CacheNeighbors() 
    {
        foreach (var tile in Dirs.Select(dir => MapManager.Instance.GetTileAtPosition(Pos + dir)).Where(tile => tile != null)) 
        {
            Neighbors.Add(tile);
        }
    }

    public void SetColor(Color color) => _renderer.color = color;

    //Metodo para calcular la distancia hacia el siguiente nodo
    public float GetDistance(NodeBase otherNode) 
    {
        var dist = new Vector2Int(Mathf.Abs((int)Pos.x - (int)otherNode.Pos.x), Mathf.Abs((int)Pos.y - (int)otherNode.Pos.y));

        var lowest = Mathf.Min(dist.x, dist.y); 
        var highest = Mathf.Max(dist.x, dist.y);

        var horizontalMovesRequire = highest - lowest;

        return lowest + horizontalMovesRequire;
    }
}
