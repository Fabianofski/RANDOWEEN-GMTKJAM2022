using System;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class BuildingManager : MonoBehaviour
{

    [SerializeField] private GameObjectVariable selectedBuilding;
    [SerializeField] private StringVariable upgradeState;
    [SerializeField] private VoidEvent itemGotPlaced;
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private LayerMask buildingLayer;
    private TilemapRenderer tilemapRenderer;
    private Vector2 worldMousePos;

    [SerializeField] private IntVariable UpgradesLeft;
    [SerializeField] private IntVariable DowngradesLeft;

    private GameObject preview;

    private void Awake()
    {
        tilemapRenderer = tilemap.GetComponent<TilemapRenderer>();

    }

    private void Update()
    {
        bool selected = selectedBuilding.Value != null;
        tilemapRenderer.enabled = selected;

        if (selected) ShowPreview();
        else Destroy(preview);
    }

    private void ShowPreview()
    {
        if (ConvertMousePosToTilePos(out var buildingPos)) return;
        if (preview == null)
        {
            preview = Instantiate(selectedBuilding.Value, buildingPos, Quaternion.identity);
            preview.GetComponentInChildren<Building.Building>().Disable();
            preview.GetComponentInChildren<BoxCollider2D>().enabled = false;
        }
        preview.transform.position = buildingPos;
    }
    
    private bool ConvertMousePosToTilePos(out Vector3 buildingPos)
    {
        buildingPos = Vector3.zero;
        Vector3Int tilePos = new Vector3Int(Mathf.RoundToInt(worldMousePos.x), Mathf.RoundToInt(worldMousePos.y), 0);
        if (TileIsNotValid(tilePos)) return true;

        buildingPos = tilePos - new Vector3(0, .5f, 0);
        return false;
    }

    private bool TileIsNotValid(Vector3Int position)
    {
        if (!tilemap.HasTile(position)) return true;
        return Physics2D.OverlapCircle((Vector3) position, .2f, buildingLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (ConvertMousePosToTilePos(out var buildingPos)) return;
        Gizmos.DrawWireSphere(buildingPos + new Vector3(0, .5f, 0), .2f);
    }

    public void OnMouseClick()
    {
        if (preview != null)
        {
            PlaceBuilding();
            return;
        }

        if (upgradeState.Value != "") UpDowngradeBuilding();
        
    }

    private void UpDowngradeBuilding()
    {
        Vector3Int tilePos = new Vector3Int(Mathf.RoundToInt(worldMousePos.x), Mathf.RoundToInt(worldMousePos.y), 0);
        Vector3 buildingPos = tilePos;

        Collider2D building = Physics2D.OverlapCircle(buildingPos, .2f);
        if (building == null) return;
        
        Building.Building buildingOb = building.GetComponentInChildren<Building.Building>();
        
        if (buildingOb == null) return;
        if (upgradeState.Value == "Upgrade" && buildingOb.IsUpgradeable() && UpgradesLeft.Value > 0)
        {
            UpgradesLeft.Value -= 1;
            buildingOb.Upgrade();
        }
        else if (upgradeState.Value == "Downgrade" && buildingOb.IsDowngradeable() && DowngradesLeft.Value > 0)
        {
            DowngradesLeft.Value -= 1;
            buildingOb.Downgrade();
        }
    }
    
    private void PlaceBuilding()
    {
        preview.GetComponentInChildren<Building.Building>().Enable();
        preview.GetComponentInChildren<BoxCollider2D>().enabled = true;
        preview = null;

        itemGotPlaced.Raise();
    }

    public void OnMousePositionChanged(InputValue value)
    {
        Vector2 mousePos = value.Get<Vector2>();
        if (Camera.main != null) 
            worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);
    }

}
