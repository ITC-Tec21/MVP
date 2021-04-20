using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CircuitManager : MonoBehaviour
{
    public Grid grid;
    private int index;
    public Tilemap tilemap;
    public AllSprites allSprites;
    public GameObject toolBox;
    public GameObject toolBoxButton;
    public bool sandBoxMode;
    public HashSet<Vector3Int> placeholders = new HashSet<Vector3Int>();
    public void Awake()
    {
        Debug.Log(Circuit.circuitComponents.Count);
        foreach (Vector3Int pos in tilemap.cellBounds.allPositionsWithin)
        {
            TileBase current = tilemap.GetTile(pos);
            if(current is WireTile)
            {
                WireTile wire = ScriptableObject.CreateInstance<WireTile>();
                wire.wireSprites = allSprites.wireSprites;
                tilemap.SetTile(new Vector3Int(pos.x, pos.y, 0), wire);
            }
            else if(current is AndTile)
            {
                AndTile and = ScriptableObject.CreateInstance<AndTile>();
                and.trueAndSprites = allSprites.twoWiresSprites;
                and.sprite = allSprites.andSprite;
                tilemap.SetTile(new Vector3Int(pos.x, pos.y, 0), and);
            }
            else if(current is OrTile)
            {
                OrTile or = ScriptableObject.CreateInstance<OrTile>();
                or.trueOrSprites = allSprites.twoWiresSprites;
                or.sprite = allSprites.orSprite;
                tilemap.SetTile(new Vector3Int(pos.x, pos.y, 0), or);
            }
            else if(current is NotTile)
            {
                NotTile not = ScriptableObject.CreateInstance<NotTile>();
                not.sprite = allSprites.notSprite;
                tilemap.SetTile(new Vector3Int(pos.x, pos.y, 0), not);
            }
            else if(current is InputOnTile)
            {
                InputOnTile inputOn = ScriptableObject.CreateInstance<InputOnTile>();
                inputOn.sprite = allSprites.inputOnSprite;
                tilemap.SetTile(new Vector3Int(pos.x, pos.y, 0), inputOn);
            }
            else if(current is InputOffTile)
            {
                InputOffTile inputOff = ScriptableObject.CreateInstance<InputOffTile>();
                inputOff.sprite = allSprites.inputOffSprite;
                tilemap.SetTile(new Vector3Int(pos.x, pos.y, 0), inputOff);
            }
            else if(current is OutputTile)
            {
                OutputTile output = ScriptableObject.CreateInstance<OutputTile>();
                output.onSprite = allSprites.outputOnSprite;
                output.offSprite = allSprites.outputOffSprite;
                tilemap.SetTile(new Vector3Int(pos.x, pos.y, 0), output);
            }
            else if(current is PlaceholderTile)
            {
                PlaceholderTile placeholder = ScriptableObject.CreateInstance<PlaceholderTile>();
                placeholder.allSprites = allSprites;
                placeholder.truePlaceholderSprites = allSprites.twoWiresSprites;
                placeholder.sprite = allSprites.placeholderSprite;
                placeholders.Add(new Vector3Int(pos.x, pos.y, 0));
                tilemap.SetTile(new Vector3Int(pos.x, pos.y, 0), placeholder);
            }
            else
            {
                tilemap.SetTile(new Vector3Int(pos.x, pos.y, 0), null);
            }
        }
    }

    private void OnMouseDown() 
    {
        // Debug.Log("OnMouseDown()");
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int location = grid.WorldToCell(worldPosition);

        if (sandBoxMode || placeholders.Contains(location)) {
            switch (index)
            {
                case 0: {
                    WireTile tile = ScriptableObject.CreateInstance<WireTile>();
                    tile.wireSprites = allSprites.wireSprites;
                    tilemap.SetTile(location, tile);
                    Debug.Log("index " + index);
                    break;
                }
                case 1: {
                    AndTile tile = ScriptableObject.CreateInstance<AndTile>();
                    tile.trueAndSprites = allSprites.twoWiresSprites;
                    tile.sprite = allSprites.andSprite;
                    tilemap.SetTile(location, tile);
                    break;
                }
                case 2: {
                    OrTile tile = ScriptableObject.CreateInstance<OrTile>();
                    tile.trueOrSprites = allSprites.twoWiresSprites;
                    tile.sprite = allSprites.orSprite;
                    tilemap.SetTile(location, tile);
                    break;
                }
                case 3: {
                    NotTile tile = ScriptableObject.CreateInstance<NotTile>();
                    tile.sprite = allSprites.notSprite;
                    tilemap.SetTile(location, tile);
                    break;
                }
                case 4: {
                    InputOffTile tile = ScriptableObject.CreateInstance<InputOffTile>();
                    tile.sprite = allSprites.inputOffSprite;
                    tilemap.SetTile(location, tile);
                    break;
                }
                case 5: {
                    Debug.Log("index " + index);
                    InputOnTile tile = ScriptableObject.CreateInstance<InputOnTile>();
                    tile.sprite = allSprites.inputOnSprite;
                    tilemap.SetTile(location, tile);
                    break;
                }
                case 6: {
                    OutputTile tile = ScriptableObject.CreateInstance<OutputTile>();
                    tile.onSprite = allSprites.outputOnSprite;
                    tile.offSprite = allSprites.outputOffSprite;
                    tilemap.SetTile(location, tile);
                    break;
                }
                case 7: {
                    if(sandBoxMode) {
                        Circuit.RemoveComponent(location);
                        tilemap.SetTile(location, null);
                    }
                    else {
                        PlaceholderTile tile = ScriptableObject.CreateInstance<PlaceholderTile>();
                        tile.sprite = allSprites.placeholderSprite;
                        tilemap.SetTile(location, tile);
                    }
                    break;
                }
                default: break;
            }            
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

    public void ToolBoxToggle() {
        if (toolBox.activeSelf) {
            toolBox.SetActive(false);
            gameObject.GetComponent<PolygonCollider2D>().enabled = true;
            //270
            toolBoxButton.transform.rotation = Quaternion.Euler(Vector3.forward * 90);
        }
        else {
            toolBox.SetActive(true);
            gameObject.GetComponent<PolygonCollider2D>().enabled = false;
            toolBoxButton.transform.rotation = Quaternion.Euler(Vector3.forward * 270);
        }
    }
}