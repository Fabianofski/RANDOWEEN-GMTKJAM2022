using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityAtoms.BaseAtoms;

public class GameManager : MonoBehaviour
{

    [SerializeField] private BoolVariable gameOver;
    [SerializeField] private TextMeshProUGUI healthUI;

    public void HealthChanged(int newHealth)
    {
        healthUI.text = $"{newHealth} Lives";
        if (newHealth == 0) gameOver.Value = true;
    }
}
