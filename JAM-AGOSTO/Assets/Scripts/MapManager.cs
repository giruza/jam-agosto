using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    public Tilemap tilemap;
    public List<TileData> tileDatas;
    private Dictionary<TileBase,TileData> dataFromTiles;

    public Vector3Int location;

    public TileBase clickedTile;



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

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            location = tilemap.WorldToCell(mp);
            clickedTile = tilemap.GetTile(location);
            Debug.Log("Pos: " + location + " / " + "transitable: " + dataFromTiles[clickedTile].transitable);
        }
    }
}
