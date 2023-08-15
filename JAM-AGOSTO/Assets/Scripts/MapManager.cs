using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    public Tilemap tilemap;
    public List<TileData> tileDatas;
    public Dictionary<TileBase,TileData> dataFromTiles;
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
    }

    // Update is called once per frame
    void Update()
    {

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
}
