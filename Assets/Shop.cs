using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{

    [SerializeField] private GameObject shopToggle;
    private Sprite sprite;


    public void SetSprite(Sprite img)
    {
        sprite = img;
    }
    
    public void AddToShop(GameObject building)
    {
        GameObject spawned = Instantiate(shopToggle, transform);
        spawned.GetComponentInChildren<Toggle>().group = GetComponent<ToggleGroup>();
        spawned.GetComponentInChildren<ShopToggle>().SetBuildingType(building);
        spawned.GetComponentInChildren<Image>().sprite = sprite;
    }

}
