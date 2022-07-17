using System;
using System.Collections;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.UI;

public class ShopToggle : MonoBehaviour
{
    private ToggleGroup toggleGroup;
    private Toggle toggle;
    [SerializeField] private GameObject buildingType;
    [SerializeField] private GameObjectVariable selectedBuildingType;

    private void Awake()
    {
        if(buildingType != null) SetBuildingType(buildingType);
    }

    public void SetBuildingType(GameObject go)
    {
        buildingType = go;
        toggle = GetComponent<Toggle>();
        toggleGroup = toggle.group;
    }
    
    public void SetSelectedItem(bool selected)
    {
        if(selected)
            selectedBuildingType.Value = buildingType;
        else if (!toggleGroup.AnyTogglesOn()) selectedBuildingType.Value = null;
    }

    public void ItemGotPlaced()
    {
        if (!toggle.isOn) return;
        selectedBuildingType.Value = null;
        Destroy(gameObject);
    }
}
