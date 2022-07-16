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
    private Vector2 worldMousePos;
    
    public void OnMouseClick()
    {
        if (selectedBuilding == null) return;
        
        Debug.Log("Click: " + worldMousePos);
        Vector2 tilePos = new Vector2(Mathf.RoundToInt(worldMousePos.x), Mathf.RoundToInt(worldMousePos.y) -.5f);
        
        Instantiate(selectedBuilding.Value, tilePos, Quaternion.identity);
        itemGotPlaced.Raise();
    }

    public void OnMousePositionChanged(InputValue value)
    {
        Vector2 mousePos = value.Get<Vector2>();
        if (Camera.main != null) 
            worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);
    }

}
