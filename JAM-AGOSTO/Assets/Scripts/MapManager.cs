using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    public Tilemap tilemap;
    public Tilemap foremap;
    public List<TileData> tileDatas;
    public Dictionary<TileBase,TileData> dataFromTiles;
    public List<Vector3Int> occupiedTiles = new List<Vector3Int>();
    //public Vector3Int location;
    //public TileBase clickedTile;

    public Camera mainCamera;
    public Camera followCamera;



    private void Awake(){
        dataFromTiles =  new  Dictionary<TileBase,TileData>();
        foreach (var tileData in tileDatas){
            foreach(var tile in tileData.tiles){
                dataFromTiles.Add(tile,tileData);
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        mainCamera.enabled = true;
        followCamera.enabled = false;
        foreach (var pos in foremap.cellBounds.allPositionsWithin){
            Vector3Int location = new Vector3Int(pos.x, pos.y, 0);
            if (foremap.HasTile(location)){
                foremap.SetTileFlags(location, TileFlags.None);
            }
        }
    }

    // Update is called once per frame
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

    public bool isCellTransitable(Vector3Int coords){
        bool cellTransitable = true;
        foreach (TileBase tile in getTilesInDepth(coords)){
            if (tile != null){
                if(dataFromTiles[tile].transitable == false)
                    cellTransitable = false;
            }
        }
        return cellTransitable;
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
                if (occupiedTiles.Contains(location) || occupiedTiles.Contains(location + Vector3Int.down)|| occupiedTiles.Contains(location + Vector3Int.up)
                || occupiedTiles.Contains(location + Vector3Int.right)|| occupiedTiles.Contains(location + Vector3Int.left)
                || occupiedTiles.Contains(location + Vector3Int.right + Vector3Int.down)|| occupiedTiles.Contains(location + Vector3Int.left + Vector3Int.down)
                || occupiedTiles.Contains(location + Vector3Int.right + Vector3Int.up)|| occupiedTiles.Contains(location + Vector3Int.left + Vector3Int.up)
                ){
                    foremap.SetColor(location, new Color(1f, 1f, 1f, 0.5f));
                } else{
                    foremap.SetColor(location, Color.white);
                }
                
            }
        }
    }

}
