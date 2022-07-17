using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.UI;

public class UIUpgrade : MonoBehaviour
{
    private ToggleGroup toggleGroup;
    [SerializeField] private StringVariable upgradeState;
    
    
    private void Awake()
    {
        toggleGroup = GetComponent<ToggleGroup>();
    }

    public void ToggleGroupChanged(bool on)
    {
        upgradeState.Value = on ? toggleGroup.ActiveToggles().FirstOrDefault().gameObject.name : "";
    }
    
}
