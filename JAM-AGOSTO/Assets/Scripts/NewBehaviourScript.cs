using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class NewBehaviourScript : MonoBehaviour
{
    public Tilemap tilemap;
    public Vector3Int location;
    public Tile tile;

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
            tile = tilemap.GetTile<Tile>(location);
            Debug.Log("sprite: " + tile.sprite);
            Debug.Log("color: " + tile.color);
            Debug.Log("transform: " + tile.transform);
            Debug.Log("flags: " + tile.flags);

        }
    }
}
