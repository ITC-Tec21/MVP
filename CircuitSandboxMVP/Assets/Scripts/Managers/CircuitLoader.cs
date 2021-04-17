using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CircuitLoader : MonoBehaviour
{
    public Grid grid;
    private int index;
    public Tilemap tilemap;
    public Sprite[] wireSprites;
    public Sprite[] twoWiresSprites;
    public Sprite andSprite;
    public Sprite orSprite;
    public Sprite notSprite;
    public Sprite inputOnSprite;
    public Sprite inputOffSprite;
    public Sprite outputOnSprite;
    public Sprite outputOffSprite;
    public Sprite placeholderSprite;
    public void Awake()
    {
        Debug.Log(Circuit.circuitComponents.Count);
        foreach (Vector3Int pos in tilemap.cellBounds.allPositionsWithin)
        {
            TileBase current = tilemap.GetTile(pos);
            if(current is WireTile)
            {
                WireTile wire = ScriptableObject.CreateInstance<WireTile>();
                wire.wireSprites = wireSprites;
                tilemap.SetTile(new Vector3Int(pos.x, pos.y, 0), wire);
            }
            else if(current is AndTile)
            {
                AndTile and = ScriptableObject.CreateInstance<AndTile>();
                and.trueAndSprites = twoWiresSprites;
                and.sprite = andSprite;
                tilemap.SetTile(new Vector3Int(pos.x, pos.y, 0), and);
            }
            else if(current is OrTile)
            {
                OrTile or = ScriptableObject.CreateInstance<OrTile>();
                or.trueOrSprites = twoWiresSprites;
                or.sprite = orSprite;
                tilemap.SetTile(new Vector3Int(pos.x, pos.y, 0), or);
            }
            else if(current is NotTile)
            {
                NotTile not = ScriptableObject.CreateInstance<NotTile>();
                not.sprite = notSprite;
                tilemap.SetTile(new Vector3Int(pos.x, pos.y, 0), not);
            }
            else if(current is InputOnTile)
            {
                InputOnTile inputOn = ScriptableObject.CreateInstance<InputOnTile>();
                inputOn.sprite = inputOnSprite;
                tilemap.SetTile(new Vector3Int(pos.x, pos.y, 0), inputOn);
            }
            else if(current is InputOffTile)
            {
                InputOffTile inputOff = ScriptableObject.CreateInstance<InputOffTile>();
                inputOff.sprite = inputOffSprite;
                tilemap.SetTile(new Vector3Int(pos.x, pos.y, 0), inputOff);
            }
            else if(current is OutputTile)
            {
                OutputTile output = ScriptableObject.CreateInstance<OutputTile>();
                output.onSprite = outputOnSprite;
                output.offSprite = outputOffSprite;
                tilemap.SetTile(new Vector3Int(pos.x, pos.y, 0), output);
            }
            else if(current is PlaceholderTile)
            {
                PlaceholderTile placeholder = ScriptableObject.CreateInstance<PlaceholderTile>();
                placeholder.sprite = placeholderSprite;
                tilemap.SetTile(new Vector3Int(pos.x, pos.y, 0), placeholder);
            }
            else
            {
                tilemap.SetTile(new Vector3Int(pos.x, pos.y, 0), null);
            }
        }
        
        //Debug.Log(Circuit.circuitComponents.Count);
    }

    private void OnMouseDown() 
    {
        Debug.Log("OnMouseDown()");
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int location = grid.WorldToCell(worldPosition);
        switch (index)
        {
            case 0: {
                WireTile tile = ScriptableObject.CreateInstance<WireTile>();
                tile.wireSprites = wireSprites;
                tilemap.SetTile(location, tile);
                Debug.Log("index " + index);
                break;
            }
            case 1: {
                AndTile tile = ScriptableObject.CreateInstance<AndTile>();
                tile.trueAndSprites = twoWiresSprites;
                tile.sprite = andSprite;
                tilemap.SetTile(location, tile);
                break;
            }
            case 2: {
                OrTile tile = ScriptableObject.CreateInstance<OrTile>();
                tile.trueOrSprites = twoWiresSprites;
                tile.sprite = orSprite;
                tilemap.SetTile(location, tile);
                break;
            }
            case 3: {
                NotTile tile = ScriptableObject.CreateInstance<NotTile>();
                tile.sprite = notSprite;
                tilemap.SetTile(location, tile);
                break;
            }
            case 4: {
                InputOffTile tile = ScriptableObject.CreateInstance<InputOffTile>();
                tile.sprite = inputOffSprite;
                tilemap.SetTile(location, tile);
                break;
            }
            case 5: {
                InputOnTile tile = ScriptableObject.CreateInstance<InputOnTile>();
                tilemap.SetTile(location, tile);
                tile.sprite = inputOnSprite;
                break;
            }
            case 6: {
                OutputTile tile = ScriptableObject.CreateInstance<OutputTile>();
                tilemap.SetTile(location, tile);
                tile.onSprite = outputOnSprite;
                tile.offSprite = outputOffSprite;
                break;
            }
            case 7: {
                Circuit.RemoveComponent(location);
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