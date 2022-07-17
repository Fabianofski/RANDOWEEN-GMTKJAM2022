using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{

    [SerializeField] private GameObject shopToggle;

    public void AddToShop(GameObject building)
    {
        GameObject spawned = Instantiate(shopToggle, transform);
        spawned.GetComponentInChildren<Toggle>().group = GetComponent<ToggleGroup>();
        spawned.GetComponentInChildren<ShopToggle>().SetBuildingType(building);
    }

}
