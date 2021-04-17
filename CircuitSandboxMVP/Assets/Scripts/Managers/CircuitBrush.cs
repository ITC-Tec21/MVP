using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CircuitBrush : MonoBehaviour
{
    public Grid grid;
    private int index;
    //private Vector3Int previousLocation = null;
    public Tilemap tilemap;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update para después
    // void Update()
    // {
    //     Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //     Vector3Int location = grid.WorldToCell(worldPosition);
    //     // ScriptableObject.CreateInstance()
    //     if (location != previousLocation) {
    //         switch (index)
    //         {
    //             case 0: {
    //                 TileBase tile = ScriptableObject.CreateInstance<WireTile>();
    //                 tilemap.SetTile(location, tile);
    //                 tilemap.SetTile(previousLocation, null);
    //             }
    //             default:
    //         }
    //     }
    //     previousLocation = location;
    // }

    // Update para ahora
    void Update()
    {

    }

    private void OnMouseDown() 
    {
        Debug.Log("OnMouseDown()");
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int location = grid.WorldToCell(worldPosition);
        switch (index)
        {
            case 0: {
                TileBase tile = ScriptableObject.CreateInstance<WireTile>();
                tilemap.SetTile(location, tile);
                Debug.Log("index " + index);
                break;
            }
            case 1: {
                TileBase tile = ScriptableObject.CreateInstance<AndTile>();
                tilemap.SetTile(location, tile);
                break;
            }
            case 2: {
                TileBase tile = ScriptableObject.CreateInstance<OrTile>();
                tilemap.SetTile(location, tile);
                break;
            }
            case 3: {
                TileBase tile = ScriptableObject.CreateInstance<NotTile>();
                tilemap.SetTile(location, tile);
                break;
            }
            case 4: {
                TileBase tile = ScriptableObject.CreateInstance<InputOffTile>();
                tilemap.SetTile(location, tile);
                break;
            }
            case 5: {
                TileBase tile = ScriptableObject.CreateInstance<InputOnTile>();
                tilemap.SetTile(location, tile);
                break;
            }
            case 6: {
                TileBase tile = ScriptableObject.CreateInstance<OutputTile>();
                tilemap.SetTile(location, tile);
                break;
            }
            case 7: {
                tilemap.SetTile(location, null);
                break;
            }
            default: break;
        }
    }

    public void WireClick() {
        index = 0;
    }

    public void AndClick() {
        index = 1;
    }

    public void OrClick() {
        index = 2;
    }

    public void NotClick() {
        index = 3;
    }

    public void OffInputClick() {
        index = 4;
    }

    public void OnInputClick() {
        index = 5;
    }

    public void OutputClick() {
        index = 6;
    }
    public void EraserClick() {
        index = 7;
    }
}
