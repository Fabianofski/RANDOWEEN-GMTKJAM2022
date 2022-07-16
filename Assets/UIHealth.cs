using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealth : MonoBehaviour
{

    [SerializeField] private Image healthImage;
    [SerializeField] private int maxHealth;


    public void SetMaxHealth(int health)
    {
        maxHealth = health;
    }
    
    public void SetHealth(int health)
    {
        EnableChildren();
        healthImage.fillAmount = (float) health / maxHealth;
    }

    private void EnableChildren()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(true);
    }
    
}
