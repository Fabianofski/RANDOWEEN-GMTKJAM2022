using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICounterDisplay : MonoBehaviour
{

    private TextMeshProUGUI amountText;

    private void Awake()
    {
        amountText = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void SetAmount(int amount)
    {
        amountText.text = amount + "";
    }
    
}
