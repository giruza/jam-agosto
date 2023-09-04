using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    //Instancia de MapManager para llamar a los metodos
    public static MapManager Instance;

    public Tilemap tilemap;
    public Tilemap foremap;
    public List<TileData> tileDatas;
    public Dictionary<TileBase,TileData> dataFromTiles;
    private List<GameObject> enemyList = new();
    public List<Vector3Int> occupiedTiles = new();
    public Dictionary<GameObject, Vector3Int> interactuables = new();
    public TileBase mouseTile; 
    public float percentageAlpha;
    //public Vector3Int location;
    //public TileBase clickedTile;

    public Camera mainCamera;
    public Camera followCamera;

    //Variables de Pathfinding
    [SerializeField] private PlayerActions player;
    [SerializeField] private SquareGrid _squareGrid;
    public Dictionary<Vector3Int, NodeBase> Tiles { get; private set; }


    private void Awake(){
        dataFromTiles =  new  Dictionary<TileBase,TileData>();
        foreach (var tileData in tileDatas){
            foreach(var tile in tileData.tiles){
                dataFromTiles.Add(tile,tileData);
            }
        }

        Instance = this;
    }

    void Start()
    {
        mainCamera.enabled = false;
        followCamera.enabled = true;
        foreach (var pos in foremap.cellBounds.allPositionsWithin){
            Vector3Int location = new Vector3Int(pos.x, pos.y, 0);
            if (foremap.HasTile(location)){
                foremap.SetTileFlags(location, TileFlags.None);
            }
        }

        Tiles = _squareGrid.GenerateGrid();
    }


    void Update()
    {
        OccupiedCellsInForemap();
        if (Input.GetKeyDown(KeyCode.C)) {
            mainCamera.enabled = !mainCamera.enabled;
            followCamera.enabled = !followCamera.enabled;
        }

        

        /*if (Input.GetMouseButtonDown(0))
        {
            Vector3 mp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            location = tilemap.WorldToCell(mp);
            clickedTile = tilemap.GetTile(location);
            Debug.Log("CellToWorld" + tilemap.CellToLocal(location));
        }*/
    }

    public Vector3Int GetClickPositionCell()
    {
        Vector3 worldCoord = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int cellPosition = tilemap.WorldToCell(worldCoord);
        return cellPosition;
    }

    public void AddEnemy(GameObject enemy)
    {
        enemyList.Add(enemy);
    }

    public void RemoveEnemy(GameObject enemy) 
    {
        enemyList.Remove(enemy);
    }

    public void AddInteractuable(GameObject enemy, Vector3Int Coords){
        interactuables.Add(enemy,Coords);
        //interactuableList.Add(enemy);
    }

    public bool GetInteractuableInRange(Vector3Int coords, int range){
        foreach(KeyValuePair<GameObject, Vector3Int> entry in interactuables){
            if (range >= Mathf.Abs(entry.Value.x - coords.x) + Mathf.Abs(entry.Value.y - coords.y)){
                //Debug.Log(entry.Key + " / " + entry.Value);
                entry.Key.GetComponent<Interactuable>().Use();
                return true;
            }
        }
        return false;
    }

    public GameObject GetEnemyInRange(Vector3Int coords, int range)
    {
        foreach (GameObject entry in enemyList)
        {
            if (range >= Mathf.Abs(entry.GetComponent<EnemyController>().coords.x - coords.x) + Mathf.Abs(entry.GetComponent<EnemyController>().coords.y - coords.y))
            {
                return entry;
            }
        }
        return null;
    }

    //-----------Simplificar con metodo Pathfinding--------------
    public GameObject GetEnemyInPosition(Vector3Int coords)
    {
        foreach (GameObject entry in enemyList)
        {
            if (entry.GetComponent<EnemyController>().coords.Equals(coords))
            {
                return entry;
            }
        }
        return null;
    }

    public bool IsEnemyInRange(GameObject enemy, int range) 
    {
        return range >= Mathf.Abs(enemy.GetComponent<EnemyController>().coords.x - GetPlayerCoords().x) + Mathf.Abs(enemy.GetComponent<EnemyController>().coords.y - GetPlayerCoords().y);
    }

    public bool isCellWalkable(Vector3Int coords)
    {
        foreach (TileBase tile in getTilesInDepth(coords)){
            if (tile != null){
                if(dataFromTiles[tile].transitable == false)
                    return false;
            }
        }
        return true;
    }

    public bool isCellOccupied(Vector3Int coords) 
    {
        if (occupiedTiles.Contains(coords))
        {
            return true;
        }
        return false;
    }

    public bool isCellTransitable(Vector3Int coords) 
    {
        return isCellWalkable(coords) && !isCellOccupied(coords);
    }

    public Vector3Int cellToLocal(Vector3Int coords){
        return Vector3Int.FloorToInt(tilemap.CellToLocal(coords));
    }

    public TileBase[] getTilesInDepth(Vector3Int coords){
        int depth = 3;
        TileBase[] tileBases = new TileBase[depth];
        for(var i = 0; i < depth; i++){
            tileBases[i] = tilemap.GetTile(coords + i*Vector3Int.back);
        }
        return tileBases;
    }

    public void AddOccupiedTile(Vector3Int location){
        occupiedTiles.Add(location);
    }

    public void RemoveOccupiedTile(Vector3Int location){
        occupiedTiles.Remove(location);
    }
    
    public void OccupiedCellsInForemap(){
        foreach (var pos in foremap.cellBounds.allPositionsWithin){
            Vector3Int location = new Vector3Int(pos.x, pos.y, 0);
            if (foremap.HasTile(location)){
                //Debug.Log(foremap.GetTile(location));
                if (occupiedTiles.Contains(location) || occupiedTiles.Contains(location + Vector3Int.down)
                ){
                    foremap.SetColor(location, new Color(1f, 1f, 1f, percentageAlpha));
                } else{
                    foremap.SetColor(location, Color.white);
                }
                
            }
        }
    }


    //Metodo que encuentro el siguiente movimiento de los enemigos o npcs
    public Vector3Int FindNextMove(Vector3Int pos) 
    {
        var path = Pathfinding.FindPath(GetTileAtPosition(pos), GetTileAtPosition(GetPlayerCoords()));

        if (path != null)
        {
            NodeBase nextMove = path[path.Count - 1];
            Vector3Int nextPos = nextMove.Pos;
            Vector3Int dir = nextPos - pos;

            return dir;
        }

        //Si no hay un camino hacia el jugador se queda quieto
        return Vector3Int.zero;
    }

    //Metodo que devuelve el nodo del grid en base a la posicion en el mapa
    public NodeBase GetTileAtPosition(Vector3Int pos) => Tiles.TryGetValue(pos, out var tile) ? tile : null;

    //Metodo que devuelve la posicion del jugador
    public Vector3Int GetPlayerCoords() 
    {
        return player.coords;
    }

    public GameObject GetPlayer() 
    {
        return player.gameObject;
    }

    public List<GameObject> GetEnemyList() 
    {
        return enemyList;
    }
}
