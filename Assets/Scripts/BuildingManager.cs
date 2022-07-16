using System;
using System.Collections;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class BuildingManager : MonoBehaviour
{

    [SerializeField] private GameObjectVariable selectedBuilding;
    [SerializeField] private VoidEvent itemGotPlaced;
    [SerializeField] private Tilemap tilemap;
    private TilemapRenderer tilemapRenderer;
    private Vector2 worldMousePos;

    private void Awake()
    {
        tilemapRenderer = tilemap.GetComponent<TilemapRenderer>();
    }

    private void Update()
    {
        tilemapRenderer.enabled = selectedBuilding.Value != null;

    }

    public void OnMouseClick()
    {
        if (selectedBuilding.Value == null) return;

        Vector3Int tilePos = new Vector3Int(Mathf.RoundToInt(worldMousePos.x), Mathf.RoundToInt(worldMousePos.y) - 1, 0);
        if (TileIsNotValid(tilePos)) return;

        Vector3 buildingPos = tilePos + new Vector3(0, 0.5f, 0);
        Instantiate(selectedBuilding.Value, buildingPos, Quaternion.identity);
        itemGotPlaced.Raise();
    }

    private bool TileIsNotValid(Vector3Int position)
    {
        if (!tilemap.HasTile(position)) return true;
        return Physics2D.OverlapCircle((Vector3) position, .5f);
    }
    
    public void OnMousePositionChanged(InputValue value)
    {
        Vector2 mousePos = value.Get<Vector2>();
        if (Camera.main != null) 
            worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);
    }

}
