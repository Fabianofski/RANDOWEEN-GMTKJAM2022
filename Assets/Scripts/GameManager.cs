using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityAtoms.BaseAtoms;

public class GameManager : MonoBehaviour
{

    [SerializeField] private BoolVariable gameOver;

    public void HealthChanged(int newHealth)
    {
        if (newHealth <= 0) gameOver.Value = true;
    }
}
